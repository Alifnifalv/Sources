using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.MenuLinks;
using Eduegate.Framework;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.CustomEntity;

namespace Eduegate.Domain.Mappers
{
    public class MenuMapper : IDTOEntityMapper<MenuDTO, MenuLink>
    {
        private CallContext _context;

        public static MenuMapper Mapper(CallContext context)
        {
            var mapper = new MenuMapper();
            mapper._context = context;
            return mapper;
        }

        public MenuLink ToEntity(MenuDTO menuDTO)
        {
            return new MenuLink();
        }
        public MenuDTO ToDTO(MenuLink menuLink)
        {
            var menuDTO = new MenuDTO()
            {
                MenuLinkIID = menuLink.MenuLinkIID,
                MenuName = menuLink.MenuName,
                ParentMenuID = menuLink.ParentMenuID,
                ActionLink = menuLink.ActionLink,
                ActionLink1 = menuLink.ActionLink1,
                ActionLink2 = menuLink.ActionLink2,
                ActionLink3 = menuLink.ActionLink3,
                MenuTitle = menuLink.MenuTitle,
                MenuIcon = menuLink.MenuIcon,
                Parameters = menuLink.Parameters,
                MenuGroup = menuLink.MenuGroup,
            };
            return menuDTO;
        }

        public MenuDTO ToDTOCulture(MenuDetails menuLink)
        {
            var menuDTO = new MenuDTO()
            {
                MenuLinkIID = menuLink.MenuLinkIID,
                MenuName = menuLink.MenuName,
                ParentMenuID = menuLink.ParentMenuID,
                ActionLink = menuLink.ActionLink,
                ActionLink1 = menuLink.ActionLink1,
                ActionLink2 = menuLink.ActionLink2,
                ActionLink3 = menuLink.ActionLink3,
                MenuTitle = menuLink.MenuTitle,
                MenuIcon = menuLink.MenuIcon,
                Parameters = menuLink.Parameters,
            };
            return menuDTO;
        }


    }
}
