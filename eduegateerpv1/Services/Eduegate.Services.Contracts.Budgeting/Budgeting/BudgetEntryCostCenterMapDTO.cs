using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class BudgetEntryCostCenterMapDTO
    {
        [DataMember]
        public long BudgetEntryCostCenterMapIID { get; set; }

        [DataMember]
        public long? BudgetEntryID { get; set; }

        [DataMember]
        public int? CostCenterID { get; set; }
      
    }
}