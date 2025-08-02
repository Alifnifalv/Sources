using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services
{
    public class News : BaseService, INews
    {
        public List<NewsDTO> GetNews(NewsTypes Type, int pageSize, int pageNumber)
        {
            return new NewsBL(CallContext).GetNews(Type, pageSize, pageNumber);
        }

        public NewsDTO GetNewsByID(string newsID)
        {
            return new NewsBL(CallContext).GetNews(long.Parse(newsID));
        }

        public NewsDTO SaveNews(NewsDTO newsDTO)
        {
            return new NewsBL(CallContext).SaveNews(newsDTO);
        }
    }
}
