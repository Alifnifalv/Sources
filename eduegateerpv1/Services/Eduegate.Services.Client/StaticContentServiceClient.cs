using Eduegate.Framework;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Client
{
    public class StaticContentServiceClient : BaseClient, IStaticContent
    {
        public StaticContentServiceClient(CallContext context = null, Action<string> logger = null)
         : base(context, logger)
        {
        }

        public StaticContentDTO GetStaticContent(long contentID)
        {
            throw new NotImplementedException();
        }

        public List<StaticContentDataDTO> GetStaticContentData(StaticContentTypes staticContentTypes, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public StaticContentTypeDTO GetStaticContentTypes(StaticContentTypes staticContentTypes)
        {
            throw new NotImplementedException();
        }

        public StaticContentDTO SaveStaticContent(StaticContentDTO contentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
