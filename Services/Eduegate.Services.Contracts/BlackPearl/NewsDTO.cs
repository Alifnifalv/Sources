using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class NewsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long NewsIID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NewsContentShort { get; set; }
        [DataMember]
        public string NewsContent { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public NewsTypes NewsType { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
