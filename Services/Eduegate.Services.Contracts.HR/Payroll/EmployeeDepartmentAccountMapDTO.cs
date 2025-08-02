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
    public class EmployeeDepartmentAccountMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeDepartmentAccountMapDTO()
        {
            EmployeeDeptAccountMapDetailDTO = new List<EmployeeDeptAccountMapDetailDTO>();
        }
        [DataMember]
        public long EmployeeDepartmentAccountMaplIID { get; set; }
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public KeyValueDTO SalaryComponent { get; set; }
        [DataMember]
        public List<EmployeeDeptAccountMapDetailDTO> EmployeeDeptAccountMapDetailDTO { get; set; }
}
}
