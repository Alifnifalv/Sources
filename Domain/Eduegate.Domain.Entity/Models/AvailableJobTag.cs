using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AvailableJobTags", Schema = "hr")]
    public partial class AvailableJobTag
    {
        [Key]
        public long AvailableJobTagIID { get; set; }
        public long? JobID { get; set; }
        public string TagName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual AvailableJob Job { get; set; }
    }
}
