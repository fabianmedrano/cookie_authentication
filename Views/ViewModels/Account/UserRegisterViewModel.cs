using Microsoft.AspNetCore.Mvc;

namespace cookie_authentication.Views.ViewModels.Account
{
    public class UserRegisterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
      
        public string Username { get; set; }
    }
}
