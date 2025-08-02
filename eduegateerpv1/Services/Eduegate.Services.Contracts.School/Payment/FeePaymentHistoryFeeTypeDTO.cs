using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Payment
{
    [DataContract]
    public class FeePaymentHistoryFeeTypeDTO : BaseMasterDTO
    {
        public FeePaymentHistoryFeeTypeDTO()
        {
            IsFeePeriodDisabled = true;
            IsRowSelected = true;
            FeePeriod = new KeyValueDTO();
            FeeMaster = new KeyValueDTO();
            FeeMonthly = new List<FeePaymentHistoryMonthlySplitDTO>();
        }

        [DataMember]
        public long FeeCollectionIID { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public bool? IsRowSelected { get; set; }

        [DataMember]
        public string FeeReceiptNo { get; set; }

        [DataMember]
        public DateTime? CollectionDate { get; set; }

        [DataMember]
        public string CollectionDateString { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public KeyValueDTO FeeMaster { get; set; }

        [DataMember]
        public int? FeePeriodID { get; set; }

        [DataMember]
        public KeyValueDTO FeePeriod { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public bool IsFeePeriodDisabled { get; set; }

        [DataMember]
        public byte? FeeCycleID { get; set; }

        [DataMember]
        public long FeeCollectionFeeTypeMapsIID { get; set; }

        [DataMember]
        public long? FeeDueFeeTypeMapsID { get; set; }

        [DataMember]
        public long? StudentFeeDueID { get; set; }

        [DataMember]
        public long? FeeMasterClassMapID { get; set; }

        [DataMember]
        public int? FeeCollectionStatusID { get; set; }

        [DataMember]
        public string FeeCollectionStatus { get; set; }

        [DataMember]
        public int? FeeCollectionDraftStatusID { get; set; }

        [DataMember]
        public int? FeeCollectionCollectedStatusID { get; set; }

        public List<FeePaymentHistoryMonthlySplitDTO> FeeMonthly { get; set; }
    }
}