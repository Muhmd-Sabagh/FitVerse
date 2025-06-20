using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitVerse.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        // IdentityUser already provides Id, UserName, Email, PhoneNumber, PasswordHash etc.

        // Custom properties
        public string FullName { get; set; } = string.Empty;

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

        public ApplicationUser()
        {
            CartItems = new HashSet<CartItem>();
            Orders = new HashSet<Order>();
        }
    }
}
