using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Mappers
{
    public class CategoryImageMapper : IDTOEntityMapper<CategoryImageMapDTO, CategoryImageMap>
    {
        private CallContext _context;
        public static CategoryImageMapper Mapper(CallContext context)
        {
            var mapper = new CategoryImageMapper();
            mapper._context = context;
            return mapper;
        }

        public CategoryImageMapDTO ToDTO(CategoryImageMap entity)
        {
            return new CategoryImageMapDTO()
            {
                CategoryID = entity.CategoryID,
                CategoryImageMapIID = entity.CategoryImageMapIID,
                ImageFile = entity.ImageFile,
                ImageTitle = entity.ImageTitle,
                ImageTypeID = entity.ImageTypeID,
                ImageType =  (ImageTypes)Enum.Parse(typeof(ImageTypes), entity.ImageTypeID.ToString()),
                ImageLinkParameters = entity.ImageLinkParameters
            };
        }
        public CategoryImageMap ToEntity(CategoryImageMapDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
