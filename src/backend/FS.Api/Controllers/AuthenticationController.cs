namespace FS.Api.Controllers
{
    using System.Threading.Tasks;
    using Application.Queries.Query;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("v1")]
    public class AuthenticationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public AuthenticationController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var auth = new AuthUserQuery(username, password);
            var result = await _mediator.Send(auth);

            if (string.IsNullOrEmpty(result))
                return NotFound("Invalid username or password");

            return Ok(result);
        }
    }
}