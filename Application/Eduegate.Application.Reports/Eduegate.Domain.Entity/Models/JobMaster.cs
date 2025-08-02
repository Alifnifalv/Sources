using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class JobMaster
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public int JobPosition { get; set; }
        public bool Active { get; set; }
    }
}
