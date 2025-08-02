namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketActionDetailDetailMaps")]
    public partial class TicketActionDetailDetailMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TicketActionDetailDetailMapIID { get; set; }

        public long TicketActionDetailMapID { get; set; }

        public int? Notify { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timestamps { get; set; }

        public virtual TicketActionDetailMap TicketActionDetailMap { get; set; }
    }
}
