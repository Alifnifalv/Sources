namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.CommunicationLogs")]
    public partial class CommunicationLog
    {
        [Key]
        public long CommunicationLogIID { get; set; }

        public long? LoginID { get; set; }

        public byte? CommunicationTypeID { get; set; }

        public byte? CommunicationStatusID { get; set; }

        public virtual Login Login { get; set; }

        public virtual CommunicationStatus CommunicationStatus { get; set; }

        public virtual CommunicationTypes1 CommunicationTypes1 { get; set; }
    }
}
