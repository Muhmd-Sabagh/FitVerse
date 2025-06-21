using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role Name is required.")]
        [Display(Name ="Role Name")]
        [StringLength(50, ErrorMessage = "Role Name cannot exceed 50 characters.")]
        public string RoleName { get; set; }
    }
}
