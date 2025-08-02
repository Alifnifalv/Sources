using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class CategoryMapper : IDTOEntityMapper<ProductCategoryDTO, Category>
    {
        private CallContext _context;

        public static CategoryMapper Mapper(CallContext context)
        {
            var mapper = new CategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductCategoryDTO ToDTO(Category entity)
        {
            var dto = new ProductCategoryDTO()
            {
                Active = entity.IsActive.HasValue ? entity.IsActive.Value : false,
                CategoryCode = entity.CategoryCode,
                CategoryID = entity.CategoryIID,
                CategoryName = entity.CategoryName,
                ImageName = entity.ImageName,
                IsInNavigationMenu = entity.IsInNavigationMenu.HasValue ? entity.IsInNavigationMenu.Value : false,
                ParentCategoryID = entity.ParentCategoryID,
                ThumbnailImageName = entity.ThumbnailImageName,
                IsReporting = entity.IsReporting,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.Created,
                UpdatedDate = entity.Updated,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };

            dto.CategoryMarketPlace.Profit = entity.Profit;

            if (entity.ParentCategoryID.HasValue)
            {
                var parentEntity = new ProductCatalogRepository().GetCategory(entity.ParentCategoryID.Value);
                dto.ParentCategoryName = parentEntity.CategoryName;
            }

            dto.ImageMaps = new List<Services.Contracts.Catalog.CategoryImageMapDTO>();

            if (entity.CategoryImageMaps != null && entity.CategoryImageMaps.Count > 0)
            {
                foreach (var imageMap in entity.CategoryImageMaps)
                {
                    dto.ImageMaps.Add(new Services.Contracts.Catalog.CategoryImageMapDTO()
                    {
                        CategoryID = dto.CategoryID,
                        CategoryImageMapIID = imageMap.CategoryImageMapIID,
                        ImageFile = imageMap.ImageFile,
                        ImageTitle = imageMap.ImageTitle,
                        ImageTypeID = imageMap.ImageTypeID,
                        ImageType =  imageMap.ImageTypeID.HasValue ? (ImageTypes)Enum.Parse(typeof(ImageTypes), imageMap.ImageTypeID.Value.ToString()) : (ImageTypes?) null,
                        ImageLinkParameters = imageMap.ImageLinkParameters,
                        ImageTarget = imageMap.ImageTarget,
                        CreatedBy = entity.CreatedBy,
                        UpdatedBy = entity.UpdatedBy,
                        CreatedDate = entity.Created,
                        UpdatedDate = entity.Updated,
                        //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(imageMap.TimeStamps),
                        ActionLinkTypeID = imageMap.ActionLinkTypeID,
                        SerialNo = imageMap.SerialNo.HasValue ? imageMap.SerialNo.Value : 0,
                        CompanyID = imageMap.CompanyID
                    });
                }
            }

            if (entity.CategoryTagMaps != null)
            {
                dto.Tags = CategoryTagMapper.Mapper(_context).ToDTO(entity.CategoryTagMaps.ToList());
            }

            if (entity.CategoryCultureDatas != null)
            {
                dto.CategoryCultureDatas = CategoryCultureDataMapper.Mapper(_context).ToDTO(entity.CategoryCultureDatas.ToList());
            }

            foreach (var item in entity.CategorySettings)
            {
                dto.CategorySetting.Add(new CategorySettingDTO() { 
                    CategorySettingsID = item.CategorySettingsID,
                    CategoryID = item.CategoryID,
                    SettingCode = item.SettingCode,
                    SettingValue = item.SettingValue,
                    UIControlTypeID = item.UIControlTypeID,
                    LookUpID = item.LookUpID,
                    Description = item.Description,
                    CreatedBy = item.CreatedBy.HasValue? (int)item.CreatedBy.Value:(int)_context.LoginID,
                    UpdatedBy = item.UpdatedBy.HasValue?(int)item.UpdatedBy.Value:(int)_context.LoginID,
                    CreatedDate = item.CreatedDate.HasValue?item.CreatedDate.Value:DateTime.Now,
                    UpdatedDate = item.UpdatedDate.HasValue ? item.UpdatedDate.Value : DateTime.Now,
                });
            }
            return dto;
        }

        public Category ToEntity(ProductCategoryDTO dto)
        {
            var categoryCode = dto.CategoryCode.Trim().Replace(" ", "-").Replace("_", "");
            categoryCode = Regex.Replace(categoryCode, @"[^\d\w\s]", "-");
            var entity = new Category()
            { 
                IsActive = dto.Active,
                CategoryCode = categoryCode,
                CategoryIID = dto.CategoryID,
                CategoryName = dto.CategoryName,
                ImageName = dto.ImageName,
                IsInNavigationMenu = dto.IsInNavigationMenu,
                ParentCategoryID = dto.ParentCategoryID,
                ThumbnailImageName = dto.ThumbnailImageName,
                IsReporting = dto.IsReporting,

                CreatedBy = dto.CategoryID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                UpdatedBy = dto.CategoryID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                Created = dto.CategoryID > 0 ? dto.CreatedDate : DateTime.Now,
                Updated = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                Profit = dto.CategoryMarketPlace.Profit,
            };

            if (entity.CategoryIID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.Created = DateTime.Now;
            }

            if (dto.ImageMaps != null)
            {
                foreach (var imageMap in dto.ImageMaps)
                {
                    entity.CategoryImageMaps.Add(new CategoryImageMap()
                    {
                        CategoryImageMapIID = imageMap.CategoryImageMapIID,
                        CategoryID = imageMap.CategoryID,
                        ImageFile = imageMap.ImageFile,
                        ImageTitle = imageMap.ImageTitle,
                        ImageTypeID = (byte)imageMap.ImageType,
                        ImageLinkParameters = imageMap.ImageLinkParameters,
                        ImageTarget = imageMap.ImageTarget,
                        CreatedBy = imageMap.CategoryImageMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                        UpdatedBy = imageMap.CategoryImageMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = imageMap.CategoryImageMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        //TimeStamps = imageMap.TimeStamps == null ? null : Convert.FromBase64String(imageMap.TimeStamps),
                        ActionLinkTypeID = imageMap.ActionLinkTypeID,
                        SerialNo = imageMap.SerialNo,
                        CompanyID = _context.IsNotNull() && _context.CompanyID.IsNotNull() ? _context.CompanyID : (int?)null,
                    });
                }
            }

            foreach (var setting in dto.CategorySetting)
            {
                if (setting.SettingValue != null) { 
                entity.CategorySettings.Add(new CategorySetting() { 
                    CategorySettingsID = setting.CategorySettingsID,
                    CategoryID = setting.CategoryID,
                    SettingCode = setting.SettingCode,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    CreatedBy = setting.CategorySettingsID > 0 ? setting.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = setting.CategorySettingsID > 0 ? (int)_context.LoginID : setting.UpdatedBy,
                    CreatedDate = setting.CategorySettingsID > 0 ? setting.CreatedDate : DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
                }
            }

            if (dto.Tags != null)
            {
                entity.CategoryTagMaps = CategoryTagMapper.Mapper(_context).ToEntity(dto.Tags);
            }

            if (dto.CategoryCultureDatas != null)
            {
                entity.CategoryCultureDatas = CategoryCultureDataMapper.Mapper(_context).ToEntity(dto.CategoryCultureDatas);
            }

            return entity;
        }
    }
}
