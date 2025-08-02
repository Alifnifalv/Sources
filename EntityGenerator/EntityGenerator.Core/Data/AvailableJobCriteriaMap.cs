using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AvailableJobCriteriaMaps", Schema = "hr")]
    public partial class AvailableJobCriteriaMap
    {
        [Key]
        public long CriteriaID { get; set; }
        public long? JobID { get; set; }
        public int? TypeID { get; set; }
        public byte? QualificationID { get; set; }
        [StringLength(250)]
        public string FieldOfStudy { get; set; }

        [ForeignKey("JobID")]
        [InverseProperty("AvailableJobCriteriaMaps")]
        public virtual AvailableJob Job { get; set; }
        [ForeignKey("QualificationID")]
        [InverseProperty("AvailableJobCriteriaMaps")]
        public virtual Qualification Qualification { get; set; }
        [ForeignKey("TypeID")]
        [InverseProperty("AvailableJobCriteriaMaps")]
        public virtual JobCriteriaType Type { get; set; }
    }
}
