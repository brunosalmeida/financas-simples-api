namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands.Command;
    using Application.Queries.Query;
    using DataObject.Movement.Request;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.Reflection.Metadata.Ecma335;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class MovementController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovementController> _logger;

        public MovementController(IMediator mediator, ILogger<MovementController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("user/{userId}/account/{accountId}/movement")]
        public async Task<IActionResult> CreateMovement([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromBody] CreateMovementRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovementCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/account/balance/{result}", new {id = result});
        }

        [HttpPut("user/{userId}/account/{accountId}/movement/{movementId}")]
        public async Task<IActionResult> EditMovement([FromRoute] Guid userId, [FromRoute] Guid accountId,
           [FromRoute] Guid movementId, [FromBody] CreateMovementRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovementCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/movement/{result}", new {id = result});
        }

        [HttpPost("user/{userId}/account/{accountId}/movement/installment")]
        public async Task<IActionResult> CreateInstallmentMovement([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromBody] CreateInstallmentMovementRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateInstallmentMovementCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Months, request.StartMonth, request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/account/balance/{result}", new {id = result});
        }

        [HttpGet("user/{userId}/account/{accountId}")]
        public async Task<IActionResult> GetAllMovements([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromQuery] int page = 1, [FromQuery] int size = 30)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            var query = new GetAllMovementsQuery(userInfo.UserId, userInfo.AccountId, page, size);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }
    }
}