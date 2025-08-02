using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Subscription
    {
        public long SubscribeIID { get; set; }
        public string SubscribeEmail { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string VarificationCode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
