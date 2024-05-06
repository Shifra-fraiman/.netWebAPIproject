using System;
using System.Text.Json;
using project.interfaces;
using project.Models;

namespace project.Services
{
    public class UserService: IUserService
    {
            List<User> ?users { get; }
            private string filePath { get; set; }

        public UserService(IWebHostEnvironment webHost){
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "user.json");
            using (var jsonFile =File.OpenText(filePath))
            {
                this.users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(), 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    private void saveToFile(){
        File.WriteAllText(filePath, JsonSerializer.Serialize(users));
    }
    public void Delete(int id)
    {
            var task = users!.Find(t => t.Id == id);
            if (task != null)
            {
                users.Remove(task);
            }
            saveToFile();
    }

    public User Get(int id)
    {
        User? u = users!.FirstOrDefault(t => t.Id == id);
        if (u == null)
            return new User() ;
        else
            return u;
    }

    public List<User> GetAll() => users!;
    
    public int Post(User user)
    {
        int newId =users!=null? users.Max(t => t.Id) : 0;
        user.Id= newId+1;
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
                users![index] =user;
            }
        }
            saveToFile();
    }

    }
}
