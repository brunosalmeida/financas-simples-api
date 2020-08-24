using System;
using System.Threading.Tasks;
using FS.Api.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FS.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class Authentication : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public Authentication(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("auth")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var auth = new AuthUserQuery(username, password);
            var result = await _mediator.Send(auth);

            if (result == Guid.Empty)
                return NotFound("Invalid username or password");

            return Ok(result);
        }
    }
}