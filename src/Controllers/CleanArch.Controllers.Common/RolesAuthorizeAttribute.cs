using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;

using CleanArch.Entities;
using CleanArch.Utils;

namespace CleanArch.Controllers.Common;

public class RolesAuthorizeAttribute : AuthorizeAttribute
{
    public RolesAuthorizeAttribute(params UserRole[] role)
    {
        Roles = EnumHelper.StringValues(role).JoinWithComma();

        Debug.WriteLine(Roles);
    }
}
