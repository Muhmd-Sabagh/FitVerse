using Microsoft.AspNetCore.Identity;

namespace FitVerse.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public static implicit operator ApplicationUser(UserManager<ApplicationUser> v)
        {
            throw new NotImplementedException();
        }
    }
}
