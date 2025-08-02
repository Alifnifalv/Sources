using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class CategoryCultureDataMapper : IDTOEntityMapper<CategoryCultureDataDTO, CategoryCultureData>
    {
        private CallContext _context;

        public static CategoryCultureDataMapper Mapper(CallContext context)
        {
            var mapper = new CategoryCultureDataMapper();
            mapper._context = context;
            return mapper;
        }

        public List<CategoryCultureDataDTO> ToDTO(List<CategoryCultureData> entities)
        {
            var dtos = new List<CategoryCultureDataDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public CategoryCultureDataDTO ToDTO(CategoryCultureData entity)
        {
            return new CategoryCultureDataDTO()
            {
                CategoryID = entity.CategoryID,
                CategoryName = entity.CategoryName,
                CultureID = entity.CultureID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public List<CategoryCultureData> ToEntity(List<CategoryCultureDataDTO> dtos)
        {
            var entities = new List<CategoryCultureData>();
            foreach (var dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }

            return entities;
        }

        public CategoryCultureData ToEntity(CategoryCultureDataDTO dto)
        {
            return new CategoryCultureData()
            {
                CategoryID = dto.CategoryID,
                CategoryName = dto.CategoryName,
                CultureID = dto.CultureID,
                CreatedBy = dto.CategoryID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                UpdatedBy = dto.CategoryID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.CategoryID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedDate = DateTime.Now,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
