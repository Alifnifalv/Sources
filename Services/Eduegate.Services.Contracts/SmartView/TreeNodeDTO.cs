using System.Collections.Generic;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.SmartView
{
    [DataContract]
    public class TreeNodeDTO
    {
        public TreeNodeDTO()
        {
            Nodes = new List<TreeNodeDTO>();
        }

        [DataMember]
        public SmartTreeNodeType SmartTreeNodeType { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public decimal? Ledger { get; set; }

        [DataMember]
        public List<TreeNodeDTO> Nodes { get; set; }
    }
}