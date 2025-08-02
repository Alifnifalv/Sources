using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Service.Client
{
    public class MenuServiceClient : BaseClient, IMenu
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.MENU_SERVICE_NAME);

        public MenuServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetails()
        {
            var url = string.Format("{0}/{1}", productService, "GetMenuDetails");
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.MenuLinks.MenuDTO>>(url, _callContext, _logger);
        }

        public Task<List<Services.Contracts.MenuLinks.MenuDTO>> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            var url = string.Format("{0}/{1}?menuLinkType={2}", productService, "GetERPMenu", menuLinkType);
            return Task.FromResult(ServiceHelper.HttpGetRequest<List<Services.Contracts.MenuLinks.MenuDTO>>(url, _callContext, _logger));
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType,long siteID)
        {
            var url = string.Format("{0}/{1}?menuLinkType={2}&siteID={3}", productService, "GetMenuDetailsByType", menuLinkType, siteID);
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.MenuLinks.MenuDTO>>(url, _callContext, _logger);
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode)
        {
            var url = string.Format("{0}/{1}?siteID={2}&categoryCode={3}", productService, "GetMenuHierarchyByCategoryCode", siteID, categoryCode);
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.MenuLinks.MenuDTO>>(url, _callContext, _logger);
        }
    }
}
