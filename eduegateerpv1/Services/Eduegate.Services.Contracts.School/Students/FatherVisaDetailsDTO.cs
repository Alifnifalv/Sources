using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class FatherVisaDetailsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long VisaDetailsIID { get; set; }

        [DataMember]
        public string VisaNo { get; set; }

        [DataMember]
        public DateTime? VisaIssueDate { get; set; }

        [DataMember]
        public DateTime? VisaExpiryDate { get; set; }
    }
}
