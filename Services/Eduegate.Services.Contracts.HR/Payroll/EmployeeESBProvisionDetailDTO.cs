using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public partial class EmployeeESBProvisionDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EmployeeESBProvisionDetailIID { get; set; }
        [DataMember]
        public long EmployeeESBProvisionHeadID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string EntryNumber { get; set; }
        [DataMember]
        public decimal? ESBAmount { get; set; }
        [DataMember]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public decimal? BasicSalary { get; set; }
        [DataMember]
        public DateTime? DOJ { get; set; }

        [DataMember]
        public decimal? NoofESBDays { get; set; }
        [DataMember]
        public decimal? NoofDaysWorked { get; set; }
        [DataMember]
        public decimal? OpeningAmount { get; set; }

        [DataMember]
        public decimal? Balance { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }
      
    }
}
