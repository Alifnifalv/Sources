using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.SearchData
{
    [DataContract]
    public class FacetsDetail
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<FacetItem> FaceItems { get; set; }
    }

    [DataContract]
    public class FacetItem
    {
        [DataMember]
        public string key { get; set; }

        [DataMember]
        public int value { get; set; }

        [DataMember]
        public string code { get; set; }

        [DataMember]
        public object ItemImage { get; set; }

        [DataMember]
        public List<FacetItem> FaceItems { get; set; }
    }
}