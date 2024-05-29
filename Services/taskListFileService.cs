using System;
using System.Text.Json;
using project.interfaces;
using project.Models;

namespace project.Services
{
    public class taskListFileService : ItaskListService
    {
        List<myTask>? tasks { get; }
        private string filePath { get; set; }
        //private readonly IUserService _userService;

        public taskListFileService(IWebHostEnvironment webHost/*, IUserService userService*/)
        {
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "taskList.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<myTask>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
          //  _userService = userService;
        }
        private void saveToFile()
        {
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
                return new myTask();
            else
                return t;
        }

        public List<myTask> GetAll(int userId)
        {
            //User user= _userService.GetAll().FirstOrDefault(u=> u.Name == name&& u.Password == password);
            //return tasks.FindAll(t => t.Id==user.Id) ;
            return tasks.Where(t => t.userId == userId).ToList();
        }

        public int Post(myTask newTask, int userId)
        {
            int newId = tasks.Max(t => t.Id);
            newTask.Id = newId + 1;
            newTask.userId = userId;
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
                    newTask.userId = task.userId;
                    int index = tasks.IndexOf(task);
                    tasks[index] = newTask;
                }
            }
            saveToFile();
        }


    }
}
