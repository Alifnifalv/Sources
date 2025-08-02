using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionPreviousFeesDTO : BaseMasterDTO
    {
        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FEECollectionID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? FeeStructureFeeMapID { get; set; }

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

        //[DataMember]
        //public bool? IsRowSelected { get; set; }

        [DataMember]
        public decimal? RefundAmount { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public List<FeeCollectionMonthlySplitDTO> MontlySplitMaps { get; set; }
    }
}