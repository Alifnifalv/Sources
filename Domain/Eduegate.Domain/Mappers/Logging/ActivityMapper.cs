using Eduegate.Domain.Entity.Logging.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.Logging
{
    public class ActivityMapper : IDTOEntityMapper<ActivityDTO, Activity>
    {
        public static ActivityMapper Mapper()
        {
            var mapper = new ActivityMapper();
            return mapper;
        }

        public ActivityDTO ToDTO(Activity entity)
        {
            var login = entity.CreatedBy.HasValue ? new Repository.AccountRepository().GetLoginDetailByLoginID(entity.CreatedBy.Value) : new Entity.Models.Login();

            return new ActivityDTO()
            {
                ActivityID = entity.ActivitiyIID,
                ActivityTypeID = entity.ActivityTypeID.Value,
                ActivityTypeName = entity.ActivityType?.ActivityTypeName,
                Description = entity.Description,
                ActionStatusID = entity.ActionStatusID,
                ActionStatusName = entity.ActionStatus?.Description,
                ActionTypeID = entity.ActionTypeID,
                ActionTypeName = entity.ActivityType?.ActivityTypeName,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.Created,
                ReferenceID = entity.ReferenceID,
                CreatedUserName = login == null ? string.Empty : login.LoginEmailID
            };
        }

        public List<ActivityDTO> ToDTO(List<Activity> entities)
        {
            var activityDTOs = new List<ActivityDTO>();

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    activityDTOs.Add(ToDTO(entity));
                }
            }

            return activityDTOs;
        }

        public Activity ToEntity(ActivityDTO dto)
        {
            return new Activity()
            {
                ActivitiyIID = dto.ActivityID,
                ActivityTypeID = dto.ActivityTypeID,
                Description = dto.Description,
                ActionStatusID = dto.ActionStatusID,
                ActionTypeID = dto.ActionStatusID,
                ReferenceID = dto.ReferenceID,
                CreatedBy = dto.CreatedBy.HasValue ? int.Parse(dto.CreatedBy.ToString()) : (int?)null,
                Created = dto.CreatedDate
            };
        }

        public List<Activity> ToEntity(List<ActivityDTO> dtos)
        {
            var entities = new List<Activity>();

            foreach (var dto in dtos)
            {
                entities.Add(ToEntity(dto));
            }

            return entities;
        }
    }
}
