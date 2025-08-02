using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class PropertyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PropertyIID { get; set; }

        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string PropertyTypeName { get; set; }

        [DataMember]
        public string PropertyDescription { get; set; }

        [DataMember]
        public string DefaultValue { get; set; }

        [DataMember]
        public Nullable<byte> PropertyTypeID { get; set; }

        [DataMember]
        public Nullable<bool> IsUnqiue { get; set; }

        [DataMember]
        public Nullable<byte> UIControlTypeID { get; set; }

        [DataMember]
        public Nullable<byte> UIControlValidationID { get; set; }

        [DataMember]
        public Nullable<bool> IsSelected { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapId { get; set; }
        [DataMember]
        public string ProductSKUCode { get; set; }
    }
}
