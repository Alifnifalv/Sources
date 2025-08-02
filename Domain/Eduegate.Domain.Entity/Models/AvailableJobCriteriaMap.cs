using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AvailableJobCriteriaMaps", Schema = "hr")]
    public partial class AvailableJobCriteriaMap
    {
        [Key]
        public long CriteriaID { get; set; }
        public long? JobID { get; set; }
        public int? TypeID { get; set; }
        public byte? QualificationID { get; set; }
        public string FieldOfStudy { get; set; }
        public virtual AvailableJob Job { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual JobCriteriaType Type { get; set; }
    }
}
