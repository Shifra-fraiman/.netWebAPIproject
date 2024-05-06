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
[Authorize(Policy ="User")]
public class taskListController : ControllerBase
{
    // private List<myTask> list;
    private ItaskListService taskListService { get; set; }
    public taskListController(ItaskListService taskListService){
        this.taskListService = taskListService;
    }

     [HttpGet]
    public ActionResult<IEnumerable<myTask>> Get()
    {
        return taskListService.GetAll().ToList();
    }
 

    [HttpGet("{id}")]
    public ActionResult<myTask> Get(int id)
    {
        myTask task= taskListService.Get(id);
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public ActionResult Post(myTask newTask)
    {
        var newId= taskListService.Post(newTask);  
        return CreatedAtAction(nameof(Post), new {id=newId}, newTask);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, myTask newTask)
    {
        taskListService.Put(id, newTask);
        return Ok();
    }
        
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    { 
        taskListService.Delete(id);
        return Ok();
    }


}