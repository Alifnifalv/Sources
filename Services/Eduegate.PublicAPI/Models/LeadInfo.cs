using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eduegate.PublicAPI.Models
{
    public class LeadInfo
    {
        [Required]
        [StringLength(100)]
        public string Sname { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Dob1 { get; set; }
        [Required]
        public string Academic_year { get; set; }
        [Required]
        public string Grade { get; set; }
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
        public string Referal_code { get; set; }
      
        public string SchoolID { get; set; }

        public string Nationality { get; set; }
    }
}