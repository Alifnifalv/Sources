using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers
{
    public class ProductImageMapMapper : IDTOEntityMapper<ProductImageMapDTO, ProductImageMap>
    {
        private CallContext _context;

        public static ProductImageMapMapper Mapper(CallContext context)
        {
            var mapper = new ProductImageMapMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ProductImageMapDTO> ToDTO(List<ProductImageMap> entities)
        {
            var dtos = new List<ProductImageMapDTO>();

            foreach (var grpEntity in entities.GroupBy(x => x.ProductSKUMapID))
            {
                var dto = new ProductImageMapDTO()
                {
                    ProductSKUMapID = grpEntity.Key.Value,
                };

                foreach (var map in grpEntity)
                {
                    switch ((ImageTypes)map.ProductImageTypeID)
                    {
                        case ImageTypes.Large:
                            dto.ZoomImage = map.ImageFile;
                            break;
                        case ImageTypes.Listing:
                            dto.ListingImage = map.ImageFile;
                            break;
                        case ImageTypes.Small:
                            dto.GalleryImage = map.ImageFile;
                            break;
                        case ImageTypes.Thumbnail:
                            dto.ThumbnailImage = map.ImageFile;
                            break;
                    }
                }

                dtos.Add(dto);
            }

            return dtos;
        }

        public ProductImageMapDTO ToDTO(ProductImageMap entity)
        {
            var dto = new ProductImageMapDTO()
            {
                ProductSKUMapID = entity.ProductImageMapIID,
            };

            switch ((ImageTypes)entity.ProductImageTypeID)
            {
                case ImageTypes.Large:
                    dto.ZoomImage = entity.ImageFile;
                    break;
                case ImageTypes.Listing:
                    dto.ListingImage = entity.ImageFile;
                    break;
                case ImageTypes.Small:
                    dto.GalleryImage = entity.ImageFile;
                    break;
                case ImageTypes.Thumbnail:
                    dto.ThumbnailImage = entity.ImageFile;
                    break;
            }

            return dto;
        }

        public ProductImageMap ToEntity(ProductImageMapDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}
