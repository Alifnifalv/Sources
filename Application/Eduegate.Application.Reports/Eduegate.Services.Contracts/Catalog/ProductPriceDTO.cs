using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductPriceDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductPriceListIID { get; set; }
        [DataMember]
        public string PriceDescription { get; set; }
        [DataMember]
        public Nullable<long> CustomerGroupID { get; set; }
        [DataMember]
        public Nullable<decimal> Quantity { get; set; }
        [DataMember]
        public Nullable<decimal> Price { get; set; }
        [DataMember]
        public Nullable<decimal> PricePercentage { get; set; }
        [DataMember]
        public Nullable<short> ProductPriceListTypeID { get; set; }
        [DataMember]
        public Nullable<short> ProductPriceListLevelID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> StartDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> EndDate { get; set; }
        [DataMember]
        public List<BranchMapDTO> BranchMaps { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
