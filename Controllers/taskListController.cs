using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
using project.Models;

namespace project.Controllers;

[ApiController]
[Route("[controller]")]
public class taskListController : ControllerBase
{
    private List<myTask> list;

    public taskListController(){
        list= new List<myTask>{
            new myTask {Id=1, Description="home work in java", IsDoing=false},
            new myTask  {Id=2, Description="angular project", IsDoing=false},
            new myTask  {Id=3, Description="home work in core", IsDoing=true}
        };
    }

     [HttpGet]
    public IEnumerable<myTask> Get()
    {
        return list;
    }
    [HttpGet("{id}")]
    public myTask Get(int id)
    {
        return list.FirstOrDefault(t => t.Id == id);
    }

    [HttpPost]
    public int Post(myTask newTask)
    {
        int newId = list.Max(t => t.Id);
        newTask.Id= newId+1;
        list.Add(newTask);
        return newTask.Id;
    }

    [HttpPut("{id}")]
    public void Put(int id, myTask newTask)
    {
        if (id == newTask.Id)
        {
            var task = list.Find(t => t.Id == id);
            if (task != null)
            {
                int index = list.IndexOf(task);
                list[index] =newTask;
            }
        }
    }
        
    [HttpDelete("{id}")]
    public void Delete(int id)
    {

            var task = list.Find(t => t.Id == id);
            if (task != null)
            {
                list.Remove(task);
            }

    }


}