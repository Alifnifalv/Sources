using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Budgeting
{
    [DataContract]
    public class BudgetEntryAccountMapDTO
    {
        public BudgetEntryAccountMapDTO()
        {
            //AccountList = new List<KeyValueDTO>();
        }

        [DataMember]
        public long BudgetEntryAccountMapIID { get; set; }

        [DataMember]
        public long? BudgetEntryID { get; set; }

        [DataMember]
        public int? GroupID { get; set; }

        [DataMember]
        public long? AccountID { get; set; }

        [DataMember]
        public string AccountName { get; set; }    
        
        [DataMember]
        public KeyValueDTO Account { get; set; }     
        
        [DataMember]
        public KeyValueDTO Group { get; set; }

        [DataMember]
        public string Remarks { get; set; }
        [DataMember]
        public string GroupDefaultSide { get; set; }

        //[DataMember]
        //public List<KeyValueDTO> AccountList { get; set; }
    }
}