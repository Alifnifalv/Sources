using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ApplicationStatuses", Schema = "schools")]
    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
            StudentApplications = new HashSet<StudentApplication>();
        }

        [Key]
        public byte ApplicationStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("ApplicationStatus")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
    }
}
