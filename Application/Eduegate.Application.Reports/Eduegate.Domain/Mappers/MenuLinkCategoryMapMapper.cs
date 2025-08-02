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

namespace Eduegate.Domain.Mappers
{
    public class MenuLinkCategoryMapMapper : IDTOEntityMapper<MenuDTO, MenuLinkCategoryMap>
    {
        private CallContext _context;

        public static MenuLinkCategoryMapMapper Mapper(CallContext context)
        {
            var mapper = new MenuLinkCategoryMapMapper();
            mapper._context = context;
            return mapper;
        }

        public MenuLinkCategoryMap ToEntity(MenuDTO menuDTO)
        {
            return new MenuLinkCategoryMap();
        }
        public MenuDTO ToDTO(MenuLinkCategoryMap menuLinkCategoryMap)
        {
            var menuDTO = new MenuDTO()
            {
                MenuLinkIID = (long)menuLinkCategoryMap.MenuLinkID,
                ActionLink = (string.IsNullOrEmpty(menuLinkCategoryMap.ActionLink)) ? "#" : menuLinkCategoryMap.ActionLink
            };
            return menuDTO;
        }

        public MenuDTO ToDTO(MenuLinkCategoryMap menuLinkCategoryMap,Category category, long? parentID)
        {

            var menuDTO = ToDTO(menuLinkCategoryMap);
            if (category != null)
            {
                menuDTO.MenuName = category.CategoryName;
                menuDTO.ParentMenuID = parentID;
                menuDTO.ThumbnailImage = category.ThumbnailImageName;
                menuDTO.CategoryID = category.CategoryIID;
            }
            
            return menuDTO;
        }
    }
}
