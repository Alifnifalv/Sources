using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Framework
{
    public interface IFrameworkService
    {
        ScreenMetadataDTO GetScreenMetadata(long screenID);

        string GetScreenData(long screenID, long IID);

        ScreenDataDTO SaveScreenData(ScreenDataDTO data);

        CRUDDataDTO SaveCRUDData(CRUDDataDTO data);

        KeyValueDTO ValidateField(CRUDDataDTO data, string field);

        bool DeleteCRUDData(long screenID, long IID);

        long CloneCRUDData(long screenID, long IID);
    }
}