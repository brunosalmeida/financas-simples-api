namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands.Command;
    using DataObject.Moviment.Request;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class MovimentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovimentController> _logger;

        public MovimentController(IMediator mediator, ILogger<MovimentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("user/{userId}/account/moviment")]
        public async Task<IActionResult> CreateMoviment(Guid userId, [FromBody] CreateMovimentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId))
                return BadRequest("Invalid token.");
                
            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovimentCommand(userInfo.UserId, userInfo.AccountId, request.Description, request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/moviment/{result}", new {id = result});
        }
        
        [HttpPut("user/{userId}/account/moviment/{movimentId}")]
        public async Task<IActionResult> EditMoviment(Guid userId, Guid movimentId, [FromBody] CreateMovimentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId))
                return BadRequest("Invalid token.");
            
            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovimentCommand(userInfo.UserId, userInfo.AccountId, request.Description, request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/moviment/{result}", new {id = result});
        }
    }
}