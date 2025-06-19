using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")] //Must two Prop IS Equaled
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
