using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductClassMapDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductClassMapDetailDTO()
        {
        }

        [DataMember]
        public long ProductClassMapIID { get; set; }

        [DataMember]
        public long? ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public long? ProductSKUMapID { get; set; }

        [DataMember]
        public int? FeeMasterID { get; set; }

        [DataMember]
        public string FeeMasterName { get; set; }

        [DataMember]
        public int? ProductTypeID { get; set; }

        [DataMember]
        public string ProductTypeName { get; set; }

        [DataMember]
        public int? StreamID { get; set; }

        [DataMember] 
        public string StreamName { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

        [DataMember]
        public string SubjectTypeName { get; set; }
    }
}
