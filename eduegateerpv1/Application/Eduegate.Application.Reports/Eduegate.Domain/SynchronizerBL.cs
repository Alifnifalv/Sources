using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Enums.Synchronizer;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Domain
{
    public class SynchronizerBL
    {
        SynchronizerRepository repository = new SynchronizerRepository();
        private Eduegate.Framework.CallContext _callContext { get; set; }

        public SynchronizerBL(Eduegate.Framework.CallContext context)
        {
            _callContext = context;
        }

        public List<EntityChangeTrackerDTO> GetNextChangeFromQueue(Eduegate.Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges)
        {
            var changeTrackers = new List<EntityChangeTrackerDTO>();
            var trackers = repository.GetNextChangeFromQueue(entity, numberOfChanges);

            if (trackers != null)
            {
                foreach (var tracker in trackers)
                {
                    changeTrackers.Add(ToChangeTrackerDTO(tracker));
                }
            }

            return changeTrackers;
        }

        public EntityChangeTrackerDTO ToChangeTrackerDTO(EntityChangeTracker entity)
        {
            return new EntityChangeTrackerDTO()
            {
                EntityChangeTrackerIID = entity.EntityChangeTrackerIID,
                ProcessedFields =  string.IsNullOrEmpty(entity.ProcessedFields) ? new List<string>() :  new List<string>(entity.ProcessedFields.Split(',').ToList()),
                ProcessedID = entity.ProcessedID.Value,
                TrackerStatus = (Services.Contracts.Enums.Synchronizer.TrackerStatuses)entity.TrackerStatusID,
                Entity = (Services.Contracts.Enums.Synchronizer.Entities)entity.EntityID,
                OperationType = (Services.Contracts.Enums.Synchronizer.OperationTypes)entity.OperationTypeID
            };
        }
        public bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID)
        {
            return repository.UpdateEntityChangeTrackerStatus(changeTrackerID, statusID);
        }

        public EntityChangeTrackerDTO SaveEntityChangeTracker(EntityChangeTrackerDTO tracker)
        {
            var updatedEntity = repository.SaveEntityChangeTracker(ToChangeTrackerEntity(tracker));

            repository.AddEntityChangeTrackersQueue(new EntityChangeTrackersQueue()
            {
                EntityChangeTrackeID = updatedEntity.EntityChangeTrackerIID,
                CreatedDate = DateTime.Now,
                IsReprocess = false,
            });

            //Add it into the quue
            return ToChangeTrackerDTO(updatedEntity);
        }

        public EntityChangeTracker ToChangeTrackerEntity(EntityChangeTrackerDTO dto)
        {
            return new EntityChangeTracker()
            {
                EntityChangeTrackerIID = dto.EntityChangeTrackerIID,
                ProcessedFields = string.Join(",", dto.ProcessedFields),
                ProcessedID = dto.ProcessedID,
                TrackerStatusID = (int)dto.TrackerStatus,
                OperationTypeID = (int) dto.OperationType,
                EntityID = (int)dto.Entity
            };
        }

        public List<FieldMapTypeDTO> GetFieldMaps(FieldMapTypes mapType)
        {
            var maps = new List<FieldMapTypeDTO>();
            foreach (var map in repository.GetFieldMaps(mapType))
            {
                maps.Add(ToFieldMapTypeDTO(map));
            }

            return maps;
        }

        public FieldMapTypeDTO ToFieldMapTypeDTO(SyncFieldMap entity)
        {
            return new FieldMapTypeDTO()
            {
                DestinationField = entity.DestinationField,
                SourceField = entity.SourceField,
                FieldMapID = entity.SyncFieldMapID,
                FieldMapType = (FieldMapTypes)entity.SynchFieldMapTypeID,
            };
        }

        public int SyncGetQueueCount(DateTime SyncLastDatetime, DateTime SyncCurrentDatetime)
        {
            return repository.SyncGetQueueCount(SyncLastDatetime, SyncCurrentDatetime);
        }

        public List<SynchronizerQueue> SyncGetQueueList(DateTime SyncLastDatetime, DateTime SyncCurrentDatetime, int PageSize, int PageNo)
        {
            var changeTrackers = new List<SynchronizerQueue>();
            var list = repository.SyncGetQueueList(SyncLastDatetime, SyncCurrentDatetime, PageSize, PageNo);
           foreach (var item in list)
           {
               changeTrackers.Add(ToChangeTrackerDTO(item));
           }

           return changeTrackers;
        }

        public SynchronizerQueue ToChangeTrackerDTO(string entity)
        {
            return new SynchronizerQueue()
            {
               SkuIID = !string.IsNullOrEmpty(entity)?long.Parse(entity):0
            };
        }
    }
}
