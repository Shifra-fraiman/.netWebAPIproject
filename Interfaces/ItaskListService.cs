using project.Models;

namespace project.interfaces
{
    public interface ItaskListService{
        List<myTask> GetAll(int userId);
        myTask Get(int id);
        int Post(myTask newTask, int userId);
        void Put(int id, myTask newTask);
        void Delete(int id);
    }
    
}