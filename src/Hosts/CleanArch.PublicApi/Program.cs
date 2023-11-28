using System.Net;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using FluentValidation;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

using CleanArch.Controllers;
using CleanArch.Controllers.Properties;
using CleanArch.DataAccess.SqlServer;
using CleanArch.UseCases;
using CleanArch.Infrastructure.Implementations;
using CleanArch.Infrastructure.Implementations.BlobStorage;
using CleanArch.Infrastructure.BackgroundWorkers;
using CleanArch.UseCases.Common.Exceptions;
using CleanArch.Controllers.Common;
using CleanArch.Infrastructure.Implementations.MultiTenancy;

var builder = WebApplication.CreateBuilder(args);

#region Configure Service Provider

builder.Host.UseDefaultServiceProvider(options =>
{
	options.ValidateScopes = builder.Environment.IsDevelopment();
	options.ValidateOnBuild = builder.Environment.IsDevelopment();
});

#endregion


#region Configure Logging

builder.Host.UseSerilog((context, service, configuration) =>
{
	configuration
		.ReadFrom.Configuration(context.Configuration)
		.ReadFrom.Services(service)
		.MinimumLevel.Information()
		.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
		.Enrich.FromLogContext()
		.Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
		.WriteTo.Console()
		.WriteTo.File(
			path: "Logs/logs-.txt",
			formatter: new CompactJsonFormatter(),
			rollingInterval: RollingInterval.Day,
			fileSizeLimitBytes: 10 * 1024 * 1024,
			retainedFileCountLimit: 3,
			rollOnFileSizeLimit: true,
			flushToDiskInterval: TimeSpan.FromSeconds(1));
});

builder.Services.AddHttpLogging(options =>
{
	options.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
});

#endregion


#region Configure Profiler

builder.Services.AddMiniProfiler(options =>
{
	options.RouteBasePath = "/profiler";
	options.IgnoredPaths.Add("/swagger");
})
	.AddEntityFramework();

#endregion


#region Configure Web

builder.Services.AddEndpointsApiExplorer();

#endregion


#region Configure Swagger

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Clean Architecture API",
		Description = $"An ASP.NET Core Web API example on clean architecture.\n\nExist users:\n- admin@mail.test.com\n- productowner@mail.test.com\n- warehouse_worker@mail.test.com\n- customer@mail.test.com \n\nPassword: 123456"
	});

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	options.AddSecurityDefinition("X-TenantId", new OpenApiSecurityScheme
	{
		Name = "X-TenantId",
		In = ParameterLocation.Header,
		Description = "Tenant ID",
		Type = SecuritySchemeType.ApiKey,
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

#endregion


#region Configure Cache

var redisConnection = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = redisConnection;
});

#endregion


#region Configure Main Application 

var blobStorageSettings = builder.Configuration
	.GetRequiredSection(nameof(BlobStorageSettings))
	.Get<BlobStorageSettings>()!;

builder.Services
	.AddPublicApiControllers()
	.AddUseCases()
	.AddTenantProvider()
	.AddUserProvider(options =>
	{
		options.IdClaimName = ClaimTypes.NameIdentifier;
		options.RolesClaimName = ClaimTypes.Role;
		options.UserNameClaimName = ClaimTypes.Name;
		options.EmailClaimName = ClaimTypes.Email;
	})
	.AddDataAccess()
	.AddBlobStorage(blobStorageSettings)
	.AddApplicationAuthentication()
	.AddBackgroundWorkers();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	using (var scope = app.Services.CreateScope())
	{
		DatabaseInitializer.InitFromMigrations(scope.ServiceProvider);
		DatabaseInitializer.SeedData(scope.ServiceProvider);
	}

	using (var scope = app.Services.CreateScope())
	{
		var tenancySettings = scope.ServiceProvider
			.GetRequiredService<IOptions<MultiTenancySettings>>()
			.Value;

		var connections = tenancySettings.Tenants
			.Where(t => !string.IsNullOrWhiteSpace(t.ConnectionString))
			.Select(t => t.ConnectionString)
			.Distinct();

		foreach (var connectionString in connections)
		{
			DatabaseInitializer.InitFromMigrations(scope.ServiceProvider, connectionString!);
		}
	}

	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseMiniProfiler();
}

app.UseExceptionHandler(cfg =>
{
	cfg.Run(async context =>
	{
		var feature = context.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();

		switch (feature.Error)
		{
			case ValidationException validationError:
				if (Log.Logger.IsEnabled(LogEventLevel.Warning))
				{
					Log.Logger.Warning(validationError.ToString() + "\nData: " + JsonSerializer.Serialize(feature.Error.Data));
				}

				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				await context.Response.WriteAsJsonAsync(
					new ErrorResponse(
						(int)HttpStatusCode.BadRequest,
						"Validation Error",
						validationError.Message,
						validationError.Errors.ToDictionary(x => x.PropertyName, x => new { x.ErrorCode, x.ErrorMessage })));
				break;
			case UseCaseException useCaseException:
				if (Log.Logger.IsEnabled(LogEventLevel.Warning))
				{
					Log.Logger.Warning(useCaseException.ToString() + "\nData: " + JsonSerializer.Serialize(feature.Error.Data));
				}

				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				await context.Response.WriteAsJsonAsync(
					new ErrorResponse(
						(int)HttpStatusCode.BadRequest,
						"Application Error",
						useCaseException.Message,
						useCaseException.Data));
				break;
			default:
				if (Log.Logger.IsEnabled(LogEventLevel.Error))
				{
					Log.Logger.Error(feature.Error.ToString() + "\nData: " + JsonSerializer.Serialize(feature.Error.Data));
				}

				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				await context.Response.WriteAsJsonAsync(
					new ErrorResponse(
					(int)HttpStatusCode.InternalServerError,
						"Fatal Application Error",
						app.Environment.IsDevelopment() ? feature.Error.Message : "Application error, try again later or contact administrator",
						feature.Error.Data));
				break;
		}
	});
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program() { }