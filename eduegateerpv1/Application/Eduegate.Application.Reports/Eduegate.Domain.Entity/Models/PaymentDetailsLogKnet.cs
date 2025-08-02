using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentDetailsLogKnet
    {
        public int LogID { get; set; }
        public string CustomerSessionID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> TrackID { get; set; }
        public Nullable<long> TrackKey { get; set; }
        public Nullable<long> PaymentID { get; set; }
        public Nullable<long> TransID { get; set; }
        public string TransResult { get; set; }
        public string TransPostDate { get; set; }
        public string TransAuth { get; set; }
        public string TransRef { get; set; }
    }
}
