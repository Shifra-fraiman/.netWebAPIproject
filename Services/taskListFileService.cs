using System;
using System.Text.Json;
using project.interfaces;
using project.Models;

namespace project.Services
{
    public class taskListFileService: ItaskListService
    {
            List<myTask> ?tasks { get; }
            private string filePath { get; set; }

        public taskListFileService(IWebHostEnvironment webHost){
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "taskList.json");
            using (var jsonFile =File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<myTask>>(jsonFile.ReadToEnd(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
 private void saveToFile(){
        File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
    }
    public void Delete(int id)
    {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            saveToFile();
    }

    public myTask Get(int id)
    {
        myTask? t = tasks.FirstOrDefault(t => t.Id == id);
        if (t == null)
            return new myTask() ;
        else
            return t;
    }

    public List<myTask> GetAll() => tasks;
    
    public int Post(myTask newTask)
    {
        int newId = tasks.Max(t => t.Id);
        newTask.Id= newId+1;
        tasks.Add(newTask);
            saveToFile();
        return newTask.Id;
    }

    public void Put(int id, myTask newTask)
    {
        if (id == newTask.Id)
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                int index = tasks.IndexOf(task);
                tasks[index] =newTask;
            }
        }
            saveToFile();
    }
    
   
    }
}
