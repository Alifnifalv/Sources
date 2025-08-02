using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Salon
{
    [DataContract]
    public class ExtraTimeTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int ExtraTimeTypeID { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
