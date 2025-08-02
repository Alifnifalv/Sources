using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Security.Secured;
using Eduegate.Services.Contracts.MenuLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain
{
    public class MenuBL
    {
        private MenuRepository menuRepository = new MenuRepository();
        private CallContext _context;

        public MenuBL(CallContext context)
        {
            _context = context;
        }

        public List<MenuDTO> GetMenuDetails()
        {
            return menuRepository.GetMenuDetails();
        }

        public async Task<List<MenuDTO>> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType)
        {
            if (_context == null) return new List<MenuDTO>();
            var cacheKey = string.Concat("ERPMenu_", menuLinkType.ToString(), "_", _context.LanguageCode, "_", _context.LoginID.HasValue
                        ? _context.LoginID.Value : 0);
            var dtos = Framework.CacheManager.MemCacheManager<List<MenuDTO>>.Get(cacheKey);

            if (dtos == null || dtos.Count == 0)
            {
                if (_context == null) return dtos;
                dtos = new List<MenuDTO>();
                var menus = await menuRepository.GetMenusByType(menuLinkType, null, _context.LanguageCode);
                var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.HasValue ? _context.LoginID.Value : 0));
                var mapper = MenuMapper.Mapper(_context);

                foreach (var menu in menus)
                {
                    if (secured.HasAccess(menu.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.Menu.ToString())))
                    {
                        dtos.Add(mapper.ToDTO(menu));
                    }
                }

                Framework.CacheManager.MemCacheManager<List<MenuDTO>>.Add(dtos, cacheKey);
            }

            return dtos;
        }

        public List<MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, long siteID)
        {
            //HomePageMenuDTO homePageMenu = new HomePageMenuDTO();
            var dto = new List<MenuDTO>();
            var languageCode = "en";
            if (_context != null)
            {
                languageCode = _context.LanguageCode;
            }
            var menus = menuRepository.GetMenusByTypeAndCulture(menuLinkType, languageCode, Convert.ToInt32(siteID));
            var categoryList = new CategoryRepository().GetCategoryList(languageCode, "").Where(a => a.IsActive == true).ToList();
            var categoryImageMapList = new CategoryRepository().GetCategoryImage(null, 0).ToList();
            var mapper = MenuMapper.Mapper(_context);

            foreach (var menu in menus)
            {
                var menuDTO = mapper.ToDTOCulture(menu);//MenuMapper.Mapper(_context).ToDTO(menu);
                var menuLinkCategoryMapList = menuRepository.GetMenuLinkCategoryMaps(menu.MenuLinkIID);
                menuLinkCategoryMapList = menuLinkCategoryMapList.Where(a => a.SiteID == siteID).ToList();
                if (menuLinkCategoryMapList.Count > 0)
                {
                    BuildMenuTree(menuDTO, menuLinkCategoryMapList, null, categoryList, categoryImageMapList, null);
                }
                else
                { menuDTO.MenuList = new List<MenuDTO>(); }
                menuDTO.ActionLink = (string.IsNullOrEmpty(menuDTO.ActionLink)) ? "#" : menuDTO.ActionLink;
                dto.Add(menuDTO);
            }
            return dto;
        }

        public List<MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode)
        {
            var dto = new List<MenuDTO>();
            var languageCode = "en";

            if (_context != null)
            {
                languageCode = _context.LanguageCode;
            }

            var categoryList = new CategoryRepository().GetCategoryList(languageCode, string.Empty).Where(a => a.IsActive == true).ToList();
            var categoryHeirarchy = menuRepository.GetCategoryTree(siteID, categoryCode);
            var IIDs = new List<long>();

            if (categoryHeirarchy != null)
                foreach (var cat in categoryHeirarchy.Split('/'))
                {
                    IIDs.Add(int.Parse(cat));
                }

            var categories = new CategoryRepository().GetCategoryList(IIDs);

            if (IIDs.Count > 0)
                foreach (var subCat in new CategoryRepository().GetSubCategories(IIDs[0], "en"))
                {
                    categories.Add(subCat);
                }

            var menuDTO = new MenuDTO();
            BuildMenuTree(menuDTO, categories, null, categoryList, null);
            dto.Add(menuDTO);
            return dto;
        }

        private void BuildMenuTree(MenuDTO menuDTO, List<Category> categories, long? parentID,
            List<Category> fullCategoryList, string childCategoryCode)
        {
            var categoryList = fullCategoryList.Where(a => (long?)a.ParentCategoryID == parentID).ToList<Category>();
            var menu = new List<MenuDTO>();

            if (categoryList.Count > 0)
            {
                menu = (from a in categories
                        join b in categoryList on a.CategoryIID equals b.CategoryIID
                        select new MenuDTO()
                        {
                            MenuName = b.CategoryName,
                            ParentMenuID = parentID,
                            CategoryID = b.CategoryIID,
                            CategoryCode = b.CategoryCode
                        }).ToList();

                menuDTO.MenuList = menu;

                if (menu.Count > 0)
                {
                    foreach (var vMenu in menu)
                    {
                        if (childCategoryCode == null || vMenu.CategoryCode != childCategoryCode)
                            BuildMenuTree(vMenu, categories, (long)vMenu.CategoryID, fullCategoryList, childCategoryCode);
                    }
                }
            }
            else
            {
                menuDTO.MenuList = menu;
            }
        }

        private void BuildMenuTree(MenuDTO menuDTO, List<MenuLinkCategoryMap> menuLinkCategoryMapList, long? parentID, List<Category> fullCategoryList, List<CategoryImageMap> categoryImageMap, string childCategoryCode)
        {
            var categoryList = fullCategoryList.Where(a => (long?)a.ParentCategoryID == parentID).ToList<Category>();
            var menu = new List<MenuDTO>();

            if (categoryList.Count > 0)
            {
                menu = (from a in menuLinkCategoryMapList
                        join b in categoryList on a.CategoryID equals b.CategoryIID
                        select new MenuDTO()
                        {
                            MenuLinkIID = (long)a.MenuLinkID,
                            MenuName = b.CategoryName,
                            ParentMenuID = menuDTO.MenuLinkIID,
                            ActionLink = (string.IsNullOrEmpty(a.ActionLink)) ? "#" : a.ActionLink,
                            ThumbnailImage = GetImage(categoryImageMap.Where(c => c.CategoryID == b.CategoryIID && c.ImageTypeID == (int)Eduegate.Services.Contracts.Enums.ImageTypes.Thumbnail).ToList()),//b.ThumbnailImageName,
                            BackgroundImage = GetImage(categoryImageMap.Where(c => c.CategoryID == b.CategoryIID && c.ImageTypeID == (int)Eduegate.Services.Contracts.Enums.ImageTypes.Background).ToList()),
                            CategoryID = b.CategoryIID,
                            CategoryCode = b.CategoryCode
                        }).ToList();

                menuDTO.MenuList = menu;

                if (menu.Count > 0)
                {
                    foreach (var vMenu in menu)
                    {
                        if (childCategoryCode == null || vMenu.CategoryCode != childCategoryCode)
                            BuildMenuTree(vMenu, menuLinkCategoryMapList, (long)vMenu.CategoryID, fullCategoryList, categoryImageMap, childCategoryCode);
                    }
                }
            }
            else
            {
                menuDTO.MenuList = menu;
            }
        }

        public string GetImage(List<CategoryImageMap> categoryImageMapList)
        {
            if (categoryImageMapList.Count > 0)
            {
                return categoryImageMapList.FirstOrDefault().ImageFile;
            }
            return "";
        }
    }
}
