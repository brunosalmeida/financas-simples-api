using System;
using System.Threading.Tasks;
using FS.Domain.Core.Interfaces;
using FS.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FS.Api.Controllers
{
    [ApiController]
    [Route("v1/seed")]
    public class SeedController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<SeedController> _logger;

        public SeedController(IUserRepository userRepository, ILogger<SeedController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await CreateAdmin();
            return Ok("Done");
        }

        private async Task CreateAdmin()
        {
            var user = new User("Admin", "admin@financassimples.com", "1234567890");
            await _userRepository.Insert(user);
        }
    }
}