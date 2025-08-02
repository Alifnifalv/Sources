using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class ProductDeliveryCountrySettingMapper
    {
        public static ProductDeliveryCountrySetting ToEntity(ProductDeliveryCountrySettingDTO dto)
        {

            if (dto != null)
            {
                return new ProductDeliveryCountrySetting()
                {
                    ProductDeliveryCountrySettingsIID = dto.ProductDeliveryCountrySettingsIID,
                    ProductID = dto.ProductID,
                    ProductSKUMapID = dto.ProductSKUMapID,
                    CountryID = dto.CountryID,
                };
            }
            else return new ProductDeliveryCountrySetting();
        }

        public static ProductDeliveryCountrySettingDTO ToDTO(ProductDeliveryCountrySetting entity)
        {

            if (entity != null)
            {
                return new ProductDeliveryCountrySettingDTO()
                {
                    ProductDeliveryCountrySettingsIID = entity.ProductDeliveryCountrySettingsIID,
                    ProductID = entity.ProductID,
                    ProductSKUMapID = entity.ProductSKUMapID,
                    CountryID = entity.CountryID,
                    CountryName = entity.CountryID.IsNotNull() ? new ReferenceDataRepository().GetCountryName((long)entity.CountryID) : string.Empty,
                };
            }
            else return new ProductDeliveryCountrySettingDTO();
        }
    }
}
