using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Services;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Service.Client.Direct
{
    public class PageRenderServiceClient :  IPageRenderService
    {
        PageRenderService service = new PageRenderService();

        public PageRenderServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            return service.GetPageInfo(pageID, parameter);
        }

        public PageDTO SavePage(PageDTO dto)
        {
            return service.SavePage(dto);
        }

        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            return service.GetPowerBIDataUsingPageID(pageID);
        }
    }
}
