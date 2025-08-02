using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CommunicationTypes", Schema = "marketing")]
    public partial class CommunicationType1
    {
        public CommunicationType1()
        {
            CommunicationLogs = new HashSet<CommunicationLog>();
        }

        [Key]
        public byte CommunicationTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("CommunicationType")]
        public virtual ICollection<CommunicationLog> CommunicationLogs { get; set; }
    }
}
