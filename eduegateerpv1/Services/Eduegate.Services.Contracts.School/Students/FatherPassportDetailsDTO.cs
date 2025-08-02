using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    public class FatherPassportDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        
        [DataMember]
        public long PassportDetailsIID { get; set; }

        [DataMember]
        public string PassportNo { get; set; }

        [DataMember]
        public int? FatherCountryofIssueID { get; set; }

        [DataMember]
        public DateTime? PassportNoIssueDate { get; set; }

        [DataMember]
        public DateTime? PassportNoExpiryDate { get; set; }


        [DataMember]
        public string FatherCountryofIssue { get; set; }
    }
}
