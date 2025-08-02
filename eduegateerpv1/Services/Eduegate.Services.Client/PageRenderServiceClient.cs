using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.PageRender;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Mutual;

namespace Eduegate.Service.Client
{
    public class PageRenderServiceClient : BaseClient, IPageRenderService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.PAGE_RENDER_SERVICE_NAME);
        public PageRenderServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public PageDTO GetPageInfo(long pageID, string parameter)
        {
            var bannerUri = string.Format("{0}/{1}?pageID={2}&parameter={3}", productService, "GetPageInfo", pageID, parameter);
            return ServiceHelper.HttpGetRequest<PageDTO>(bannerUri, _callContext, _logger);
        }

        public PageDTO SavePage(PageDTO dto)
        {
            return ServiceHelper.HttpPostGetRequest<PageDTO>(string.Format("{0}/{1}", productService, "SavePage"),dto, _callContext, _logger);
        }

        public PowerBiDashBoardDTO GetPowerBIDataUsingPageID(long? pageID)
        {
            var bannerUri = string.Format("{0}/{1}?pageID={2}&parameter={3}", productService, "GetPowerBIDataUsingPageID", pageID);
            return ServiceHelper.HttpGetRequest<PowerBiDashBoardDTO>(bannerUri, _callContext, _logger);
        }
    }
}
