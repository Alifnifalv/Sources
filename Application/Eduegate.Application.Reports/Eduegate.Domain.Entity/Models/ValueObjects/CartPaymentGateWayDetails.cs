using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
  public  class CartPaymentGateWayDetails
    {
        public int PaymentGateWayID { get; set; }
        public long CartIID { get; set; }
        public int CartStatusID { get; set; }
        public long TrackKey { get; set; }
        public string PaymentGateWay { get; set; }
    }
}
