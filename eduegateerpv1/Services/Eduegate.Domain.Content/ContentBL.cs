using Eduegate.Domain.Mappers.Contents;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eduegate.Domain.Content
{
    public class ContentBL
    {
        private CallContext _callContext { get; set; }

        public ContentBL(CallContext context)
        {
            _callContext = context;
        }

        public ContentFileDTO SaveFile(ContentFileDTO data)
        {
            return ContentFileMapper.Mapper(_callContext).SaveEntity(data);
        }

        public List<ContentFileDTO> SaveFiles(List<ContentFileDTO> data)
        {
            return ContentFileMapper.Mapper(_callContext).SaveEntity(data);
        }

        public ContentFileDTO GetFile(ContentType contentType, long referenceID)
        {
            return ContentFileMapper.Mapper(_callContext).GetSaveFile(contentType, referenceID);
        }

        public ContentFileDTO ReadContentsById(long contentID)
        {
            return ContentFileMapper.Mapper(_callContext).ReadContentsById(contentID);
        }
        public long DeleteEntity(long contentID)
        {
            return ContentFileMapper.Mapper(_callContext).DeleteEntity(contentID);
        }
        public List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs)
        {
            return ContentFileMapper.Mapper(_callContext).SaveBulkContentFiles(fileDTOs);
        }
        public List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs)
        {
            return ContentFileMapper.Mapper(_callContext).GetContentFileList(fileDTOs);
        }

        public async Task<ContentFileDTO> ReadContentsByIdAsync(long contentID)
        {
            return await ContentFileMapper.Mapper(_callContext).ReadContentsByIdAsync(contentID);
        }
    }
}
