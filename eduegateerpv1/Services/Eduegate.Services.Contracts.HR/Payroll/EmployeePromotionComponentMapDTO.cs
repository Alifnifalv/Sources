using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
   public class EmployeePromotionComponentMapDTO
    {
        public EmployeePromotionComponentMapDTO()
        {
            SalaryComponent = new KeyValueDTO();
        }
        [DataMember]
        public long EmployeePromotionSalaryComponentMapIID { get; set; }

        [DataMember]
        public long? EmployeePromotionID { get; set; }

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
    }
}
