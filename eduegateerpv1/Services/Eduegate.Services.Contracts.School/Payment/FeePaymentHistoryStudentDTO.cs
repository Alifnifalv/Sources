using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Payment
{
    [DataContract]
    public class FeePaymentHistoryStudentDTO : BaseMasterDTO
    {
        public FeePaymentHistoryStudentDTO()
        {
            FeeCollectionTypeHistories = new List<FeePaymentHistoryFeeTypeDTO>();
        }

        [DataMember]
        public long FeeCollectionIID { get; set; }

        [DataMember]
        public bool IsExpand { get; set; }

        [DataMember]
        public string FeeReceiptNo { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public DateTime? CollectionDate { get; set; }

        [DataMember]
        public string CollectionDateString { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public int? FeeCollectionStatusID { get; set; }

        [DataMember]
        public string FeeCollectionStatus { get; set; }

        [DataMember]
        public int? FeeCollectionDraftStatusID { get; set; }

        [DataMember]
        public int? FeeCollectionCollectedStatusID { get; set; }

        [DataMember]
        public string TransactionNumber { get; set; }

        [DataMember]
        public int? FeePaymentModeID { get; set; }

        [DataMember]
        public string FeePaymentMode { get; set; }

        [DataMember]
        public bool? IsEnableRetry { get; set; }

        [DataMember]
        public List<FeePaymentHistoryFeeTypeDTO> FeeCollectionTypeHistories { get; set; }
    }
}