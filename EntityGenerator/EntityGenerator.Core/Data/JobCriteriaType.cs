using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(250)]
        public string Description { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<AvailableJobCriteriaMap> AvailableJobCriteriaMaps { get; set; }
    }
}
