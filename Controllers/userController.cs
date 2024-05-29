using Microsoft.AspNetCore.Mvc;
using project.interfaces;
using System.Collections.Generic;
using project.Models;
using project.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace project.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;



    public UserController(IUserService userService)
    {
        _userService = userService;

    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public ActionResult<IEnumerable<User>> Get()
    {
        System.Console.WriteLine("get users");
        return _userService.GetAll().ToList();
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<User> Get(int id)
    {
        User user = _userService.Get(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post([FromBody] User newUser)
    {
        System.Console.WriteLine("Post!");
        var newId = _userService.Post(newUser);
        return CreatedAtAction(nameof(Post), new { id = newId }, newUser);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, User newUser)
    {
        _userService.Put(id, newUser);
        return Ok("The user updated!");
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok("The user deleted!");
    }

}
