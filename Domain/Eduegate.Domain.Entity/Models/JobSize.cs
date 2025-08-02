using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobSizes", Schema = "jobs")]
    public class JobSize
    {
        public JobSize()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        [Key]
        public Int16 JobSizeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
