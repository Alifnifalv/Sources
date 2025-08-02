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
    public class NewsDataServiceClient : BaseClient, INews
    {
        public NewsDataServiceClient(CallContext context = null, Action<string> logger = null)
          : base(context, logger)
        {
        }

        public List<NewsDTO> GetNews(NewsTypes type, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public NewsDTO GetNewsByID(string newsID)
        {
            throw new NotImplementedException();
        }

        public NewsDTO SaveNews(NewsDTO newsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
