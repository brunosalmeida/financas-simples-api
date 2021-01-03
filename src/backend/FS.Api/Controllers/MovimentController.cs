namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands.Command;
    using Application.Queries.Query;
    using DataObject.Moviment.Request;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.Reflection.Metadata.Ecma335;

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

        [HttpPost("user/{userId}/account/{accountId}/moviment")]
        public async Task<IActionResult> CreateMoviment([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromBody] CreateMovimentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovimentCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/account/balance/{result}", new {id = result});
        }

        [HttpPut("user/{userId}/account/{accountId}/moviment/{movimentId}")]
        public async Task<IActionResult> EditMoviment([FromRoute] Guid userId, [FromRoute] Guid accountId,
           [FromRoute] Guid movimentId, [FromBody] CreateMovimentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateMovimentCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/moviment/{result}", new {id = result});
        }

        [HttpPost("user/{userId}/account/{accountId}/moviment/installment")]
        public async Task<IActionResult> CreateInstallmentMoviment([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromBody] CreateInstallmentMovimentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateInstallmentMovimentCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,
                request.Months, request.StartMonth, request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/account/balance/{result}", new {id = result});
        }

        [HttpGet("user/{userId}/account/{accountId}")]
        public async Task<IActionResult> GetAllMoviments([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromQuery] int page = 1, [FromQuery] int size = 30)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            var query = new GetAllMovimentsQuery(userInfo.UserId, userInfo.AccountId, page, size);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }
    }
}