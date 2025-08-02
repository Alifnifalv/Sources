using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class EmployeeBankDetailDTO : BaseMasterDTO
    {
        [DataMember]
        public long EmployeeBankIID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public long? BankID { get; set; }

        [DataMember]
        public string EmpBankName { get; set; }

        [DataMember]
        public string IBAN { get; set; }

        [DataMember]
        public string SwiftCode { get; set; }

        [DataMember]
        public string AccountHolderName { get; set; }

        [DataMember]
        public string BankDetails { get; set; }
    }
}
