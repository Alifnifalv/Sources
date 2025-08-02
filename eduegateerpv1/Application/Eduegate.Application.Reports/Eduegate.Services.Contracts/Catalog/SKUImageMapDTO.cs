using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class SKUImageMapDTO
    {
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public int Sequence { get; set; }
        [DataMember]
        public string ImagePath { get; set; }
        [DataMember]
        public Nullable<long> ImageMapID { get; set; }
        [DataMember]
        public Nullable<long> SKUMapID { get; set; }
        [DataMember]
        public Nullable<long> ProductImageTypeID { get; set; }
        [DataMember]
        public Nullable<long> ProductID { get; set; }
    }
}
