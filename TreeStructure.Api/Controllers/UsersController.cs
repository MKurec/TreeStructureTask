using TreeStructure.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStructure.Infrastructure.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TreeStructure.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : DefaultController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetAccountAsync(UserId));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            return Json(await _userService.LoginAsync(command.Name, command.Password));
        }


    }
}
