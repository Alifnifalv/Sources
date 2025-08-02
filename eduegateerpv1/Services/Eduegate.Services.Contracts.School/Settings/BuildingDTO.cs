using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Settings
{
    public class BuildingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int BuildingID { get; set; }
        [DataMember]
        public string BuildingName { get; set; }
    }
}
