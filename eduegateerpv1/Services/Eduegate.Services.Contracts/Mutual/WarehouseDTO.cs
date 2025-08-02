using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class WarehouseDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long WareHouseID { get; set; }
        [DataMember]
        public string WarehouseName { get; set; }
        [DataMember]
        public Nullable<Byte> StatusID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
