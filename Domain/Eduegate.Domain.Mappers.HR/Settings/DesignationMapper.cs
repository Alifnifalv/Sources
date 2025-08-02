using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Designation;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eduegate.Framework;
using System;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity.HR.Payroll;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Settings
{
    public class DesignationMapper : DTOEntityDynamicMapper
    {
        public static DesignationMapper Mapper(CallContext context)
        {
            var mapper = new DesignationMapper();
            mapper._context = context;
            return mapper;
        }

        public List<DesignationDTO> ToDTO(List<Designation> entities)
        {
            var dtos = new List<DesignationDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public DesignationDTO ToDTO(Designation entity)
        {
            return new DesignationDTO() { DesignationID = entity.DesignationID, DesignationName = entity.DesignationName };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<DesignationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as DesignationDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.Designations.Where(X => X.DesignationID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new DesignationDTO()
                {
                    DesignationName = entity.DesignationName,
                    DesignationID = entity.DesignationID,
                    IsTransportNotification = entity.IsTransportNotification,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as DesignationDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Designation()
            {
                DesignationID = toDto.DesignationID,
                DesignationName = toDto.DesignationName,
                IsTransportNotification = toDto.IsTransportNotification,
                CreatedBy = toDto.DesignationID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.DesignationID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.DesignationID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.DesignationID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {               
                if (entity.DesignationID == 0)
                {
                    var maxGroupID = dbContext.Designations.Max(a => (int?)a.DesignationID);
                    entity.DesignationID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Designations.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Designations.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

            }

            return ToDTOString(new DesignationDTO()
            {
                DesignationID = entity.DesignationID,
                DesignationName = entity.DesignationName,
                IsTransportNotification = toDto.IsTransportNotification,
                CreatedBy = toDto.DesignationID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.DesignationID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.DesignationID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.DesignationID > 0 ? DateTime.Now : dto.UpdatedDate,
            });
        }
    }
}