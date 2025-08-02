using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
  public  class OnlineEnquiryViewModel
    {
        [Required]
        [StringLength(100)]
        public string Sname { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Dob1 { get; set; }
        [Required]
        public KeyValueViewModel Academic_year { get; set; }
        [Required]
        public KeyValueViewModel Grade { get; set; }
        [Required]
        [StringLength(100)]
        public string Pname { get; set; }
        [Required]
        [StringLength(40)]
        public string Mobile { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public KeyValueViewModel Referal_code { get; set; }
        [Required]
        public string Dob { get; set; }
    }
}
