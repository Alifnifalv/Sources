using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR.Payroll
{
    [DataContract]
    public  class SalaryComponentRelationMapDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public long SalaryComponentRelationMapIID { get; set; }
        [DataMember]
        public int? SalaryComponentID { get; set; }
        [DataMember]
        public int? RelatedComponentID { get; set; }
        [DataMember]
        public short? RelationTypeID { get; set; }
        [DataMember]
        public int? NoOfDaysApplicable { get; set; }
    }
}
