using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CommunicationLogs", Schema = "marketing")]
    public partial class CommunicationLog
    {
        [Key]
        public long CommunicationLogIID { get; set; }
        public long? LoginID { get; set; }
        public byte? CommunicationTypeID { get; set; }
        public byte? CommunicationStatusID { get; set; }

        [ForeignKey("CommunicationStatusID")]
        [InverseProperty("CommunicationLogs")]
        public virtual CommunicationStatus CommunicationStatus { get; set; }
        [ForeignKey("CommunicationTypeID")]
        [InverseProperty("CommunicationLogs")]
        public virtual CommunicationType1 CommunicationType { get; set; }
        [ForeignKey("LoginID")]
        [InverseProperty("CommunicationLogs")]
        public virtual Login Login { get; set; }
    }
}
