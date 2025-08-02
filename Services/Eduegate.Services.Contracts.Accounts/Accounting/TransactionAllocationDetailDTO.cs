using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    [DataContract]
    public partial class TransactionAllocationDetailDTO : BaseMasterDTO
    {
        [DataMember]
        public long TransactionAllocationDetailIID { get; set; }
        [DataMember]
        public long TransactionAllocationID { get; set; }
        [DataMember]
        public long? AccountTransactionHeadID { get; set; }
        [DataMember]
        public long? Ref_TH_ID { get; set; }
        [DataMember]
        public int? Ref_SlNo { get; set; }
        [DataMember]
        public int? CostCenterID { get; set; }
        [DataMember]
        public int? DepartmentID { get; set; }
        [DataMember]
        public int? BudgetID { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public long? GL_AccountID { get; set; }
        [DataMember]
        public long? SL_AccountID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public decimal? TaxAmount { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public  KeyValueDTO Account { get; set; }
        [DataMember]
        public KeyValueDTO CostCenter { get; set; }
        [DataMember]
        public KeyValueDTO GL_Account { get; set; }
       
    }
}
