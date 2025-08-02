using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class LoginDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LoginIID { get; set; }

        [DataMember]
        public string LoginUserID { get; set; }

        [DataMember]
        public string LoginEmailID { get; set; }

        [DataMember]
        public string ProfileFile { get; set; }

        [DataMember]
        public string PasswordSalt { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string PasswordHint { get; set; }

        [DataMember]
        public Nullable<bool> RequirePasswordReset { get; set; }

        [DataMember]
        public LoginUserStatus? StatusID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> LastLoginDate { get; set; }
               
        [DataMember]
        public bool IsRequired { get; set; }

        [DataMember]
        public Nullable<int> RegisteredCountryID { get; set; }

        [DataMember]
        public string RegisteredIP { get; set; }

        [DataMember]
        public Nullable<int> SiteID { get; set; }

        [DataMember]
        public string RegisteredIPCountry { get; set; }
    }
}
