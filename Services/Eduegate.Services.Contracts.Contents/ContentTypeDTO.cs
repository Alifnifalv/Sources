using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Contents
{
    [DataContract]
    public class ContentTypeDTO : BaseMasterDTO
    {
        [DataMember]
        public int ContentTypeID { get; set; }

        [DataMember]
        public string ContentName { get; set; }
    }
}
