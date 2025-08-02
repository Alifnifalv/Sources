using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class CategoryTagMapper : IDTOEntityMapper<KeyValueDTO, CategoryTagMap>
    {
        private CallContext _context;

        public static CategoryTagMapper Mapper(CallContext context)
        {
            var mapper = new CategoryTagMapper();
            mapper._context = context;
            return mapper;
        }

        public List<KeyValueDTO> ToDTO(List<CategoryTagMap> entities)
        {
            var dtos = new List<KeyValueDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public KeyValueDTO ToDTO(CategoryTagMap entity)
        {
            return new KeyValueDTO()
            {
                Key = entity.CategoryTagID.ToString(),
                Value = entity.CategoryTag.TagName
            };
        }

        public List<KeyValueDTO> ToDTO(List<CategoryTag> entities)
        {
            var dtos = new List<KeyValueDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public KeyValueDTO ToDTO(CategoryTag entity)
        {
            return new KeyValueDTO()
            {
                Key = entity.CategoryTagIID.ToString(),
                Value = entity.TagName
            };
        }

        public List<CategoryTagMap> ToEntity(List<KeyValueDTO> dtos)
        {
            var entities = new List<CategoryTagMap>();

            foreach (var dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }

            return entities;
        }

        public CategoryTagMap ToEntity(KeyValueDTO dto)
        {
            long tagID;
            long.TryParse(dto.Key, out tagID);

            return new CategoryTagMap()
            {
                CategoryTagID = tagID,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                CreatedBy = int.Parse(_context.LoginID.ToString()),
                CreatedDate = DateTime.Now,
                CategoryTag = new CategoryTag()
                {
                    CategoryTagIID = tagID,
                    TagName = dto.Value
                }
            };
        }
    }
}
