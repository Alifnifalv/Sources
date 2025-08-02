using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketActionDetailDetailMaps", Schema = "cs")]
    public partial class TicketActionDetailDetailMap
    {
        [Key]
        public long TicketActionDetailDetailMapIID { get; set; }

        public long TicketActionDetailMapID { get; set; }

        public int? Notify { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] Timestamps { get; set; }

        public virtual TicketActionDetailMap TicketActionDetailMap { get; set; }
    }
}