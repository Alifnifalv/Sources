using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
     [DataContract]
    public class ChartOfAccountDetailDTO : BaseMasterDTO
    {
        [DataMember]
        public long ChartOfAccountMapIID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
        [DataMember]
        public string AccountCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Nullable<int> IncomeOrBalanceID { get; set; }
        [DataMember]
        public Nullable<int> ChartRowTypeID { get; set; }
        [DataMember]
        public string Total { get; set; }
        [DataMember]
        public string NetChange { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public Nullable<int> NoOfBlankLines { get; set; }
        [DataMember]
        public Nullable<bool> IsNewPage { get; set; }
        [DataMember]
        public long AccountGroupID { get; set; }
        [DataMember]
        public string AccountGroupName { get; set; }
        [DataMember]
        public int? ParentID { get; set; }
        [DataMember]
        public string LevelSort { get; set; }
        [DataMember]
        public int? Level { get; set; }

    }
}
