// unset

namespace FS.Api.Controllers
{
    using Application.Commands.Command;
    using Application.Queries.Query;
    using DataObject.Moviment.Request;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class InvestmentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvestmentController> _logger;

        public InvestmentController(ILogger<InvestmentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        
        [HttpPost("user/{userId}/account/{accountId}/investment")]
        public async Task<IActionResult> CreateInvestment([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromBody] CreateInvestmentRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateInvestmentCommand(userInfo.UserId, userInfo.AccountId, request.Description,
                request.Value,request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/account/investment/balance/{result}", new {id = result});
        }
        
        [HttpGet("user/{userId}/account/{accountId}/investment")]
        public async Task<IActionResult> GetInvestment([FromRoute] Guid userId, [FromRoute] Guid accountId,
            [FromQuery] int page = 1, [FromQuery] int size = 30)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId) || !accountId.Equals(userInfo.AccountId))
                return BadRequest("Invalid token.");

            var query = new GetAllInvestmentsQuery(userInfo.UserId, userInfo.AccountId, page, size);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }
    }
}