using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public partial class DeliveryTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short DeliveryTypeID { get; set; }
        [DataMember]
        public string DeliveryMethod { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public List<ProductDeliveryTypeDTO> DeliveryDetails { get; set; }
        [DataMember]
        public List<ZoneDeliveryChargeDTO> ZoneDeliveryDetails { get; set; }
        [DataMember]
        public List<AreaDeliveryChargeDTO> AreaDeliveryDetails { get; set; }
        [DataMember]
        public List<CustomerGroupDeliveryChargeDTO> CustomerGroupDeliveryDetails { get; set; }
    }

    public class DeliveryTypeMapper
    {
        public static DeliveryType ToEntity(DeliveryTypeDTO dto)
        {
            if (dto != null)
            {
                return new DeliveryType() {
                    DeliveryTypeID = dto.DeliveryTypeID,
                    DeliveryMethod = dto.DeliveryMethod,
                    Description = dto.Description
                };
            }
            else
                return new DeliveryType();
        }


        public static DeliveryTypeDTO ToDto(DeliveryType entity)
        {
            if (entity != null)
            {
                return new DeliveryTypeDTO()
                {
                    DeliveryTypeID = entity.DeliveryTypeID,
                    DeliveryMethod = entity.DeliveryMethod,
                    Description = entity.Description
                };
            }
            else
                return new DeliveryTypeDTO();
        }

    }
}
