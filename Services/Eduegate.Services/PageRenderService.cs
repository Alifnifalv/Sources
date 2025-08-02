using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Services
{
    public class PageRenderService : BaseService, IPageRenderService
    {

        public PageDTO GetPageInfo(long pageID,string parameter)
        {
            try
            {
                var pageInfo = new PageRenderBL(CallContext).GetPageInfo(pageID, parameter);
                Eduegate.Logger.LogHelper<PageDTO>.Info("Service Result : " + pageInfo.ToString());
                return pageInfo;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public PageDTO SavePage(PageDTO dto)
        {
            try
            {
                var pageInfo = new PageRenderBL(CallContext).SavePage(dto);
                Eduegate.Logger.LogHelper<PageDTO>.Info("Service Result : " + pageInfo.ToString());
                return pageInfo;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            try
            {
                var pageInfo = new PageRenderBL(CallContext).GetPowerBIDataUsingPageID(pageID);
                Eduegate.Logger.LogHelper<PageDTO>.Info("Service Result : " + pageInfo.ToString());
                return pageInfo;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
