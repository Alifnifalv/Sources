using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeDeptAccountMapDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long EmployeeDepartmentAccountMaplIID { get; set; }
       
        [DataMember]
        public long? DepartmentID { get; set; }
        [DataMember]
        public long? StaffLedgerAccountID { get; set; }
        [DataMember]
        public long? ProvisionLedgerAccountID { get; set; }
        [DataMember]
        public long? ExpenseLedgerAccountID { get; set; }
        [DataMember]
        public long? TaxLedgerAccountID { get; set; }
        [DataMember]
        public KeyValueDTO Departments { get; set; }
        [DataMember]
        public KeyValueDTO ExpenseLedgerAccount { get; set; }
        [DataMember]
        public KeyValueDTO ProvisionLedgerAccount { get; set; }
        
        [DataMember]
        public KeyValueDTO StaffLedgerAccount { get; set; }
        [DataMember]
        public KeyValueDTO TaxLedgerAccount { get; set; }
      
    }
}
