using Microsoft.AspNetCore.Mvc;
using project.interfaces;
using System.Collections.Generic;
using project.Models;
using project.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace project.Controllers
{
// [Authorize]
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("[action]")]
    public ActionResult<String> Login([FromBody] User User)
    {
        List<User> users = _userService.GetAll();
        User user = users.FirstOrDefault(user => user.Name == User.Name && user.Password == User.Password);

        if (user == null)
        {
            return Unauthorized();
        }
        System.Console.WriteLine($"User: {user.Name} Login: {user.Password} Admin: {user.IsAdmin}");
        var claims = new List<Claim>
            {
                new Claim("type", "User"),
                new Claim("userId", user.Id.ToString())
            };
        if (user.IsAdmin)
        {
            System.Console.WriteLine("I am Admin");
            claims.Add(new Claim("type", "Admin"));
        }

        System.Console.WriteLine("I in Login");
        var token = TokenService.GetToken(claims);
        return new OkObjectResult(TokenService.WriteToken(token));
    }
    // [HttpPost]
    // [Route("[action]")]
    // [Authorize(Policy = "Admin")]
    // public ActionResult Post([FromBody] User newUser)
    // {
    //     System.Console.WriteLine("Post!");
    //     var newId = _userService.Post(newUser);
    //     return CreatedAtAction(nameof(Post), new { id = newId }, newUser);
    // }

    // [HttpPost]
    // [Route("[action]")]
    // [Authorize(Policy = "Admin")]
    // public IActionResult create([FromBody] Agent Agent)
    // {
    //     System.Console.WriteLine("GenerateBadge");
    //     var claims = new List<Claim>
    //         {
    //             new Claim("type", "Agent"),
    //             new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
    //         };
    //     System.Console.WriteLine("claim: " + claims);

    //     var token = FbiTokenService.GetToken(claims);

    //     return new OkObjectResult(FbiTokenService.WriteToken(token));
    // }
}}