using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Services;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class MetadataServiceClient : IMetadataService
    {
        MetadataService service = new MetadataService();

        public MetadataServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public List<Services.Contracts.Metadata.FilterColumnDTO> GetFilterMetadata(Services.Contracts.Enums.SearchView view)
        {
            return service.GetFilterMetadata(view);
        }

        public bool SaveUserFilterMetadata(Services.Contracts.Metadata.UserFilterValueDTO value)
        {
            return service.SaveUserFilterMetadata(value);
        }

        public List<Services.Contracts.Metadata.FilterUserValueDTO> GetUserFilterMetadata(Services.Contracts.Enums.SearchView view)
        {
            return service.GetUserFilterMetadata(view);
        }

        public Services.Contracts.DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null)
        {
            return service.GetDocumentType(documentTypeID, type);
        }

        public Services.Contracts.DocumentTypeDTO SaveDocumentType(Services.Contracts.DocumentTypeDTO dto)
        {
            return service.SaveDocumentType(dto);
        }

        public List<Services.Contracts.Search.ColumnDTO> AvailableViewColumns(Services.Contracts.Enums.SearchView view)
        {
            return service.AvailableViewColumns(view);
        }

        public LocationDTO GetLocation(long locationID)
        {
            return service.GetLocation(locationID);
        }

        public LocationDTO SaveLocation(LocationDTO location)
        {
            return service.SaveLocation(location);
        }
    }
}
