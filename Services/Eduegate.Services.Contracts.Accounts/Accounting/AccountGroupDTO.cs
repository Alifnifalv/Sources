using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
     [DataContract]
    public class AccountGroupDTO : BaseMasterDTO
    {
        public AccountGroupDTO()
        {
            ParentGroup = new KeyValueDTO();
        }

        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public int? AccountGroupID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string GroupCode { get; set; }

        [DataMember]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DataMember]
        public int? Parent_ID { get; set; }

        [DataMember]
        public KeyValueDTO ParentGroup { get; set; }

        [DataMember]
        public int? Affect_ID { get; set; }

        [DataMember]
        public string Default_Side { get; set; }
    }
}
