using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ApplicationSubmitType", Schema = "mutual")]
    public partial class ApplicationSubmitType
    {
        public ApplicationSubmitType()
        {
            StudentApplications = new HashSet<StudentApplication>();
        }

        [Key]
        public int SubmitTypeID { get; set; }
        [StringLength(50)]
        public string SubmitTypeName { get; set; }

        [InverseProperty("ApplicationType")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
    }
}
