using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Warehouses
{
    [DataContract]
    public class JobEntryDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long JobEntryDetailIID { get; set; }

        [DataMember]
        public Nullable<long> JobEntryHeadID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUID { get; set; }

        [DataMember]
        public string ProductSkuName { get; set; }

        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }

        [DataMember]
        public Nullable<decimal> Quantity { get; set; }

        [DataMember]
        public Nullable<int> LocationID { get; set; }

        [DataMember]
        public string LocationName { get; set; }

        [DataMember]
        public Nullable<bool> IsQuantiyVerified { get; set; }

        [DataMember]
        public Nullable<bool> IsBarCodeVerified { get; set; }

        [DataMember]
        public Nullable<bool> IsLocationVerified { get; set; }

        [DataMember]
        public Nullable<int> JobStatusID { get; set; }

        [DataMember]
        public Nullable<decimal> ValidatedQuantity { get; set; }

        [DataMember]
        public string ValidatedLocationBarcode { get; set; }

        [DataMember]
        public string ValidatedPartNo { get; set; }

        [DataMember]
        public string ValidationBarCode { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public string BarCode { get; set; }

        [DataMember]
        public string ProductImage { get; set; }

        [DataMember]
        public Nullable<long> ParentJobEntryHeadID { get; set; }

        [DataMember]
        public string LocationBarcode { get; set; }

        [DataMember]
        public Nullable<decimal> ProductPrice { get; set; }

        [DataMember]
        public string PartNo { get; set; }

        [DataMember]
        public long ProductIID { get; set; }

        [DataMember]
        public string AWBNo { get; set; }
       
    }
}
