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
    public  class SalaryComponentDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public SalaryComponentDTO()
        {
            SalaryComponentRelationMap = new List<SalaryComponentRelationMapDTO>();

        }

        [DataMember]
        public int SalaryComponentID { get; set; }

        [DataMember]
        public byte? ComponentTypeID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Abbreviation { get; set; }

        [DataMember]
        public byte? SalaryComponentGroupID { get; set; }
        [DataMember]
        public string ReportHeadGroup { get; set; }
        [DataMember]
        public int? ReportHeadGroupID { get; set; }

        [DataMember]
        public int? NoOfDaysApplicable { get; set; }
        [DataMember]
        public long? ProvisionLedgerAccountID { get; set; }
        [DataMember]
        public long? StaffLedgerAccountID { get; set; }
        [DataMember]
        public long? ExpenseLedgerAccountID { get; set; }
        [DataMember]
        public KeyValueDTO ProvisionLedgerAccount { get; set; }
        [DataMember]
        public KeyValueDTO ExpenseLedgerAccount { get; set; }
        [DataMember]
        public KeyValueDTO StaffLedgerAccount { get; set; }
        [DataMember]
        public List<SalaryComponentRelationMapDTO> SalaryComponentRelationMap { get; set; }
    }
}
