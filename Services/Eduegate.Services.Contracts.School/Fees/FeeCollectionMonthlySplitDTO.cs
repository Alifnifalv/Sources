using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionMonthlySplitDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long FeeCollectionMonthlySplitIID { get; set; }

        [DataMember]
        public long FeeCollectionFeeTypeMapId { get; set; }

        [DataMember]
        public long? FeeDueMonthlySplitID { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public int MonthID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public decimal? RefundAmount { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public decimal? PrvCollect { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public decimal? ReceivableAmount { get; set; }

        //[DataMember]
        //public  FeeCollectionFeeTypeDTO FeeCollectionFeeTypeDTO { get; set; }

        //[DataMember]
        //public  FeeDueMonthlySplitDTO FeeDueMonthlySplitDTO { get; set; }

        #region For fee collection history
        [DataMember]
        public decimal? DueAmount { get; set; }
        #endregion

        #region Mobile app use
        [DataMember]
        public string MonthName { get; set; }
        #endregion


        #region For CampusTransfer Feedue --Start
        [DataMember]
        public long CampusTransferMonthlySplitIID { get; set; }

        [DataMember]
        public decimal? FromCampusDue { get; set; }

        [DataMember]
        public decimal? ToCampusDue { get; set; }

        [DataMember]
        public decimal? PayableAmount { get; set; }

        [DataMember]
        public decimal? RecievableAmount { get; set; } 


        #endregion --End
    }
}