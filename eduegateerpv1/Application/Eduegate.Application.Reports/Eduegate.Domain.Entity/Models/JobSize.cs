using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public class JobSize
    {
        public JobSize()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public Int16 JobSizeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
    }
}
