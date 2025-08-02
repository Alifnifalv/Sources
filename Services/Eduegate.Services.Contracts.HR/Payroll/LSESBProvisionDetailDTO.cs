using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class LSESBProvisionDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long? EmployeeLSProvisionIID { get; set; }
        [DataMember]
        public System.DateTime? EntryDate { get; set; }
        
        [DataMember]
        public long? EmployeeESBProvisionIID { get; set; }

        [DataMember]
        public DateTime? LSSettlementLastDate { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }
        [DataMember]
        public int? DepartmentID { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public DateTime? DOJ { get; set; }

        [DataMember]
        public int? LSDays { get; set; }
        [DataMember]
        public int? ESBDays { get; set; }
        [DataMember]
        public decimal? LeaveSalary { get; set; }
        [DataMember]
        public decimal? ESB { get; set; }
        [DataMember]
        public decimal? Basic { get; set; }

    }
}
