using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TicketProductMaps", Schema = "cs")]
    public partial class TicketProductMap
    {
        [Key]
        public long TicketProductMapIID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<Int16> ReasonID { get; set; }
        public string Narration { get; set; }
        public Nullable<long> TicketID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual TicketReason TicketReason { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
