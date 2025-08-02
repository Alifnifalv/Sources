using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class BrandMapper : IDTOEntityMapper<BrandDTO, Brand>
    {
        private CallContext _context;

        public static BrandMapper Mapper(CallContext context)
        {
            var mapper = new BrandMapper();
            mapper._context = context;
            return mapper;
        }

        public BrandDTO ToDTO(Brand entity)
        {
            var brand = new BrandDTO()
            {
                BrandIID = entity.BrandIID,
                BrandName = entity.BrandName,
                Description = entity.Descirption,
                BrandLogo = entity.LogoFile,
                StatusID = Convert.ToInt32(entity.StatusID),
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                Tags = new List<KeyValueDTO>(),
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                ImageMaps = new List<BrandImageMapDTO>(),
            };

            if (entity.BrandImageMaps != null)
            {
                foreach (var imageMap in entity.BrandImageMaps)
                {
                    brand.ImageMaps.Add(new Services.Contracts.Catalog.BrandImageMapDTO()
                    {
                        BrandID = brand.BrandIID,
                        BrandImageMapIID = imageMap.BrandImageMapIID,
                        ImageFile = imageMap.ImageFile,
                        ImageTitle = imageMap.ImageTitle,
                        ImageTypeID = imageMap.ImageTypeID,
                        ImageType = imageMap.ImageTypeID.HasValue ? (ImageTypes)Enum.Parse(typeof(ImageTypes), imageMap.ImageTypeID.Value.ToString()) : (ImageTypes?)null,

                        CreatedBy = entity.CreatedBy,
                        UpdatedBy = entity.UpdatedBy,
                        CreatedDate = entity.CreatedDate,
                        UpdatedDate = entity.UpdatedDate,
                        TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(imageMap.TimeStamps),
                    });
                }
            }


            foreach (var tag in entity.BrandTagMaps)
            {
                brand.Tags.Add(new Eduegate.Framework.Contracts.Common.KeyValueDTO()
                {
                    Key = tag.BrandTagID.ToString(),
                    Value = tag.Brand.BrandName == null ? null : tag.Brand.BrandName
                });
            }

            return brand;
        }

        public Brand ToEntity(BrandDTO dto)
        {
            string brandCode = dto.BrandName.Trim().Replace(" ", "-").Replace("_", "-");
            brandCode = Regex.Replace(brandCode, @"[^\d\w\s]", "-");

            var brand = new Brand()
            {
                BrandIID = dto.BrandIID,
                BrandName = dto.BrandName,
                // this will make Brand code unique  
                BrandCode = brandCode,
                Descirption = dto.Description,
                LogoFile = dto.BrandLogo,
                StatusID = dto.StatusID != default(long) ? Convert.ToByte(dto.StatusID) : (byte?)null,
                UpdatedBy = _context.LoginID > 0 ? int.Parse(_context.LoginID.ToString()) : default(int),
                UpdatedDate = DateTime.Now,
                TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
            };

            if (dto.BrandIID == 0)
            {
                brand.CreatedBy = int.Parse(_context.LoginID.ToString());
                brand.CreatedDate = DateTime.Now;
            }

            if (dto.ImageMaps != null && dto.ImageMaps.Count>0)
            {
                foreach (var imageMap in dto.ImageMaps)
                {
                    brand.BrandImageMaps.Add(new BrandImageMap()
                    {
                        BrandImageMapIID = imageMap.BrandImageMapIID,
                        BrandID = imageMap.BrandID,
                        ImageFile = imageMap.ImageFile,
                        ImageTitle = imageMap.ImageTitle,
                        ImageTypeID = (byte)imageMap.ImageType,

                        CreatedBy = imageMap.BrandImageMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                        UpdatedBy = imageMap.BrandImageMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = imageMap.BrandImageMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                        UpdatedDate = imageMap.BrandImageMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                        TimeStamps = imageMap.TimeStamps == null ? null : Convert.FromBase64String(imageMap.TimeStamps),
                    });
                }
            }

            foreach (var tag in dto.Tags)
            {
                long tagIID;
                long.TryParse(tag.Key, out tagIID);

                brand.BrandTagMaps.Add(new BrandTagMap()
                {
                    BrandID = dto.BrandIID,
                    BrandTagID = tagIID,
                    CreatedBy = int.Parse(_context.LoginID.ToString()),
                    CreatedDate = DateTime.Now,
                    UpdatedBy = int.Parse(_context.LoginID.ToString()),
                    UpdatedDate = DateTime.Now,
                    BrandTag = new BrandTag()
                    {
                        TagName = tag.Value,
                        BrandTagIID = tagIID,
                    }
                });
            }

            return brand;
        }

        public List<BrandDTO> ToDTO(List<Brand> entities)
        {
            var brands = new List<BrandDTO>();
            foreach (var entity in entities)
            {
                brands.Add(ToDTO(entity));
            }

            return brands;
        }
    }
}
