using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeDueMonthlySplitDTO
    {
        [DataMember]
        public long FeeDueMonthlySplitIID { get; set; }

        [DataMember]
        public long FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public long? FeeCollectionMonthlySplitID { get; set; }

        [DataMember]
        public long FeeStructureMontlySplitMapID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public int MonthID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public decimal? TotalAmount { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public bool FeeCollectionStatus { get; set; }

        [DataMember]
        public decimal? RefundAmount { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? OldBalance { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public bool? IsRefundable { get; set; }

        #region Mobile app use
        [DataMember]
        public decimal? CreditNote { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public decimal? PrvCollect { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }
     

        [DataMember]
        public decimal? OldNowPaying { get; set; }

        [DataMember]
        public long? FeeDueMonthlySplitID { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public string MonthName { get; set; }
        #endregion
    }
}