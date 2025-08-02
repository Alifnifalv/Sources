using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class UserManagementDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public decimal UserIID { get; set; }

        [DataMember]
        public Nullable<int> RoleID { get; set; }

        [DataMember]
        public Nullable<short> TitleIID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string LoginEmailID { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }

        [DataMember]
        public Nullable<decimal> StatusID { get; set; }
    }
}
