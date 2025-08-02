using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalaryStructureComponentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public SalaryStructureComponentDTO()
        {
            SalaryComponent = new KeyValueDTO();
        }

        [DataMember]
        public long SalaryStructureComponentMapIID { get; set; }

       

        [DataMember]
        public int? SalaryComponentID { get; set; }

        [DataMember]
        public KeyValueDTO SalaryComponent { get; set; }

        [DataMember]
        public long? SalaryStructureID { get; set; }

        [DataMember]
        public decimal? MinAmount { get; set; }
        
        [DataMember]
        public decimal? MaxAmount { get; set; }

        [DataMember]
        public string Formula { get; set; }
    }
}
