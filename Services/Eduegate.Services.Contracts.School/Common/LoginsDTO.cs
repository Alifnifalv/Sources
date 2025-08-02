using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts.School.Students;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Common
{
    [DataContract]
    public class LoginsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public LoginsDTO()
        {
            LoginRoleMaps = new LoginRoleMapDTO();
        }

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
        public Infrastructure.Enums.LoginUserStatus? StatusID { get; set; }

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

        [DataMember]
        public LoginRoleMapDTO LoginRoleMaps { get; set; }
    }
}
