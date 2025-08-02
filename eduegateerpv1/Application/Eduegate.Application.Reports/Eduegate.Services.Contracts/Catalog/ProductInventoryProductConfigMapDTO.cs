using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public partial class ProductInventoryProductConfigMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductInventoryConfigID { get; set; }
        [DataMember]
        public long ProductID { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        //public Nullable<int> UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        //public virtual ProductInventoryConfig ProductInventoryConfig { get; set; }
        //public virtual Product Product { get; set; }
    }
}
