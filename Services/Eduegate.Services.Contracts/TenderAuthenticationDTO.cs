using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class TenderAuthenticationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public TenderAuthenticationDTO() {
            Employee = new KeyValueDTO();
        }

        [DataMember]
        public long AuthenticationID { get; set; }
        [DataMember]
        public long? TenderID { get; set; }
        [DataMember]
        public long? LoginID { get; set; }
        [DataMember]
        public long? EmployeeID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public long TenderAuthMapIID { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PasswordHint { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public string Password { get; set; } 

        [DataMember]
        public string OldPassword { get; set; } 
        
        [DataMember]
        public string OldPasswordSalt { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public bool? IsApprover { get; set; } 

        [DataMember]
        public bool? IsError { get; set; }

        [DataMember]
        public string ReturnMessage { get; set; }

        //Log Data

        [DataMember]
        public bool? IsTenderOpened { get; set; }
        
        [DataMember]
        public bool? IsTenderApproved { get; set; }

        [DataMember]
        public string OpenedDateString { get; set; }

        [DataMember]
        public string OpenedTime { get; set; }

        [DataMember]
        public int? NumOfAuthorities { get; set; }
    }
}