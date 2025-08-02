using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.MyAccountViewModel
{
   public class CutomerBaseViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} cannot be longer than {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} cannot be longer than {1} characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [StringLength(20, ErrorMessage = "Invalid number", MinimumLength = 6)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }


        [Display(Name = "I would like to get email updates from blink.com.kw")]
        public bool EmailUpdates { get; set; }
    }
}
