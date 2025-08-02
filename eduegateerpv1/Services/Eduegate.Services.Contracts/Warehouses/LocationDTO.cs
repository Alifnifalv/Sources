using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Warehouses
{
    [DataContract]
    public class LocationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long LocationIID { get; set; }
        [DataMember]
        public string LocationCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> BranchID { get; set; }
        [DataMember]
        public Nullable<byte> LocationTypeID { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
