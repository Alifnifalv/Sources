using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    public class SalaryStructureScaleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        public SalaryStructureScaleDTO()
        {

        }
        [DataMember]
        public long StructureScaleID { get; set; }
        [DataMember]
        public bool? IsSponsored { get; set; }
        [DataMember]
        public decimal? MinAmount { get; set; }
        [DataMember]
        public decimal? MaxAmount { get; set; }
        [DataMember]
        public int? AccomodationTypeID { get; set; }
        [DataMember]
        public string IncrementNote { get; set; }
        [DataMember]
        public int? MaritalStatusID { get; set; }
        [DataMember]
        public string LeaveTicket { get; set; }
        [DataMember]
        public long? SalaryStructureID { get; set; }

    }
}
