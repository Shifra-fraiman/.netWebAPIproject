using Microsoft.AspNetCore.Mvc;
using project.interfaces;
using System.Collections.Generic;
using project.Models;
using project.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace project.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] User User)
        {
            System.Console.WriteLine("LoginController!");
            string token = _loginService.Login(User);
            return new OkObjectResult(token);;
        }


       
    }
}