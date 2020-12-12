namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands.Command;
    using DataObject.Expense.Request;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class ExpenseController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(IMediator mediator, ILogger<ExpenseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("user/{userId}/account")]
        public async Task<IActionResult> CreateExpense(Guid userId, [FromBody] CreateExpenseRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId))
                return BadRequest("Invalid token.");
                
            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateExpenseCommand(userInfo.UserId, userInfo.AccountId, request.Description, request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/expense/{result}", new {id = result});
        }
        
        [HttpPut("user/{userId}/account/{expenseId}")]
        public async Task<IActionResult> EditExpense(Guid userId, Guid expenseId, [FromBody] CreateExpenseRequest request)
        {
            var userInfo = this.GetUserInfo();

            if (!userId.Equals(userInfo.UserId))
                return BadRequest("Invalid token.");
            
            if (request is null)
                return BadRequest("Request is null");

            var command = new CreateExpenseCommand(userInfo.UserId, userInfo.AccountId, request.Description, request.Value,
                request.Category, request.Type);

            var result = await _mediator.Send(command);

            return Created($"user/{userId}/expense/{result}", new {id = result});
        }
    }
}