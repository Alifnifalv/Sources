namespace Eduegate.Domain.Entity
{
    using Eduegate.Domain.Entity.School.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentSkillMasterMaps", Schema = "schools")]
    public partial class StudentSkillMasterMap
    {
        [Key]
        public long StudentSkillMasterMapIID { get; set; }

        public long StudentSkillGroupMapsID { get; set; }

        public int? SkillMasterID { get; set; }

        public decimal? MarksObtained { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? MarksGradeMapID { get; set; }

        public int? SkillGroupMasterID { get; set; }

        public decimal? MinimumMark { get; set; }

        public decimal? MaximumMark { get; set; }

        public virtual MarkGradeMap MarkGradeMap { get; set; }

        public virtual SkillGroupMaster SkillGroupMaster { get; set; }

        public virtual SkillMaster SkillMaster { get; set; }

        public virtual StudentSkillGroupMap StudentSkillGroupMap { get; set; }
    }
}
