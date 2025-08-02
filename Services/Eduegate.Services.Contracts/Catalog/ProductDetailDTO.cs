using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductDetailDTO
    {
        [DataMember]
        public decimal ProductIID { get; set; }
        [DataMember]
        public string ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string ProductImageUrl { get; set; }

        [DataMember]
        public string Metal { get; set; }

        [DataMember]
        public string Stone { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string PriceUnit { get; set; }

        [DataMember]
        public decimal OrderedQuantity { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public decimal AvailableQuantity { get; set; }

        [DataMember]
        public decimal AllowedQuantity { get; set; }

        [DataMember]
        public int? DeliveryDaysCount { get; set; }

    }


    [DataContract]
    public class CartProductDTO : ProductDTO
    {
        [DataMember]
        public string PriceUnit { get; set; }

        [DataMember]
        public string SubTotal { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public string Details { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }

        [DataMember]
        public decimal AvailableQuantity { get; set; }

        [DataMember]
        public decimal AllowedQuantity { get; set; }

        //[DataMember]
        //public long SKUID { get; set; }

        [DataMember]
        public Int32 DeliveryTypeID { get; set; }

        [DataMember]
        public Nullable<decimal> MinimumQuanityInCart { get; set; }
        [DataMember]
        public Nullable<decimal> MaximumQuantityInCart { get; set; }
        [DataMember]
        public List<KeyValuePair<byte, string>> Properties { get; set; }
        [DataMember]
        public List<KeyValuePair<long, string>> Categories { get; set; }

         [DataMember]
        public List<DeliveryDTO> DeliveryTypes { get; set; }
         [DataMember]
         public bool IsOutOfStock { get; set; }

         [DataMember]
         public bool IsCartQuantityAdjusted { get; set; }

        [DataMember]
         public bool IsSerialNo { get; set; }

        [DataMember]
        public long BranchID { get; set; }

        [DataMember]    
        public int DeliveryPriority { get; set; }

        [DataMember]
        public long? CustomerID { get; set; }

        [DataMember]
        public bool IntlDeliveryEnabled { get; set; }

        [DataMember]
        public string DisplayText { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime DeliveryDateTime { get; set; }
    }


     [DataContract]
    public class DeliveryDTO
    {
         public DeliveryDTO()
         {
             CutOffDisplayTextCulture = new List<DeliveryTypeTimeSlotMapCultureDTO>();
         }
        [DataMember]
        public int DeliveryMethodID { get; set; }
        [DataMember]
        public string DeliveryType { get; set; }
        [DataMember]
        public decimal DeliveryCharge { get; set; }

        [DataMember]
        public  Nullable<int> DeliveryRange { get; set; }
        [DataMember]
        public Nullable<bool> IsCutOff { get; set; }
        [DataMember]
        public Nullable<short> CutOffDays { get; set; }
        [DataMember]
        public string CutOffTime { get; set; }
        [DataMember]
        public string CutOffHour { get; set; }

        [DataMember]
        public string CutOffDisplayText { get; set; }

        [DataMember]
        public List<DeliveryTypeTimeSlotMapCultureDTO> CutOffDisplayTextCulture { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime DeliveryDateTime { get; set; }
    }
}
