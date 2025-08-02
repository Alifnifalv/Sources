using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class EmploymentRequestDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public string NAME { get; set; }

        [DataMember]
        public string PASSPORT_NO { get; set; }
        [DataMember]
        public KeyValueDTO Nationality { get; set; }

        [DataMember]
        public KeyValueDTO PayComp { get; set; }
        [DataMember]
        public KeyValueDTO Shop { get; set; }

        [DataMember]
        public KeyValueDTO Group { get; set; }
        [DataMember]
        public KeyValueDTO MainDesignation { get; set; }
        [DataMember]
        public KeyValueDTO Designation { get; set; }

        [DataMember]
        public KeyValueDTO ProductiveType { get; set; }

        [DataMember]
        public KeyValueDTO EmploymentType { get; set; }

        [DataMember]
        public KeyValueDTO RecruitmentType { get; set; }

        [DataMember]
        public bool StaffCar { get; set; }

        [DataMember]
        public decimal BasicSalary { get; set; }

        [DataMember]
        public string Reason_Basic { get; set; }

        //[DataMember]
        //public decimal? ProposedIncrease { get; set; }

        //[DataMember]
        //public KeyValueDTO SalaryChangeAfterPeriod { get; set; }
        [DataMember]
        public string RecuritRemark { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public string AlternativeEmailID { get; set; }

        [DataMember]
        public string EmpPersonalRemarks { get; set; }


        [DataMember]
        public List<EmployementAllowanceDTO> Allowance { get; set; }

        [DataMember]
        public List<EmploymentProposedIncreaseDTO> ProposedIncrease { get; set; }

        [DataMember]
        public int? EMP_REQ_NO { get; set; }

        [DataMember]
        public int? EMP_NO { get; set; }

        [DataMember]
        public string F_Name { get; set; }


        [DataMember]
        public string M_Name { get; set; }


        [DataMember]
        public string L_Name { get; set; }


        [DataMember]
        public DateTime? DOB { get; set; }

        [DataMember]
        public DateTime? PassportIssueDate { get; set; }

        [DataMember]
        public DateTime? PassportExpiryDate { get; set; }

        [DataMember]
        public KeyValueDTO Gender { get; set; }

        [DataMember]
        public KeyValueDTO Location { get; set; }

        [DataMember]
        public string CIVILID { get; set; }

        [DataMember]
        public string CRE_WEBUSER { get; set; }

        [DataMember]
        public string CRE_IP { get; set; }

        [DataMember]
        public string CRE_BY { get; set; }

        [DataMember]
        public DateTime? CRE_DT { get; set; }

        [DataMember]
        public DateTime? REQ_DT { get; set; }
        
        [DataMember]
        public bool? isbudgeted { get; set; }

        [DataMember]
        public KeyValueDTO replacedEmployee { get; set; }

        [DataMember]

        public KeyValueDTO Marital_Status { get; set; }

        [DataMember]
        public KeyValueDTO DocType { get; set; }

        [DataMember]
        public bool? isNewRequest { get; set; }

        [DataMember]
        public List<DocumentFileDTO> documents { get; set; }

        [DataMember]
        public KeyValueDTO Agent { get; set; }

        [DataMember]

        public KeyValueDTO ContractType { get; set; }

        [DataMember]

        public int? Period { get; set; }

        [DataMember]
        public string Photo { get; set; }

        [DataMember]
        public string FieldNameToValidate { get;set;}

        [DataMember]
        public KeyValueDTO VisaCompany { get; set; }

        [DataMember]
        public KeyValueDTO QuotaType { get; set; }

        [DataMember]
        public KeyValueDTO EmpProcessRequestStatus { get; set; }

        [DataMember]
        public KeyValueDTO EmpRequestStatus { get; set; }

        [DataMember]
        public string PersonalRemarks { get; set; }
    }
}
