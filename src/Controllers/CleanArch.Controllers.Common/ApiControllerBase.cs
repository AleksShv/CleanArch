using System.Net.Mime;

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesErrorResponseType(typeof(ErrorResponse))]
public abstract class ApiControllerBase : ControllerBase
{
    public const string DefaultContentType = MediaTypeNames.Application.Octet;

    private ISender? _sender;
    private IMapper? _mapper;
    private IContentTypeProvider? _ontentTypeProvider;

    public ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    public IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    public IContentTypeProvider ContentTypeProvider => _ontentTypeProvider ??= new FileExtensionContentTypeProvider();
}