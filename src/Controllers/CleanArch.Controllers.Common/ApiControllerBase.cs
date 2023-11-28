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
    protected const string DefaultContentType = MediaTypeNames.Application.Octet;

    private ISender? _sender;
    private IMapper? _mapper;
    private IContentTypeProvider? _contentTypeProvider;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    protected IContentTypeProvider ContentTypeProvider => _contentTypeProvider ??= new FileExtensionContentTypeProvider();
}