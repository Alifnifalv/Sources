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
    public class EmployeeSalaryStructureComponentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public EmployeeSalaryStructureComponentMapDTO()
        {
            SalaryComponent = new KeyValueDTO();
            EmployeeSalaryStructureVariableMap = new List<EmployeeSalaryStructureVariableDTO>();
        }
        [DataMember]
        public long EmployeeSalaryStructureComponentMapIID { get; set; }

        [DataMember]
        public long? EmployeeSalaryStructureID { get; set; }            

        [DataMember]
        public int? SalaryComponentID { get; set; }

        [DataMember]
        public KeyValueDTO SalaryComponent { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? Deduction { get; set; }

        [DataMember]
        public decimal? Earnings { get; set; }

        [DataMember]
        public string Formula { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
        [DataMember]
        public decimal? NoOfDays { get; set; }
        [DataMember]
        public List<EmployeeSalaryStructureVariableDTO> EmployeeSalaryStructureVariableMap { get; set; }
    }
}
