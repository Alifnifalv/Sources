using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class VoucherDTO
    {
        [DataMember]
        public string VoucherValue { get; set; }

        [DataMember]
        public string VoucherPriceUnit { get; set; }

        [DataMember]
        public bool IsVoucherValid { get; set; }

        [DataMember]
        public Nullable<decimal> CurrentBalance { get; set; }

        [DataMember]
        public Status Status { get; set; }

        [DataMember]
        public string VoucherMessage { get; set;}

    }
}
