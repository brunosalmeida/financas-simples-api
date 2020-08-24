using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using FS.Api.Queries.Request;
using MediatR;

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
        public async Task<IActionResult> Get(Guid id)
        {
            var request = new GetUserQuery {Id =id};

            var result = await _mediator.Send(request);

            return Ok(result);
        }
        
        
    }
}