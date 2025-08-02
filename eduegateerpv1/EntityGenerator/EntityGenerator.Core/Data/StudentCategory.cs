using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentCategories", Schema = "schools")]
    public partial class StudentCategory
    {
        public StudentCategory()
        {
            EventAudienceMaps = new HashSet<EventAudienceMap>();
            StudentApplications = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
        }

        [Key]
        public int StudentCategoryID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("StudentCategory")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
        [InverseProperty("StudentCategory")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("StudentCategory")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
