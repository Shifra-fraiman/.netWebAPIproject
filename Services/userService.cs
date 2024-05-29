using System;
using System.Text.Json;
using project.interfaces;
using project.Models;

namespace project.Services
{
    public class UserService : IUserService
    {
        List<User>? users { get; }
        private string filePath { get; set; }
        private readonly ItaskListService _itaskListService;
        public UserService(IWebHostEnvironment webHost, ItaskListService itaskListService )
        {
             _itaskListService = itaskListService;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "user.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                this.users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }
        public void Delete(int id/*, ItaskListService taskListService*/)
        {
            var user = users!.Find(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
            }
            saveToFile();
            //מחיקת כל המשימות
            List<myTask> tasks = _itaskListService.GetAll(id).ToList();
            tasks.ForEach(task => _itaskListService.Delete(task.Id));  
        }

        public User Get(int id)
        {
            User? u = users!.FirstOrDefault(t => t.Id == id);
            if (u == null)
                return new User();
            else
                return u;
        }

        public List<User> GetAll()
        {
            System.Console.WriteLine("in getAll users");
            return users;
        }

        public int Post(User user)
        {
            int newId = users != null ? users.Max(t => t.Id) : 0;
            user.Id = newId + 1;
            users!.Add(user);
            saveToFile();
            return user.Id;
        }

        public void Put(int id, User user)
        {
            if (id == user.Id)
            {
                var task = users!.Find(t => t.Id == id);
                if (task != null)
                {
                    int index = users!.IndexOf(task);
                    users![index] = user;
                }
            }
            saveToFile();
        }

    }
}
