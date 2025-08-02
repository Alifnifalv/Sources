using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class DeliveryChargeDTO
    {
        [DataMember]
        public long DeliveryChargeID { get; set; }
        //[DataMember]
        //public string DeliveryName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> CountryID { get; set; }
        [DataMember]
        public Nullable<long> CustomerGroupID { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }
        [DataMember]
        public Nullable<decimal> DeliveryCharge1 { get; set; }
        [DataMember]
        public Nullable<bool> IsPercentage { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }


    public static class DeliveryChargeMapper
    {
        public static DeliveryChargeDTO ToDeliveryChargeDTOMap(DeliveryCharge obj)
        {
            return new DeliveryChargeDTO()
            {
                DeliveryChargeID = obj.DeliveryChargeIID,
                //Description = obj.Description,
                CountryID = obj.ToCountryID,
                //CustomerGroupID = obj.CustomerGroupID,
                //CustomerID = obj.CustomerID,
                DeliveryCharge1 = obj.DeliveryCharge1,
                //IsPercentage = obj.IsPercentage,
                CreatedBy = obj.CreatedBy,
                CreatedDate = obj.CreatedDate,
                UpdatedDate = obj.UpdatedDate,
            };
        }
    }
}
