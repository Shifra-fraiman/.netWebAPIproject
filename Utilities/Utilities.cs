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
    }
}