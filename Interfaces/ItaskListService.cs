using project.Models;

namespace project.interfaces
{
    public interface ItaskListService{
        List<myTask> GetAll();
        myTask Get(int id);
        int Post(myTask newTask);
        void Put(int id, myTask newTask);
        void Delete(int id);
    }
    
}