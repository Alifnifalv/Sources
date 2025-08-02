using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class MenuServiceClient :  IMenu
    {
        Menu service = new Menu();

        public MenuServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetails()
        {
            return service.GetMenuDetails();
        }

        public Task<List<Services.Contracts.MenuLinks.MenuDTO>> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            return service.GetERPMenu(menuLinkType);
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType,long siteID)
        {
            return service.GetMenuDetailsByType(menuLinkType, siteID);
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode)
        {
            return service.GetMenuHierarchyByCategoryCode(siteID, categoryCode);
        }
    }
}
