using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.CommonDTO
{  
    public class OperationResultWithIDsDTO : OperationResultDTO
    {
       
        [DataMember]
        public List<long> IDList { get; set; }
        [DataMember]
        public List<SalarySlipDTO> SalarySlipIDList { get; set; }

        [DataMember]
        public EmployeeSettlementDTO EmployeeSettlementDTO { get; set; }

    }
}
