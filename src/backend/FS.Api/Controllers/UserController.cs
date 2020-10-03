using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FS.Api.Commands.Command;
using FS.Api.Queries.Request;
using FS.DataObject.User.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace FS.Api.Controllers
{
   
    [ApiController]
    [Route("v1")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserQuery {Id =id};

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand(request.Name, request.Email, request.Password);

            var result = await _mediator.Send(command);

            if (result is null) return BadRequest();

            return Created($"v1/user/{result}", new {Id = result});
        }

        [HttpPut("user/{id}")]
        public async Task<IActionResult> EditUser(Guid id, [FromBody] EditUserResquest request)
        {
            var command = new EditUserCommand(id, request.Name, request.Email);

            var result = await _mediator.Send(command);

            return Created($"v1/user/{result}", new {Id = result});
        }

        [HttpPost("user/{id}/password/change")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePassword request)
        {
            var command = new ChangePasswordCommand(id, request.OldPassword, 
                request.NewPassword);

            var result = await _mediator.Send(command);
            
            return Created($"v1/user/{result}", new {Id = result});
        }
    }
}