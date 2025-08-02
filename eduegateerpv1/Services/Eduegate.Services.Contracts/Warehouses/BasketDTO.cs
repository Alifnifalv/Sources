using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Warehouses
{
    [DataContract]
    public class BasketDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long BasketID { get; set; }
        [DataMember]
        public string BasketCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
    }
}
