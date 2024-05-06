using project.Models;

namespace project.interfaces;

public interface IUserService{
    User Get(int id);
    List<User> GetAll();
    int Post(User user);
    void Put(int id, User user);
    void Delete(int id);
}