using Eduegate.Frameworks.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenDataSetDTO
    {
        [DataMember]
        public Screens Screen { get; set; }
        [DataMember]
        List<ColumnDTO> Columns { get; set; }
        [DataMember]
        public List<DataRowDTO> Rows { get; set; }
    }
}
