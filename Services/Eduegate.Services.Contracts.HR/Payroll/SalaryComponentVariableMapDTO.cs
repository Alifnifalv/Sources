using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public class SalaryComponentVariableMapDTO
    {
        [DataMember]
        public long SalaryComponentVariableMapIID { get; set; }
        [DataMember]
        public long? SalaryStructureComponentMapID { get; set; }
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public string VariableKey { get; set; }
        [DataMember]
        public string VariableValue { get; set; }
        [DataMember]
        public virtual KeyValueDTO SalaryComponent { get; set; }
    }
}
