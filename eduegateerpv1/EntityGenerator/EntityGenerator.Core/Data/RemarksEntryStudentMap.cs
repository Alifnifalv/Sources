using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RemarksEntryStudentMaps", Schema = "schools")]
    [Index("RemarksEntryID", Name = "idx_RemarksEntryStudentMapsRemarksEntryID")]
    [Index("StudentID", Name = "idx_RemarksEntryStudentMaps_StudentID")]
    public partial class RemarksEntryStudentMap
    {
        public RemarksEntryStudentMap()
        {
            RemarksEntryExamMaps = new HashSet<RemarksEntryExamMap>();
        }

        [Key]
        public long RemarksEntryStudentMapIID { get; set; }
        public long? RemarksEntryID { get; set; }
        public long? StudentID { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }

        [ForeignKey("RemarksEntryID")]
        [InverseProperty("RemarksEntryStudentMaps")]
        public virtual RemarksEntry RemarksEntry { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("RemarksEntryStudentMaps")]
        public virtual Student Student { get; set; }
        [InverseProperty("RemarksEntryStudentMap")]
        public virtual ICollection<RemarksEntryExamMap> RemarksEntryExamMaps { get; set; }
    }
}
