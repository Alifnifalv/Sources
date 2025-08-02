using System;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
   public  class FinancialYearClosingProcDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
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
