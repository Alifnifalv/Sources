using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class StudentPassportDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PassportDetailsIID { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

        [DataMember]
        public int? CountryofIssueID { get; set; }

        [DataMember]
        public DateTime? PassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? PassportNoExpiryDate { get; set; }
        [DataMember]
        public string StudentCountryofIssue { get; set; }
        [DataMember]
        public KeyValueDTO CountryofIssue { get; set; }
    }
}
