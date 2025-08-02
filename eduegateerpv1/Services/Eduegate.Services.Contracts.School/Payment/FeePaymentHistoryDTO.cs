using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Payment
{
    [DataContract]
    public class FeePaymentHistoryDTO : BaseMasterDTO
    {
        public FeePaymentHistoryDTO()
        {
            StudentHistories = new List<FeePaymentHistoryStudentDTO>();
        }

        [DataMember]
        public long FeeCollectionIID { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public string TransactionNumber { get; set; }

        [DataMember]
        public DateTime? CollectionDate { get; set; }

        [DataMember]
        public string CollectionDateString { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public int? FeeCollectionStatusID { get; set; }

        [DataMember]
        public string FeeCollectionStatus { get; set; }

        [DataMember]
        public int? FeeCollectionDraftStatusID { get; set; }

        [DataMember]
        public int? FeeCollectionCollectedStatusID { get; set; }

        [DataMember]
        public string ParentEmailID { get; set; }

        [DataMember]
        public int? FeePaymentModeID { get; set; }

        [DataMember]
        public string FeePaymentMode { get; set; }

        [DataMember]
        public bool? IsEnableRetry { get; set; }

        [DataMember]
        public List<FeePaymentHistoryStudentDTO> StudentHistories { get; set; }
    }
}