using System.Collections.Generic;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.MenuLinks;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChat" in both code and config file together.
    public interface IMenu
    {
        List<MenuDTO> GetMenuDetails();

        Task<List<MenuDTO>> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType);

        List<MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, long siteID);

        List<MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode);
    }
}