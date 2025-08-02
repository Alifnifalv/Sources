using Eduegate.Domain.Entity.Models;
using System;
using System.Runtime.Serialization;

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
        public Eduegate.Infrastructure.Enums.VoucherStatus Status { get; set; }

        [DataMember]
        public string VoucherMessage { get; set;}

    }
}