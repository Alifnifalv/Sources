using Eduegate.Services.Contracts.Contents.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Contents.Interfaces
{
    public interface IContentService
    {
        ContentFileDTO SaveFile(ContentFileDTO data);

        List<ContentFileDTO> SaveFiles(List<ContentFileDTO> datas);

        ContentFileDTO GetFile(ContentType contentType, long referenceID);

        ContentFileDTO ReadContentsById(long contentID);

        long DeleteEntity(long contentID);

        List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs);

        List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs);

        Task<ContentFileDTO> ReadContentsByIdAsync(long contentID);


    }
}