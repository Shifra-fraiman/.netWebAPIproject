using project.interfaces;
using project.Services;

namespace project.Utilities
{
  public static class Utilities
  {
    //extention method
    public static void AddTask(this IServiceCollection services)
    {
      services.AddSingleton<ItaskListService, taskListFileService>();
    }
    //extention method
    public static void AddUser(this IServiceCollection services)
    {
      services.AddSingleton<IUserService, UserService>();
    }

    //extention method
    public static void AddLogin(this IServiceCollection services)
    {
      services.AddSingleton<ILoginService, LoginService>();
    }

  }
}