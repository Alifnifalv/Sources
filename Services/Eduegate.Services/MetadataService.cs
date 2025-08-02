using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Services;

namespace Eduegate.Services
{
    public class MetadataService : BaseService, IMetadataService
    {
        public List<Contracts.Metadata.FilterColumnDTO> GetFilterMetadata(Contracts.Enums.SearchView view)
        {
            return new MetadataBL(this.CallContext).GetFilterMetadata(view);
        }
      
        public bool SaveUserFilterMetadata(Contracts.Metadata.UserFilterValueDTO value)
        {
            return new MetadataBL(this.CallContext).SaveUserFilterMetadata(value);
        }

        public List<Contracts.Metadata.FilterUserValueDTO> GetUserFilterMetadata(Contracts.Enums.SearchView view)
        {
            return new MetadataBL(this.CallContext).GetUserFilterMetadata(view);
        }

        public Contracts.DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null)
        {
            return new MetadataBL(this.CallContext).GetDocumentType(documentTypeID, type == null ? (SchedulerTypes?)null : (SchedulerTypes)Enum.Parse(typeof(SchedulerTypes), type));
        }

        public Contracts.DocumentTypeDTO SaveDocumentType(Contracts.DocumentTypeDTO dto)
        {
            return new MetadataBL(this.CallContext).SaveDocumentType(dto);
        }


        public List<Contracts.Search.ColumnDTO> AvailableViewColumns(Contracts.Enums.SearchView view)
        {
            return new MetadataBL(this.CallContext).AvailableViewColumns(view);
        }


        public Contracts.Warehouses.LocationDTO GetLocation(long locationID)
        {
            return new MetadataBL(this.CallContext).GetLocation(locationID);
        }

        public Contracts.Warehouses.LocationDTO SaveLocation(Contracts.Warehouses.LocationDTO location)
        {
            return new MetadataBL(this.CallContext).SaveLocation(location);
        }
    }
}
