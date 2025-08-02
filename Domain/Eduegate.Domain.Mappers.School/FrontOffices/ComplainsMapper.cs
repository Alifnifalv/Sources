using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.FrontOffices
{
    public class ComplainsMapper : DTOEntityDynamicMapper
    {
        public static ComplainsMapper Mapper(CallContext context)
        {
            var mapper = new ComplainsMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<ComplainsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private ComplainsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Complains.Where(x => x.ComplainIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new ComplainsDTO()
                {
                    ComplainIID = entity.ComplainIID,
                    ComplainTypeID = entity.ComplainTypeID,
                    SourceID = entity.SourceID,
                    ComplainType = entity.ComplainType,
                    Phone = entity.Phone,
                    Date = entity.Date,
                    Description = entity.Description,
                    ActionTaken = entity.ActionTaken,
                    Assigned = entity.Assigned,
                    Note = entity.Note,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as ComplainsDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Complain()
            {
                ComplainIID = toDto.ComplainIID,
                ComplainTypeID = toDto.ComplainTypeID,
                SourceID = toDto.SourceID,
                ComplainType = toDto.ComplainType,
                Phone = toDto.Phone,
                Date = toDto.Date,
                Description = toDto.Description,
                ActionTaken = toDto.ActionTaken,
                Assigned = toDto.Assigned,
                Note = toDto.Note,
                CreatedBy = toDto.ComplainIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.ComplainIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.ComplainIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.ComplainIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.ComplainIID == 0)
                {
                    var maxGroupID = dbContext.Complains.Max(a => (long?)a.ComplainIID);
                    entity.ComplainIID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    dbContext.Complains.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Complains.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.ComplainIID));
        }

    }
}