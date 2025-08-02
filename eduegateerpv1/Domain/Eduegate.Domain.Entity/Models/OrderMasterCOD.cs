using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderMasterCOD
    {
        [Key]
        public long OrderMasterCodID { get; set; }
        public long RefOrderID { get; set; }
        public string CollectionRvNo { get; set; }
        public decimal CollectionAmount { get; set; }
        public short CollectionUserID { get; set; }
        public System.DateTime CollectionDateTime { get; set; }
        public Nullable<decimal> ReceivedAmount { get; set; }
        public Nullable<short> ReceiverUserID { get; set; }
        public Nullable<System.DateTime> ReceivedDateTime { get; set; }
    }
}
