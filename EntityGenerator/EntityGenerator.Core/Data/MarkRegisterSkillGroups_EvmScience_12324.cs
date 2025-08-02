using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MarkRegisterSkillGroups_EvmScience_12324
    {
        public long? MarkRegisterSkillGroupIID { get; set; }
        public long? MarkRegisterSubjectMapID { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MinimumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MaximumMark { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? MarkObtained { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? SkillGroupMasterID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsPassed { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? ClassSubjectSkillGroupMapID { get; set; }
        public int? SubjectID { get; set; }
    }
}
