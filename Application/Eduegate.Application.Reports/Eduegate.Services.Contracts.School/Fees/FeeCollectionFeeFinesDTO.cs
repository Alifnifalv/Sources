using System;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionFeeFinesDTO : BaseMasterDTO
    {
        [DataMember]
        public int? FineMasterID { get; set; }

        [DataMember]
        public string FineName { get; set; }

        [DataMember]
        public long? FineMasterStudentMapID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FEECollectionID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public decimal? PrvCollect { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }
    }
}