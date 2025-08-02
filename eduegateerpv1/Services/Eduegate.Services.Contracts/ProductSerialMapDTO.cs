using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductSerialMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductSerialID { get; set; }
        [DataMember]
        public string SerialNo { get; set; }
        [DataMember]
        public Nullable<long> DetailID { get; set; }
        [DataMember]
        public long ProductSKUMapID { get; set; }
        [DataMember]
        public int ProductLength { get; set; }
        [DataMember]
        public bool IsError { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
