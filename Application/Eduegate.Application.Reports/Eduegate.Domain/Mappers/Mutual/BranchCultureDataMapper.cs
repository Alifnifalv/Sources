using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Mutual
{
    public class BranchCultureDataMapper : IDTOEntityMapper<BranchCultureDataDTO, BranchCultureData>
    {
        private CallContext _context;

        public static BranchCultureDataMapper Mapper(CallContext context)
        {
            var mapper = new BranchCultureDataMapper();
            mapper._context = context;
            return mapper;
        }

        public List<BranchCultureDataDTO> ToDTO(List<BranchCultureData> entities)
        {
            var dtos = new List<BranchCultureDataDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public BranchCultureDataDTO ToDTO(BranchCultureData entity)
        {
            return new BranchCultureDataDTO()
            {
                BranchID = entity.BranchID,
                BranchName = entity.BranchName,
                CultureID = entity.CultureID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public List<BranchCultureData> ToEntity(List<BranchCultureDataDTO> dtos)
        {
            var entities = new List<BranchCultureData>();
            foreach (var dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }

            return entities;
        }

        public BranchCultureData ToEntity(BranchCultureDataDTO dto)
        {
            return new BranchCultureData()
            {
                BranchID = dto.BranchID,
                BranchName = dto.BranchName,
                CultureID = dto.CultureID,
                CreatedBy = dto.BranchID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                UpdatedBy = dto.BranchID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.BranchID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
