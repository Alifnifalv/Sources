using Eduegate.Domain.Repository;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;
using System.ServiceModel;

namespace Eduegate.Services
{
    public class StaticContent : BaseService, IStaticContent
    {
        public List<StaticContentDataDTO> GetStaticContentData(StaticContentTypes staticContentTypes, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public StaticContentTypeDTO GetStaticContentTypes(StaticContentTypes staticContentTypes)
        {
            throw new NotImplementedException();
        }

        //public StaticContentDTO SaveStaticContent(StaticContentDTO contentDTO)
        //{
        //    return new StaticContentBL(CallContext).SaveContent(contentDTO);
        //}

        public StaticContentDTO GetStaticContent(long contentID)
        {
            try
            {
                var entity = new StaticContentRepository().GetStaticContent(contentID);

                return new StaticContentDTO()
                {
                    ContentDataIID = entity.ContentDataIID,
                    Title = entity.Title,
                    Description = entity.Description,
                    ImageFilePath = entity.ImageFilePath,
                };
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<StaticContentDTO>.Fatal(ex.Message, ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //public StaticContentTypeDTO GetStaticContentTypes(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes)
        //{
        //    return new StaticContentBL(CallContext).GetStaticContentTypes(staticContentTypes);
        //}

        //public List<StaticContentDataDTO> GetStaticContentData(Eduegate.Services.Contracts.Enums.StaticContentTypes staticContentTypes, int pageSize, int pageNumber)
        //{
        //    return new StaticContentBL(CallContext).GetStaticContentData(staticContentTypes, pageSize, pageNumber);
        //}

        public StaticContentDTO SaveStaticContent(StaticContentDTO contentDTO)
        {
            throw new NotImplementedException();
        }

        //StaticContentDTO IStaticContent.GetStaticContent(long contentID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}