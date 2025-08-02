using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class JobOpeningCultureDataDTO : BaseMasterDTO
    {
        [DataMember]
        public byte CulturID { get; set; }
        [DataMember]
        public long JobIID { get; set; }
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string JobDetail { get; set; }
    }
}
