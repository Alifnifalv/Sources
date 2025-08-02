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
    public class ProspectusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProspectusDTO()
        {

        }
        [DataMember]
        public string ProsNo { get; set; }

        [DataMember]
        public decimal? ProsFee { get; set; }
        [DataMember]
        public string ProsRemarks { get; set; }
    }
}
