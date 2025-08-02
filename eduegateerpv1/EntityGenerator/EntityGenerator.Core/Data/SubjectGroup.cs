using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubjectGroups", Schema = "schools")]
    public partial class SubjectGroup
    {
        public SubjectGroup()
        {
            SubjectGroupSubjectMaps = new HashSet<SubjectGroupSubjectMap>();
        }

        [Key]
        public byte SubjectGroupID { get; set; }
        [StringLength(50)]
        public string SubjectGroupName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("SubjectGroup")]
        public virtual ICollection<SubjectGroupSubjectMap> SubjectGroupSubjectMaps { get; set; }
    }
}
