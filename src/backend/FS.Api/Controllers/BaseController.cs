namespace FS.Api.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : ControllerBase
    {
        protected InfoUser GetUserInfo()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity))
            {
                return null;
            }

            var user = identity.FindFirst("user").Value;
            var account = identity.FindFirst("account").Value;

            return new InfoUser(Guid.Parse(user), Guid.Parse(account)); 
        }
    }
    
    public class InfoUser
    {
        public Guid UserId { get; private set; }
        public Guid AccountId { get; private set; }

        public InfoUser(Guid userId, Guid accountId)
        {
            UserId = userId;
            AccountId = accountId;
        }
    }
}