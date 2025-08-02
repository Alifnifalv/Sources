using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DocumentTypeTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember] 
        public long DocumentTypeTypeMapIID { get; set; }
        [DataMember] 
        public Nullable<int> DocumentTypeID { get; set; }
        [DataMember] 
        public Nullable<int> DocumentTypeMapID { get; set; }
        [DataMember] 
        public string DocumentTypeMapName { get; set; }
    }
}
