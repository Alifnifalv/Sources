using System.Runtime.Serialization;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.SmartView
{
    [DataContract]
    public class SmartTreeViewDTO
    {
        public SmartTreeViewDTO()
        {
            Node = new TreeNodeDTO();
        }

        [DataMember]
        public SmartViewType SmartViewType {get;set;}

        [DataMember]
        public TreeNodeDTO Node { get; set; }
    }
}