using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
   public class EditSalaryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public EditSalaryDTO()
        {
            
            SalaryComponent = new List<SalarySlipComponentDTO>();
        }

        [DataMember]
        public long SalarySlipIID { get; set; }

        [DataMember]
        public DateTime? SlipDate { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public int? SalaryComponentID { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

      

        [DataMember]
        public string EmployeeName { get; set; }
       
        [DataMember]
        public List<SalarySlipComponentDTO> SalaryComponent { get; set; }

    }
}
