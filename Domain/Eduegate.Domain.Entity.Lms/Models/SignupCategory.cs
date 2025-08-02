using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("SignupCategories", Schema = "signup")]
    public partial class SignupCategory
    {
        public SignupCategory()
        {
            Signups = new HashSet<Signup>();
        }

        [Key]
        public byte SignupCategoryID { get; set; }

        [StringLength(100)]
        public string SignupCategoryName { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }

    }
}