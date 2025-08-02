using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class FeeCollectionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public FeeCollectionDTO()
        {
            FeeTypes = new List<FeeCollectionFeeTypeDTO>();
            FeeFines = new List<FeeCollectionFeeFinesDTO>();
            FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
            FeeCollectionPreviousFeesDTO = new List<FeeCollectionPreviousFeesDTO>();
            FeeCollectionPendingInvoiceDTO =new List<FeeCollectionPendingInvoiceDTO>();
        }

        [DataMember]
        public long FeeCollectionIID { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public bool? IsAutoFill { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public long? ClassFeeMasterId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string AdmissionNo { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public DateTime? CollectionDate { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? DiscountAmount { get; set; }

        [DataMember]
        public decimal? FineAmount { get; set; }

        [DataMember]
        public decimal? PaidAmount { get; set; }

        [DataMember]
        public bool? IsPaid { get; set; }

        [DataMember]
        public string FeeReceiptNo { get; set; }

        [DataMember]
        public byte? FeeReceiptID { get; set; }

        [DataMember]
        public int? FeePaymentModeID { get; set; }

        [DataMember]
        public string FeePaymentMode { get; set; }

        [DataMember]
        public string ClassFeeMaster { get; set; }

        [DataMember]
        public long? CashierID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public KeyValueDTO CashierEmployee { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public string SiblingFeeInfo { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public int? FeeCollectionStatusID { get; set; }

        [DataMember]
        public string FeeCollectionStatusName { get; set; }

        [DataMember]
        public List<FeeCollectionFeeTypeDTO> FeeTypes { get; set; }

        [DataMember]
        public List<FeeCollectionFeeFinesDTO> FeeFines { get; set; }

        [DataMember]
        public List<FeeCollectionPaymentModeMapDTO> FeeCollectionPaymentModeMapDTO { get; set; }

        [DataMember]
        public List<FeeCollectionPreviousFeesDTO> FeeCollectionPreviousFeesDTO { get; set; }
        [DataMember]
        public List<FeeCollectionPendingInvoiceDTO> FeeCollectionPendingInvoiceDTO { get; set; }

        //For feehistory
        [DataMember]
        public int? FeeCollectionDraftStatusID { get; set; }

        [DataMember]
        public int? FeeCollectionCollectedStatusID { get; set; }

        [DataMember]
        public string SchoolName { get; set; }

        [DataMember]
        public string GroupTransactionNumber { get; set; }

        [DataMember]
        public string TransactionHeadID { get; set; }

        [DataMember]
        public string ReportName { get; set; }

        [DataMember]
        public bool IsOnlineStore { get; set; }

        [DataMember]
        public string StudentClassCode { get; set; }

        [DataMember]
        public string StudentSchoolShortName { get; set; }

        [DataMember]
        public long? PaymentTrackID { get; set; }

        [DataMember]
        public bool? IsEnableRetry { get; set; }

        #region Mobile app use
        [DataMember]
        public string CollectionStringDate { get; set; }
        #endregion
    }
}