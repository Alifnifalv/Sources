using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.MenuLinks;

namespace Eduegate.Web.Library.ViewModels.MenuLinks
{
    public class MenuLinkViewModel
    {
        public List<MenuLinkItemViewModel> MenuItems { get; set; }
        public MenuLinkTypes MenuLinkType { get; set; }

        public static MenuLinkViewModel BuildMenuLinks(List<MenuDTO> menus)
        {
            var menuVm = new MenuLinkViewModel();
            menuVm.MenuItems = new List<MenuLinkItemViewModel>();
            foreach (var item in menus.Where(a => a.ParentMenuID == null))
            {
                var menuItemVM = new MenuLinkItemViewModel()
                {
                    MenuID = item.MenuLinkIID,
                    MenuName = item.MenuName,
                    IconImage = item.MenuIcon,
                    Title = item.MenuTitle,
                    HtmlAttributes = item.ActionLink,
                    HtmlAttributes1 = item.ActionLink1,
                    HtmlAttributes2 = item.ActionLink2,
                    HtmlAttributes3 = item.ActionLink3,
                    Parameters = item.Parameters,
                    Hierarchy = item.MenuName,
                };

                BuildRecursive(menus, menuItemVM);
                menuVm.MenuItems.Add(menuItemVM);
            }

            PopulateHeirarchyNames(menuVm.MenuItems);
            return menuVm;
        }

        private static void PopulateHeirarchyNames(List<MenuLinkItemViewModel> menuVm)
        {
            foreach (var childItem in menuVm)
            {
                childItem.Hierarchy = childItem.Hierarchy +
                    GetChildHeirarchyNames(childItem, new StringBuilder());
                if (childItem.SubItems != null)
                {
                    PopulateHeirarchyNames(childItem.SubItems);
                }
            }
        }

        private static string GetChildHeirarchyNames(MenuLinkItemViewModel parent, StringBuilder menuHeirarchy)
        {
            var childMenu = new StringBuilder();

            if (parent.SubItems != null)
            {               
                foreach (var childItem in parent.SubItems)
                {
                    GetChildHeirarchyNames(childItem, menuHeirarchy);
                }
            }

            menuHeirarchy.Append(",");
            menuHeirarchy.Append(parent.MenuName);
            return menuHeirarchy.ToString();
        }


        private static void BuildRecursive(List<MenuDTO> menus, MenuLinkItemViewModel menuItem)
        {         
            foreach (var menu in menus.Where(a=> a.ParentMenuID == menuItem.MenuID))
            {
                var hierarchy = string.Concat(menuItem.Hierarchy, ",", menu.MenuName);

                var subMenuItem = new MenuLinkItemViewModel()
                {
                    MenuID = menu.MenuLinkIID,
                    MenuName = menu.MenuName,
                    IconImage = menu.MenuIcon,
                    Title = menu.MenuTitle,
                    HtmlAttributes = menu.ActionLink,
                    HtmlAttributes1 = menu.ActionLink1,
                    HtmlAttributes2 = menu.ActionLink2,
                    HtmlAttributes3 = menu.ActionLink3,
                    Hierarchy = hierarchy,
                    Parameters = menu.Parameters,
                };              

                if (menuItem.SubItems == null)
                {
                    menuItem.SubItems = new List<MenuLinkItemViewModel>();
                }

                menuItem.SubItems.Add(subMenuItem);
                BuildRecursive(menus, subMenuItem);
            }
        }
    }
}
