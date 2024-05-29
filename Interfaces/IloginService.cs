using Microsoft.AspNetCore.Mvc;
using project.Models;

namespace project.interfaces
{
    public interface ILoginService
    {
        string Login(User user);
    }
}    