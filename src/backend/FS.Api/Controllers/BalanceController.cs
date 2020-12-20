namespace FS.Api.Controllers
{
    using Application.Queries.Query;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class BalanceController: BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BalanceController> _logger;

        public BalanceController(IMediator mediator, ILogger<BalanceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("user/{userId}/account/balance")]
        public async Task<ActionResult> GetBalance(Guid userId)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId))
                return BadRequest("Invalid token.");
           
            var command = new GetBalanceQuery(userInfo.UserId, userInfo.AccountId);

            var result = await _mediator.Send(command);

            if (result is null) return BadRequest("No balance found");
            
            return Ok(result);
        }
    }
}