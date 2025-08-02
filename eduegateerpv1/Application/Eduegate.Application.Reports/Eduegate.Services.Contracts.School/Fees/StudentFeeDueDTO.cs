
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.School.Fees
{
    [DataContract]
    public class StudentFeeDueDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public StudentFeeDueDTO()
        {
            Class = new KeyValueDTO();
            Student = new List<KeyValueDTO>();
            FeePeriod = new List<KeyValueDTO>();
            FeeDueFeeTypeMap = new List<FeeDueFeeTypeMapDTO>();
            FeeFineMap = new List<FeeDueFeeFineMapDTO>();
            FeeMaster = new List<KeyValueDTO>();
            FeeInvoiceList = new List<KeyValueDTO>();
        }

        [DataMember]
        public long StudentFeeDueIID { get; set; }

        [DataMember]
        public int? ClassId { get; set; }

        [DataMember]
        public int? SectionId { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public string FeeReceiptNo { get; set; }

        [DataMember]
        public bool IsAccountPost { get; set; }

        [DataMember]
        public bool IsAccountPostEdit { get; set; }

        [DataMember]
        public bool CollectionStatusEdit { get; set; }

        [DataMember]
        public DateTime? CollectionDate { get; set; }

        [DataMember]
        public long? StudentId { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public string InvoiceNo { get; set; }

        [DataMember]
        public bool CollectionStatus { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public List<KeyValueDTO> Student { get; set; }

        [DataMember]
        public List<KeyValueDTO> FeePeriod { get; set; }

        [DataMember]
        public List<KeyValueDTO> FeeMaster { get; set; }

        [DataMember]
        public List<KeyValueDTO> FineMaster { get; set; }

        [DataMember]
        public List<KeyValueDTO> Parents { get; set; }

        [DataMember]
        public List<KeyValueDTO> ClassMaster { get; set; }

        [DataMember]
        public List<KeyValueDTO> SectionMaster { get; set; }

        [DataMember]
        public List<FeeDueFeeTypeMapDTO> FeeDueFeeTypeMap { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public decimal? CreditNoteAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public decimal? CollectedAmount { get; set; }

        [DataMember]
        public string SectionName { get; set; }

        [DataMember]
        public List<FeeDueFeeFineMapDTO> FeeFineMap { get; set; }

        [DataMember]
        public List<FeeCollectionPreviousFeesDTO> PreviousFees { get; set; }
        
        [DataMember]
        public KeyValueDTO AcademicYear { get; set; }

        [DataMember]
        public int? AcadamicYearID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int DocumentTypeID { get; set; }

        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int? FeePeriodId { get; set; }

        [DataMember]
        public decimal? FeeMasterAmount { get; set; }

        [DataMember]
        public string ActCode { get; set; } = "1";

        [DataMember]
        public string ActDescription { get; set; } = "Fee Due";

        [DataMember]
        public int ResposeCode { get; set; } = 1;

        [DataMember]
        public string ResposeDescription { get; set; } = "Fee Due";

        [DataMember]
        public bool? IsCancel { get; set; }

        [DataMember]
        public bool? IsExternal { get; set; }

        [DataMember]
        public string ReportName { get; set; }


        //ForCreditNoteScreeGridLookup
        [DataMember]
        public List<KeyValueDTO> FeeInvoiceList { get; set; }
    }
}
