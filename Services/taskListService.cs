using project.interfaces;
using project.Models;

namespace project.Services;
public class taskListService : ItaskListService
{
    private List<myTask> list;

        public taskListService(){
            list= new List<myTask>{
                new myTask {Id=1, Description="home work in java", IsDoing=false},
                new myTask  {Id=2, Description="angular project", IsDoing=false},
                new myTask  {Id=3, Description="home work in core", IsDoing=true}
            };
        }

    public void Delete(int id)
    {

            var task = list.Find(t => t.Id == id);
            if (task != null)
            {
                list.Remove(task);
            }

    }

    public myTask Get(int id)
    {
        myTask? t = list.FirstOrDefault(t => t.Id == id);
        if (t == null)
            return new myTask() ;
        else
            return t;
    }

    public List<myTask> GetAll() => list;
    
    public int Post(myTask newTask)
    {
        int newId = list.Max(t => t.Id);
        newTask.Id= newId+1;
        list.Add(newTask);
        return newTask.Id;
    }

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
}