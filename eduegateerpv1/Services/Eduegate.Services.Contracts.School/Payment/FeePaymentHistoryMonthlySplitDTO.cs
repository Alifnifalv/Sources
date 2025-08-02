using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Payment
{
    [DataContract]
    public class FeePaymentHistoryMonthlySplitDTO : BaseMasterDTO
    {
        public FeePaymentHistoryMonthlySplitDTO()
        {
        }

        [DataMember]
        public long MapIID { get; set; }

        [DataMember]
        public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public long? FeeDueMonthlySplitID { get; set; }

        [DataMember]
        public long? FeeCollectionFeeTypeMapId { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public bool? FeeCollectionStatus { get; set; }

        [DataMember]
        public long? ParentID { get; set; }

        [DataMember]
        public int MonthID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public string MonthName { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TotalAmount { get; set; }

        [DataMember]
        public decimal? CreditNote { get; set; }

        [DataMember]
        public decimal? PrvCollect { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public decimal? NowPayingOld { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public decimal? OldNowPaying { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }
    }
}