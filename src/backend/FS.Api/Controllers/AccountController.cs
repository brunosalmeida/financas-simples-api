namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Commands.Command;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    // [Authorize]
    [ApiController]
    [Route("v1")]
    public class AccountController  : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("account/{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            return Ok();
        }
        
        [HttpGet("account/user/{id}")]
        public async Task<IActionResult> GetAccountByUserId(Guid id)
        {
            return Ok();
        }

        [HttpPost("account")]
        public async Task<IActionResult> CreatAccount([FromHeader]Guid user)
        {
            if (user.Equals(Guid.Empty))
                return BadRequest("No header found!");
            
            var command = new CreateAccountCommand(user);

            var result = await _mediator.Send(command);

            return Created($"account/{result}", new {id = result});
        }
    }
}