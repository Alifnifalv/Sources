using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Mappers.Logging;
using Eduegate.Services.Contracts.Logging;
using common = Eduegate.Services.Contracts.Commons;
using search = Eduegate.Services.Contracts.Search;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Domain.Repository.Logging;

namespace Eduegate.Domain.Logging
{
    public class LoggingBL
    {
        private CallContext _callContext;

        public LoggingBL(CallContext callcontext)
        {
            _callContext = callcontext;
        }

        public void SyncLogger(CatalogLoggerDTO catalogLoggerDTO)
        {
            new CatalogLoggerRepository().SyncLogger(new CatalogLoggerMapper().ToEntity(catalogLoggerDTO));
        }

        public DataHistoryEntityDTO GetDataHistoryEntity(int EntityID)
        {
            var dataHistoryEntity = new DataHistoryRepository().GetDataHistoryEntity(EntityID);
            var dataHistoryEntityDTO = DataHistoryMapper.Mapper(_callContext).ToDTO(dataHistoryEntity);
            return dataHistoryEntityDTO;
        }

        public DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName)
        {
            var repo = new DataHistoryRepository();
            var history = repo.GetDataHistory(EntityID, IID, FieldName);
            var dataHistory = history.ConvertToList<DataHistoryDTO>();
            var dataHistoryEntity = repo.GetDataHistoryEntity(EntityID);

            return new DataHistoryResultDTO()
            {
                DataHistoryEntityID = dataHistoryEntity.DataHistoryEntityID,
                Name = dataHistoryEntity.Name,
                Description = dataHistoryEntity.Description,
                HistoryData = dataHistory
            };
        }

        public List<ActivityDTO> GetActivitiesByLoginID(long loginID)
        {
            return ActivityMapper.Mapper().ToDTO(new ActivityLoggerRepository().GetActivitiesByLoginID(loginID));
        }

        public ActivityDTO GetActivity(long activityID)
        {
            return ActivityMapper.Mapper().ToDTO(new ActivityLoggerRepository().GetActivity(activityID));
        }

        public List<ActivityDTO> GetActivities(int activityTypeID)
        {
            return ActivityMapper.Mapper().ToDTO(new ActivityLoggerRepository().GetActivities(activityTypeID));
        }

        public async void SaveActivitiesAsynch(List<ActivityDTO> activities)
        {
            await Task.Factory.StartNew(() => new ActivityLoggerRepository().SaveActivities(ActivityMapper.Mapper().ToEntity(activities)));
        }

        public void SaveActivities(List<ActivityDTO> activities)
        {
            new ActivityLoggerRepository().SaveActivities(ActivityMapper.Mapper().ToEntity(activities));
        }
    }
}
