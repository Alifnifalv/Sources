using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class EmployeeSalaryStructureVariableDTO
    {
        [DataMember]
        public long EmployeeSalaryStructureVariableMapIID { get; set; }
        [DataMember]
        public long? EmployeeSalaryStructureComponentMapID { get; set; }
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public string VariableKey { get; set; }
        [DataMember]
        public string VariableValue { get; set; }       
    }
}
