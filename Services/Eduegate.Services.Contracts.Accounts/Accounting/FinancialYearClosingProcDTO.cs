using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
   public  class FinancialYearClosingProcDTO : BaseMasterDTO
    {
        [DataMember]
        public int Main_Group_ID { get; set; }

        [DataMember]
        public string Main_GroupName { get; set; }

        [DataMember]
        public int Sub_Group_ID { get; set; }

        [DataMember]
        public string Sub_GroupName { get; set; }

        //[DataMember]
        //public string AccountName { get; set; }

        [DataMember]
        public DateTime? AuditedDate { get; set; }

        [DataMember]
        public int? OrderNo { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public int Group_ID { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        public string AccountName { get; set; }

    }
}
