using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class BrandTagMapper : IDTOEntityMapper<KeyValueDTO, BrandTag>
    {
        private CallContext _context;

        public static BrandTagMapper Mapper(CallContext context)
        {
            var mapper = new BrandTagMapper();
            mapper._context = context;
            return mapper;
        }

        public List<KeyValueDTO> ToDTO(List<BrandTag> entities)
        {
            var dtos = new List<KeyValueDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(new KeyValueDTO()
                {
                    Key = entity.BrandTagIID.ToString(),
                    Value = entity.TagName
                });
            }

            return dtos;
        }

        public KeyValueDTO ToDTO(BrandTag entity)
        {
            return new KeyValueDTO()
            {
                Key = entity.BrandTagIID.ToString(),
                Value = entity.TagName
            };
        }

        public BrandTag ToEntity(KeyValueDTO dto)
        {
            return new BrandTag()
            {
                TagName = dto.Value,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                UpdatedDate = DateTime.Now,
                CreatedBy = int.Parse(_context.LoginID.ToString()),
                CreatedDate = DateTime.Now,
            };
        }
    }
}
