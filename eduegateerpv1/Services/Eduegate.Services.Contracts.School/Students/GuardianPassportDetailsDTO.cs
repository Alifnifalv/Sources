using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    public class GuardianPassportDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        
        [DataMember]
        public long PassportDetailsIID { get; set; }

        [DataMember]
        public string GuardianPassportNumber { get; set; }

        [DataMember]
        public string GuardianCountryofIssue { get; set; }

        [DataMember]
        public int? CountryofIssueID { get; set; }

        [DataMember]
        public DateTime? PassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? PassportNoExpiryDate { get; set; }

    }
}
