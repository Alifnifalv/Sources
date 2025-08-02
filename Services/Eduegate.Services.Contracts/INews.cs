using System.Collections.Generic;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INews" in both code and config file together.
    public interface INews
    {
        List<NewsDTO> GetNews(NewsTypes type, int pageSize, int pageNumber);

        NewsDTO GetNewsByID(string newsID);

        NewsDTO SaveNews(NewsDTO newsDTO);
    }
}