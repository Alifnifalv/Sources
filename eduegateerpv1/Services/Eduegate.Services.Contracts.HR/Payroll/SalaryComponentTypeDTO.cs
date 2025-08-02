using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
   public class SalaryComponentTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte SalaryComponentTypeID { get; set; }

        [DataMember]
        public string TypeName { get; set; }
    }
}
