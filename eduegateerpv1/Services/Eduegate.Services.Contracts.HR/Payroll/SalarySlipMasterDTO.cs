using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
   public class SalarySlipMasterDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public SalarySlipMasterDTO()
        {
            SalarySlipEmployee = new List<SalarySlipEmployeeDTO>();
        }

        //[DataMember]
        //public long? SchoolID { get; set; }

        [DataMember]
        public int Month { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int? DepartmentID { get; set; }

        [DataMember]
        public List<SalarySlipEmployeeDTO> SalarySlipEmployee { get; set; }

    }
}
