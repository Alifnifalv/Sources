using System;
using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionFeeTypeDTO : BaseMasterDTO
    {
        public FeeCollectionFeeTypeDTO()
        {
            MontlySplitMaps = new List<FeeCollectionMonthlySplitDTO>();
        }

        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FEECollectionID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? FeeStructureFeeMapID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public byte? FeeCycleID { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }
       
        [DataMember]
        public string FeeMaster { get; set; }

        [DataMember]
        public string FeePeriod { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public decimal? DueAmount { get; set; }

        [DataMember]
        public decimal? RefundAmount { get; set; }

        [DataMember]
        public bool? IsRefundable { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public decimal? ReceivableAmount { get; set; }

        [DataMember]
        public decimal? PrvCollect { get; set; }

        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public List<FeeCollectionMonthlySplitDTO> MontlySplitMaps { get; set; }


        #region For CampusTransfer Feedue --Start

        [DataMember]
        public long CampusTransferFeeTypeMapsIID { get; set; }

        [DataMember]
        public decimal? FromCampusDue { get; set; }

        [DataMember]
        public decimal? ToCampusDue { get; set; }

        [DataMember]
        public decimal? PayableAmount { get; set; }

        [DataMember]
        public long? FeeDueMonthlySplitIID { get; set; }

        [DataMember]
        public int? MonthID { get; set; }

        [DataMember]
        public string MonthName { get; set; }

        [DataMember]
        public string InvoiceDateString { get; set; }

        [DataMember]
        public int? FeeTypeID { get; set; }

        [DataMember]
        public bool? IsTutionFee { get; set; }

        [DataMember]
        public bool? IsTransportFee { get; set; }

        [DataMember]
        public decimal? FeeDueDifference { get; set; }

        #endregion --End
    }
}