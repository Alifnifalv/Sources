using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("AvailableJobTags", Schema = "hr")]
    public partial class AvailableJobTag
    {
        [Key]
        public long AvailableJobTagIID { get; set; }
        public long? JobID { get; set; }
        public string TagName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual AvailableJob AvailableJobs { get; set; }
    }
}
