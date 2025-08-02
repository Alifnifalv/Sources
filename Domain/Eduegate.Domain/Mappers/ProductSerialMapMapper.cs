using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Security;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class ProductSerialMapMapper : IDTOEntityMapper<ProductSerialMapDTO, ProductSerialMap>
    {
        private CallContext _context;

        public static ProductSerialMapMapper Mapper(CallContext context)
        {
            var mapper = new ProductSerialMapMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductSerialMap ToEntity(ProductSerialMapDTO dto)
        {
            if (dto != null)
            {
                var settingRepository = new SettingRepository();
                var setting = settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.WRITESERIALKEYCLAIM);
                var hasClaim = setting == null || string.IsNullOrEmpty(setting.SettingValue) ? false : new SecurityRepository().HasClaimAccess(long.Parse(setting.SettingValue), _context.LoginID.Value);
                var hash = settingRepository.GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                long? productType = default(long);
                if (dto.ProductSKUMapID > 0)
                {
                    productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(dto.ProductSKUMapID)).ProductTypeID;
                }
                var isDigitalProduct = (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital;
                string serialNo = isDigitalProduct ? hasClaim ? StringCipher.Encrypt(dto.SerialNo, hash) : null : dto.SerialNo;

                if (serialNo.IsNotNull())
                {
                    return new ProductSerialMap
                    {
                        ProductSerialIID = dto.ProductSerialID,
                        SerialNo = serialNo,
                        DetailID = dto.DetailID,
                        ProductSKUMapID = dto.ProductSKUMapID,

                        UpdatedDate = dto.ProductSerialID > 0 ? DateTime.Now : dto.UpdatedDate,
                        UpdatedBy = dto.ProductSerialID > 0 ? (int)_context.LoginID : dto.UpdatedBy,

                        CreatedDate = dto.ProductSerialID == 0 ? DateTime.Now : dto.CreatedDate,
                        CreatedBy = dto.ProductSerialID == 0 ? (int)_context.LoginID : dto.CreatedBy,

                        //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    };
                }
                else return null;
            }
            else return null;
        }

        public ProductSerialMapDTO ToDTO(ProductSerialMap entity)
        {
            if (entity != null)
            {
                var settingRepository = new SettingRepository();
                var securityRepository = new SecurityRepository();
                var hash = settingRepository.GetSettingDetail("SERIALPASSPHRASE").SettingValue;
                long? productType = default(long);
                if (entity.ProductSKUMapID > 0)
                {
                    productType = new ProductDetailBL().GetProductBySKUID(Convert.ToInt64(entity.ProductSKUMapID)).ProductTypeID;
                }
                var isDigitalProduct = (Framework.Enums.ProductTypes)(productType) == ProductTypes.Digital;
                var hasFullReadClaim = false;
                try
                {
                    hasFullReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM).SettingValue), _context.LoginID.Value);
                }
                catch (Exception) { }

                var hasPartialReadClaim = false;
                try
                {
                    hasPartialReadClaim = securityRepository.HasClaimAccess(long.Parse(settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READPARTIALSERIALKEYCLAIM).SettingValue), _context.LoginID.Value);
                }
                catch (Exception ex) { }
                var visibleSerialKey = "";
                if (isDigitalProduct)
                {
                    if (entity.SerialNo.IsNotNull())
                    {
                        entity.SerialNo = StringCipher.Decrypt(entity.SerialNo, hash);
                        if (hasFullReadClaim)
                            visibleSerialKey = entity.SerialNo;
                        else if (hasPartialReadClaim)
                        {
                            var length = entity.SerialNo.Length;

                            if (length <= 4)
                            {
                                visibleSerialKey = new String('x', length);
                            }
                            else
                            {
                                visibleSerialKey = new String('x', length - 4) + entity.SerialNo.Substring(length - 4);
                            }
                        }
                        else
                            visibleSerialKey = "";
                    }
                }
                else
                {
                    visibleSerialKey = entity.SerialNo;
                }
                return new ProductSerialMapDTO
                {
                    ProductSerialID = entity.ProductSerialIID,
                    SerialNo = visibleSerialKey,
                    DetailID = entity.DetailID.IsNotNull() ? (long)entity.DetailID : default(long),
                    ProductSKUMapID = entity.ProductSKUMapID.IsNotNull() ? (long)entity.ProductSKUMapID : default(long),
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new ProductSerialMapDTO();
        }
    }
}
