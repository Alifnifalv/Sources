using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Metadata;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Services.Contracts.Services
{
    public interface IMetadataService
    {
        List<FilterColumnDTO> GetFilterMetadata(SearchView view);

        bool SaveUserFilterMetadata(UserFilterValueDTO value);

        List<Contracts.Metadata.FilterUserValueDTO> GetUserFilterMetadata(Contracts.Enums.SearchView view);

        DocumentTypeDTO GetDocumentType(long documentTypeID, string type = null);

        DocumentTypeDTO SaveDocumentType(DocumentTypeDTO familyDTO);

        List<ColumnDTO> AvailableViewColumns(SearchView view);

        LocationDTO GetLocation(long locationID);

        LocationDTO SaveLocation(LocationDTO location);
    }
}