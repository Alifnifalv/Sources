using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SignupCategory")]
        public virtual ICollection<Signup> Signups { get; set; }
    }
}
