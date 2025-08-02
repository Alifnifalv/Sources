using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeDueFeeTypeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeDueFeeTypeMapDTO()
        {
            FeeMaster = new KeyValueDTO();
            FeePeriod = new KeyValueDTO();
            FeeStructureFeeMap = new KeyValueDTO();
            FeeDueMonthlySplit = new List<FeeDueMonthlySplitDTO>();
        }

        [DataMember]
        public long FeeDueFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        //[DataMember]
        //public long? ClassFeeMasterID { get; set; }

        [DataMember]
        public long? FeeStructureFeeMapID { get; set; }

        [DataMember]
        public long? CreditNoteFeeTypeMapID { get; set; }

        [DataMember]
        public long? FeeCollectionFeeTypeMapsID { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public bool? IsExternal { get; set; }

        [DataMember]
        public string ReportName { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? TaxPercentage { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public decimal? TaxAmount { get; set; }

        [DataMember]
        public bool? FeeCollectionStatus { get; set; }
        [DataMember]
        public int? FeeMasterID { get; set; }

        //[DataMember]
        //public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        //[DataMember]
        //public KeyValueDTO FeeMasterClassMap { get; set; }

        [DataMember]
        public KeyValueDTO FeeStructureFeeMap { get; set; }

        [DataMember]
        public KeyValueDTO ClassFeeMaster { get; set; }

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }

        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public byte? FeeCycleID { get; set; }

        [DataMember]
        public virtual List<FeeDueMonthlySplitDTO> FeeDueMonthlySplit { get; set; }

        [DataMember]
        public int? FineMasterID { get; set; }

        [DataMember]
        public long? FineMasterStudentMapID { get; set; }

        [DataMember]
        public bool? IsRefundable { get; set; }

        [DataMember]
        public decimal? RefundAmount { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public long? AccountTransactionHeadID { get; set; }

        [DataMember]
        public bool? IsMandatoryToPay { get; set; }

        #region Mobile app use
        [DataMember]
        public decimal? NowPaying { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public bool IsPayingNow { get; set; }

        [DataMember]
        public string InvoiceStringDate { get; set; }
        #endregion
    }
}