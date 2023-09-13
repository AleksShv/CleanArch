using System.Collections;

namespace CleanArch.Controllers.Common;

public record ErrorResponse(int Status, string Title, string Message, IDictionary? Details);
