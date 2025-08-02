using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            this.TransactionHeads = new List<TransactionHead>();
        }

        public short DeliveryTypeID { get; set; }
        public string DeliveryMethod { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> DeliveryCost { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
