using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Metadata
{
    [DataContract]
    public class UserFilterValueDTO
    {
        [DataMember]
        public SearchView View { get; set; }
        [DataMember]
        public long? LoginID { get; set; }
        [DataMember]
        public List<FilterColumnValueDTO> ColumnValues { get; set; }
    }
}
