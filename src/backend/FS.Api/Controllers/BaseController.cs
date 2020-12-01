namespace FS.Api.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity))
            {
                return Guid.Empty;
            }

            var claim = identity.FindFirst("user").Value;
                
            return new Guid(claim);
        }
    }
}