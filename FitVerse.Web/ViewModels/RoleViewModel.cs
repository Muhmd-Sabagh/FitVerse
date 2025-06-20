using System.ComponentModel.DataAnnotations;

namespace FitVerse.Web.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
