using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobCriteriaTypes", Schema = "hr")]
    public partial class JobCriteriaType
    {
        public JobCriteriaType()
        {
            AvailableJobCriteriaMaps = new HashSet<AvailableJobCriteriaMap>();
        }

        [Key]
        public int TypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AvailableJobCriteriaMap> AvailableJobCriteriaMaps { get; set; }
    }
}
