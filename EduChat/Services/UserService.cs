using EduChat.Models;

namespace EduChat.Services
{
   public class UserService
   {
        public string UserName { get; set; }

        public static User LoggedInUser { get; set; }


    }
}
