using Task_Manager.Models;

namespace Task_Manager.Data
{
    public static  class AppData
    {
        public static List<User> users = new List<User>()
        {
            new User {Id = 1, Name = "suhail aslam" , Password = "12345" , Role = "Admin"},
            new User {Id = 2, Name = "Nisar falily" , Password = "11111" , Role = "User"},
            new User {Id = 3, Name = "shakkeera" , Password = "22222" , Role = "User"},
            new User {Id = 4, Name = "safuvan" , Password = "33333" , Role = "User"},
            new User {Id = 5, Name = "ihsan" , Password = "44444" , Role = "User"},
            new User {Id = 6, Name = "salman faris" , Password = "55555" , Role = "User"},
            new User {Id = 7, Name = "thabsheer" , Password = "66666" , Role = "User"}

        };

        public static List<TaskModel> tasks = new List<TaskModel>();

    }
}
