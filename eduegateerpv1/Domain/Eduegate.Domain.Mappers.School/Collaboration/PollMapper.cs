using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class PollMapper : DTOEntityDynamicMapper
    {
        public static PollMapper Mapper(CallContext context)
        {
            var mapper = new PollMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<PollDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private PollDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Polls.Where(a => a.PollIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new PollDTO()
                {
                    PollIID = entity.PollIID,
                    PollTitle = entity.PollTitle,
                    Description = entity.Description,
                    ExpiryDate = entity.ExpiryDate,
                    IsAllowOtherAnwser = entity.IsAllowOtherAnwser,
                    IsOpenForPolling = entity.IsOpenForPolling,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as PollDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Poll()
            {
                PollIID = toDto.PollIID,
                PollTitle = toDto.PollTitle,
                Description = toDto.Description,
                ExpiryDate = toDto.ExpiryDate,
                IsAllowOtherAnwser = toDto.IsAllowOtherAnwser,
                IsOpenForPolling = toDto.IsOpenForPolling,
                CreatedBy = toDto.PollIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.PollIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.PollIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.PollIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Polls.Add(entity);
                if (entity.PollIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.PollIID));
        }

    }
}