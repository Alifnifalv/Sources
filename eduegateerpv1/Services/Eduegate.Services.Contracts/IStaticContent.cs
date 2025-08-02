using System.Collections.Generic;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    public interface IStaticContent
    {
        StaticContentDTO SaveStaticContent(StaticContentDTO contentDTO);

        StaticContentDTO GetStaticContent(long contentID);

        StaticContentTypeDTO GetStaticContentTypes(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes);

        List<StaticContentDataDTO> GetStaticContentData(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes, int pageSize, int pageNumber);
    }
}