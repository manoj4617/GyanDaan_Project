using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class LoginViewModel
    {
     
         [Required(ErrorMessage = "Email is required")]
         public string Email { get; set; }

         [Required(ErrorMessage = "Password is required"), RegularExpression(pattern: "^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$@%&? \"]).*$", ErrorMessage = "Minimum 8 characters, must contain a digit, a special character, and a capital letter")]
         public string Password { get; set; }
        
    }
}
