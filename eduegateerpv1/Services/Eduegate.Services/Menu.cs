using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.MenuLinks;

namespace Eduegate.Services
{
    public class Menu : BaseService, IMenu
    {
        public List<MenuDTO> GetMenuDetails()
        {
            return new MenuBL(CallContext).GetMenuDetails();
        }

        public async Task<List<MenuDTO>> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            return await new MenuBL(CallContext).GetERPMenu(menuLinkType);
        }
        public List<MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType,long siteID)
        {
            return new MenuBL(CallContext).GetMenuDetailsByType(menuLinkType, siteID);
        }

        public List<MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode)
        {
            return new MenuBL(CallContext).GetMenuHierarchyByCategoryCode(siteID, categoryCode);
        }
    }
}
