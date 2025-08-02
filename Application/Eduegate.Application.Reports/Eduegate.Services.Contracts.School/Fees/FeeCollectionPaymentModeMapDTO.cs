using System;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionPaymentModeMapDTO : BaseMasterDTO
    {
        public FeeCollectionPaymentModeMapDTO()
        {
            PaymentMode = new KeyValueDTO();
        }
        [DataMember]
        public long FeeCollectionPaymentModeMapIID { get; set; }

        [DataMember]
        public long? FeeCollectionID { get; set; }

        [DataMember]
        public int? PaymentModeID { get; set; }

        [DataMember]
        public int? BankID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public DateTime? TDate { get; set; }

        [DataMember]
        public string ReferenceNo { get; set; }

        [DataMember]
        public long? AccountId { get; set; }

        [DataMember]
        public KeyValueDTO PaymentMode { get; set; }

        //[DataMember]
        //public FeeCollection FeeCollection { get; set; }

        //[DataMember]
        //public FeePaymentMode FeePaymentMode { get; set; }
    }
}