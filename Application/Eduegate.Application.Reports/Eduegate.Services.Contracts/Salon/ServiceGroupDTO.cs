using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class ServiceGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ServiceAvailableID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
    }
}
