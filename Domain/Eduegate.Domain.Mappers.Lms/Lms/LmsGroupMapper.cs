using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;
using Eduegate.Domain.Entity.Lms.Models;
using Eduegate.Services.Contracts.Lms;

namespace Eduegate.Domain.Mappers.Lms.Lms
{
    public class LmsGroupMapper : DTOEntityDynamicMapper
    {
        public static LmsGroupMapper Mapper(CallContext context)
        {
            var mapper = new LmsGroupMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LmsGroupDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LmsGroupDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.SignupGroups.Where(x => x.SignupGroupID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LmsGroupDTO ToDTO(SignupGroup entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var currentDate = DateTime.Now.Date;

            var groupDTO = new LmsGroupDTO()
            {
                SignupGroupID = entity.SignupGroupID,
                GroupTitle = entity.GroupTitle,
                GroupDescription = entity.GroupDescription,
                IsActive = entity.IsActive,
                FromDate = entity.FromDate,
                FromDateString = entity.FromDate.HasValue ? entity.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                ToDate = entity.ToDate,
                ToDateString = entity.ToDate.HasValue ? entity.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                DueDate = entity.DueDate,
                DueDateString = entity.DueDate.HasValue ? entity.DueDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                CurrentDate = currentDate.Date,
                CurrentDateString = currentDate.Date.ToString(dateFormat, CultureInfo.InvariantCulture),
                IsExpired = false,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LmsGroupDTO;

            var currentDate = DateTime.Now.Date;

            if (toDto.SignupGroupID == 0)
            {
                if (currentDate > toDto.FromDate)
                {
                    throw new Exception("The 'From date' should be greater than or equal to today's date.");
                }
                else if (currentDate > toDto.ToDate)
                {
                    throw new Exception("The 'To date' should be greater than or equal to today's date.");
                }
                else if (currentDate > toDto.DueDate)
                {
                    throw new Exception("The 'Due date' should be greater than or equal to today's date.");
                }
            }

            if (toDto.ToDate < toDto.FromDate)
            {
                throw new Exception("To date must be greater than or equal to 'From date'");
            }
            else if (toDto.DueDate < toDto.FromDate)
            {
                throw new Exception("Due date must be greater than or equal to 'From date'");
            }
            else if (toDto.DueDate > toDto.ToDate)
            {
                throw new Exception("Due date must be greater than or equal to 'To date'");
            }

            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = new SignupGroup()
                {
                    SignupGroupID = toDto.SignupGroupID,
                    GroupTitle = toDto.GroupTitle,
                    GroupDescription = toDto.GroupDescription,
                    FromDate = toDto.FromDate,
                    ToDate = toDto.ToDate,
                    DueDate = toDto.DueDate,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.SignupGroupID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SignupGroupID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SignupGroupID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SignupGroupID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.SignupGroups.Add(entity);
                if (entity.SignupGroupID == 0)
                {
                    var maxGroupID = dbContext.SignupGroups.Max(a => (int?)a.SignupGroupID);
                    entity.SignupGroupID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.SignupGroupID));
            }
        }

        public List<LmsGroupDTO> GetActiveSignupGroups()
        {
            var dtos = new List<LmsGroupDTO>();

            using (var dbContext = new dbEduegateLmsContext())
            {
                var currentDate = DateTime.Now;

                var entities = dbContext.SignupGroups.Where(x => x.IsActive == true && x.ToDate.Value.Date >= currentDate.Date)
                    .OrderByDescending(o => o.SignupGroupID)
                    .AsNoTracking()
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        public LmsGroupDTO GetGroupDetailsByID(int groupID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var groupDTO = new LmsGroupDTO();

                var entity = dbContext.SignupGroups.Where(x => x.SignupGroupID == groupID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    groupDTO = ToDTO(entity);
                }

                return groupDTO;
            }
        }

    }
}