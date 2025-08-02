using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("MarkRegisterSubjectMaps", Schema = "schools")]
    [Index("MarkRegisterID", Name = "IDX_MarkRegisterSubjectMaps_MarkRegisterID")]
    [Index("MarksGradeMapID", Name = "IDX_MarkRegisterSubjectMaps_MarksGradeMapID_MarkRegisterID__SubjectID__Mark__CreatedBy__UpdatedBy__")]
    public partial class MarkRegisterSubjectMap
    {
        public MarkRegisterSubjectMap()
        {
            MarkRegisterSkillGroups = new HashSet<MarkRegisterSkillGroup>();
        }

        [Key]
        public long MarkRegisterSubjectMapIID { get; set; }
        public long? MarkRegisterID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarksGradeMapID { get; set; }
        public bool? IsPassed { get; set; }
        public byte? MarkEntryStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMark { get; set; }

        [ForeignKey("MarkEntryStatusID")]
        [InverseProperty("MarkRegisterSubjectMaps")]
        public virtual MarkEntryStatus MarkEntryStatus { get; set; }
        [ForeignKey("MarkRegisterID")]
        [InverseProperty("MarkRegisterSubjectMaps")]
        public virtual MarkRegister MarkRegister { get; set; }
        [ForeignKey("MarksGradeMapID")]
        [InverseProperty("MarkRegisterSubjectMaps")]
        public virtual MarkGradeMap MarksGradeMap { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("MarkRegisterSubjectMaps")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("MarkRegisterSubjectMap")]
        public virtual ICollection<MarkRegisterSkillGroup> MarkRegisterSkillGroups { get; set; }
    }
}
