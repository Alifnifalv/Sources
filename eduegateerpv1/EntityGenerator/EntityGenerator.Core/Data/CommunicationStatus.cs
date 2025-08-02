using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CommunicationStatuses", Schema = "marketing")]
    public partial class CommunicationStatus
    {
        public CommunicationStatus()
        {
            CommunicationLogs = new HashSet<CommunicationLog>();
        }

        [Key]
        public byte CommunicationStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("CommunicationStatus")]
        public virtual ICollection<CommunicationLog> CommunicationLogs { get; set; }
    }
}
