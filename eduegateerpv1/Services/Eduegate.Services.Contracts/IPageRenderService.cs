using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPageRenderService" in both code and config file together.
    public interface IPageRenderService
    {

        PageDTO GetPageInfo(long pageID, string parameter);

        PageDTO SavePage(PageDTO dto);

        PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID);
    }
}