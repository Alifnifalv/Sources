using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public partial class EmployeeLSProvisionDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeLSProvisionDetailIID { get; set; }
        [DataMember]
        public long EmployeeLSProvisionHeadID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string EntryNumber { get; set; }
        [DataMember]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [DataMember]
        public decimal? BasicSalary { get; set; }
        [DataMember]
        public decimal? NoofDaysWorked { get; set; }
        [DataMember]
        public decimal? LeaveSalaryAmount { get; set; }
        [DataMember]
        public decimal? NoofLeaveSalaryDays { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public DateTime? DOJ { get; set; }
        [DataMember]
        public decimal? OpeningAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }
       
    }
}
