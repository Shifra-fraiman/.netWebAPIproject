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
[Authorize(Policy = "User")]
public class taskListController : ControllerBase
{
    // private List<myTask> list;
    private ItaskListService taskListService { get; set; }
    private int userId;
    public taskListController(ItaskListService taskListService, IHttpContextAccessor httpContextAccessor)
    {
        this.taskListService = taskListService;
        this.userId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
    }

    [HttpGet]
    public ActionResult<IEnumerable<myTask>> Get()
    {
        return taskListService.GetAll(userId).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<myTask> Get(int id)
    {
        myTask task = taskListService.Get(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public ActionResult Post(myTask newTask)
    {
        var newId = taskListService.Post(newTask, userId);
        return CreatedAtAction(nameof(Post), new { id = newId }, newTask);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, myTask newTask)
    {
        taskListService.Put(id, newTask);
        return Ok("The task update!");
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        taskListService.Delete(id);
        return Ok("The task deleted!");
    }

}