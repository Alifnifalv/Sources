using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class CategoryCultureDataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte CultureID { get; set; }
        [DataMember]
        public long CategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        public string CultureCode { get; set; }
    }
}
