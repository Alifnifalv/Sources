using Eduegate.Frameworks.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenDataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ScreenDataDTO()
        {
            DataSet = new List<ScreenDataSetDTO>();
        }

        [DataMember]
        public List<ScreenDataSetDTO> DataSet { get; set; }
    }
}
