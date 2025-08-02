using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentGroupType", Schema = "schools")]
    public partial class StudentGroupType
    {
        public StudentGroupType()
        {
            StudentGroups = new HashSet<StudentGroup>();
        }

        [Key]
        public int GroupTypeIID { get; set; }
        [StringLength(35)]
        public string GroupTypeName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("GroupType")]
        public virtual ICollection<StudentGroup> StudentGroups { get; set; }
    }
}
