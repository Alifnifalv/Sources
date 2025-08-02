using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransportApplicationStatuses", Schema = "schools")]
    public partial class TransportApplicationStatus
    {
        public TransportApplicationStatus()
        {
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
        }

        [Key]
        public byte TransportApplcnStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("TransportApplcnStatus")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
    }
}
