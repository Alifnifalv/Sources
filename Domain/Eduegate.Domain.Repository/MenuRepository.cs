using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.MenuLinks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Domain.Repository
{
    public class MenuRepository
    {
        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetails()
        {
            List<MenuDTO> MenuDTOList = new List<MenuDTO>();
            dbEduegateERPContext db = new dbEduegateERPContext();

            // get all menu
            var qryMenu = (from m in db.MenuLinks
                           where m.ParentMenuID == null && m.MenuLinkTypeID == null
                           select new
                           {
                               m.MenuLinkIID,
                               m.ParentMenuID,
                               m.MenuName,
                               m.MenuLinkTypeID,
                               m.ActionLink,
                               m.SortOrder
                           })
                           .AsNoTracking()
                           .ToList();

            // get all Submenu
            var qrySubMenu = (from m in db.MenuLinks
                              where m.ParentMenuID != null && m.MenuLinkTypeID == null
                              select new
                              {
                                  m.MenuLinkIID,
                                  m.ParentMenuID,
                                  m.MenuName,
                                  m.MenuLinkTypeID,
                                  m.ActionLink,
                                  m.SortOrder
                              }).ToList();

            // get all category
            var qryCategory = (from c in db.Categories
                               join mc in db.MenuLinkCategoryMaps on c.CategoryIID equals mc.CategoryID
                               select new
                               {
                                   mc.MenuLinkID,
                                   mc.SortOrder,
                                   mc.ActionLink,
                                   c.CategoryIID,
                                   c.ParentCategoryID,
                                   c.CategoryName,
                                   c.CategoryCode
                               }).ToList();

            // get all Brand(Designer)
            var qryBrand = (from b in db.Brands
                            join mb in db.MenuLinkBrandMaps on b.BrandIID equals mb.BrandID
                            orderby mb.SortOrder
                            select new
                            {
                                b.BrandIID,
                                b.BrandName,
                                mb.MenuLinkID,
                                mb.ActionLink,
                                mb.SortOrder
                            }).ToList();

            // for each for all menu
            qryMenu.ToList().ForEach(x =>
            {
                MenuDTO dto = new MenuDTO();
                dto.SubMenuList = new List<SubMenuDTO>();
                dto.BrandList = new List<BrandDTO1>();
                dto.MenuLinkIID = x.MenuLinkIID;
                dto.ParentMenuID = x.ParentMenuID;
                dto.MenuName = x.MenuName;
                dto.MenuLinkTypeID = x.MenuLinkTypeID;
                dto.ActionLink = x.ActionLink;
                dto.SortOrder = x.SortOrder;

                qrySubMenu.Where(y => y.ParentMenuID == x.MenuLinkIID).ToList().ForEach(y =>
                {
                    SubMenuDTO _SubMenuDTO = new SubMenuDTO();
                    _SubMenuDTO.MenuLinkID = (long)y.MenuLinkIID;
                    _SubMenuDTO.SortOrder = y.SortOrder;
                    _SubMenuDTO.ActionLink = y.ActionLink;
                    _SubMenuDTO.SubMenuID = (long)y.ParentMenuID;
                    _SubMenuDTO.SubMenuParentID = 0;
                    _SubMenuDTO.SubMenuName = y.MenuName;
                    // add category
                    dto.SubMenuList.Add(_SubMenuDTO);
                });

                // filter based on menuid in category
                qryCategory.Where(y => y.MenuLinkID == x.MenuLinkIID).ToList().ForEach(y =>
                {
                    SubMenuDTO _SubMenuDTO = new SubMenuDTO();
                    _SubMenuDTO.MenuLinkID = (long)y.MenuLinkID;
                    _SubMenuDTO.SortOrder = y.SortOrder;
                    _SubMenuDTO.ActionLink = y.ActionLink;
                    _SubMenuDTO.SubMenuID = y.CategoryIID;
                    _SubMenuDTO.SubMenuParentID = y.ParentCategoryID;
                    _SubMenuDTO.SubMenuName = y.CategoryName;
                    // add category
                    dto.SubMenuList.Add(_SubMenuDTO);
                });

                // filter based on menuid in brands
                qryBrand.Where(b => b.MenuLinkID == x.MenuLinkIID).ToList().ForEach(b =>
                {
                    SubMenuDTO _SubMenuDTO = new SubMenuDTO();
                    _SubMenuDTO.MenuLinkID = (long)b.MenuLinkID;
                    _SubMenuDTO.SubMenuID = b.BrandIID;
                    _SubMenuDTO.SubMenuParentID = 0;
                    _SubMenuDTO.SubMenuName = b.BrandName;
                    _SubMenuDTO.ActionLink = b.ActionLink;
                    _SubMenuDTO.SortOrder = b.SortOrder;
                    // add brands
                    dto.SubMenuList.Add(_SubMenuDTO);
                });

                // add whole object in list of MenuDTO
                MenuDTOList.Add(dto);
            });
            return MenuDTOList;
        }

        // not useful
        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetails(bool id)
        {
            List<MenuDTO> MenuDTOList = new List<MenuDTO>();
            dbEduegateERPContext db = new dbEduegateERPContext();

            // get all menu
            var qryMenu = (from m in db.MenuLinks
                           where m.MenuLinkTypeID == null
                           select new
                           {
                               m.MenuLinkIID,
                               m.ParentMenuID,
                               m.MenuName,
                               m.MenuLinkTypeID,
                               m.ActionLink,
                               m.ActionLink1,
                               m.ActionLink2,
                               m.ActionLink3,
                               m.SortOrder
                           }).AsNoTracking().ToList();

            // get all category
            var qryCategory = (from c in db.Categories
                               join mc in db.MenuLinkCategoryMaps on c.CategoryIID equals mc.CategoryID
                               select new
                               {
                                   mc.MenuLinkID,
                                   mc.SortOrder,
                                   mc.ActionLink,
                                   c.CategoryIID,
                                   c.ParentCategoryID,
                                   c.CategoryName,
                                   c.CategoryCode
                               }).AsNoTracking().ToList();

            // get all Brand(Designer)
            var qryBrand = (from b in db.Brands
                            join mb in db.MenuLinkBrandMaps on b.BrandIID equals mb.BrandID
                            select new
                            {
                                b.BrandIID,
                                b.BrandName,
                                b.Descirption,
                                b.LogoFile,
                                mb.MenuLinkID,
                                mb.ActionLink,
                                mb.SortOrder
                            }).AsNoTracking().ToList();

            // for each for all menu
            qryMenu.ToList().ForEach(x =>
            {
                MenuDTO dto = new MenuDTO();
                dto.MenuCategoryList = new List<MenuCategoryDTO>();
                dto.BrandList = new List<BrandDTO1>();
                dto.MenuLinkIID = x.MenuLinkIID;
                dto.ParentMenuID = x.ParentMenuID;
                dto.MenuName = x.MenuName;
                dto.MenuLinkTypeID = x.MenuLinkTypeID;
                dto.ActionLink = x.ActionLink;
                dto.ActionLink1 = x.ActionLink1;
                dto.ActionLink2 = x.ActionLink2;
                dto.ActionLink3 = x.ActionLink3;
                dto.SortOrder = x.SortOrder;

                // filter based on menuid in category
                qryCategory.Where(y => y.MenuLinkID == x.MenuLinkIID).ToList().ForEach(y =>
                {
                    MenuCategoryDTO _MenuCategoryDTO = new MenuCategoryDTO();
                    _MenuCategoryDTO.MenuLinkID = (long)y.MenuLinkID;
                    _MenuCategoryDTO.SortOrder = y.SortOrder;
                    _MenuCategoryDTO.ActionLink = y.ActionLink;
                    _MenuCategoryDTO.CategoryIID = y.CategoryIID;
                    _MenuCategoryDTO.ParentCategoryID = y.ParentCategoryID;
                    _MenuCategoryDTO.CategoryName = y.CategoryName;
                    _MenuCategoryDTO.CategoryCode = y.CategoryCode;
                    // add category
                    dto.MenuCategoryList.Add(_MenuCategoryDTO);
                });

                // filter based on menuid in brands
                qryBrand.Where(b => b.MenuLinkID == x.MenuLinkIID).ToList().ForEach(b =>
                {
                    BrandDTO1 _BrandDTO = new BrandDTO1();
                    _BrandDTO.MenuLinkID = b.MenuLinkID;
                    _BrandDTO.BrandIID = b.BrandIID;
                    _BrandDTO.BrandName = b.BrandName;
                    _BrandDTO.Descirption = b.Descirption;
                    _BrandDTO.LogoFile = b.LogoFile;
                    _BrandDTO.ActionLink = b.ActionLink;
                    _BrandDTO.SortOrder = b.SortOrder;
                    // add brands
                    dto.BrandList.Add(_BrandDTO);
                });

                // add whole object in list of MenuDTO
                MenuDTOList.Add(dto);
            });
            return MenuDTOList;
        }

        public async Task<List<MenuLink>> GetMenusByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, long? parentMenuID = null, string languageCode = "en")
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                List<MenuLink> menLinks;

                if (parentMenuID.HasValue && parentMenuID.Value != 0)
                {
                    menLinks = await dbContext.MenuLinks
                        .Include(x=> x.MenuLinkCultureDatas)
                        .Where(x => x.MenuLinkTypeID == (int)menuLinkType && x.ParentMenuID == parentMenuID.Value)
                        .OrderBy(a => a.SortOrder)
                        .AsNoTracking()
                        .ToListAsync();
                }
                else
                {
                    menLinks = await dbContext.MenuLinks
                        .Include(x=> x.MenuLinkCultureDatas).ThenInclude(x=>x.Culture)
                        .Where(x => x.MenuLinkTypeID == (int)menuLinkType)
                        .OrderBy(a => a.SortOrder)
                        .AsNoTracking()
                        .ToListAsync();
                }

                if (languageCode != null && !languageCode.ToLower().StartsWith("en"))
                {
                    foreach (var menu in menLinks)
                    {
                        //var cultureData = menu.MenuLinkCultureDatas.FirstOrDefault(x => x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower()));

                        var cultureData = menu.MenuLinkCultureDatas.FirstOrDefault(x =>
                            x.Culture != null &&
                            x.Culture.CultureCode != null &&
                            x.Culture.CultureCode.ToLower().StartsWith(languageCode.ToLower())
                        );                        

                        if (cultureData != null)
                        {
                            menu.MenuName = cultureData.MenuName ?? menu.MenuName;
                            menu.MenuTitle = cultureData.MenuTitle ?? menu.MenuTitle;
                            menu.ActionLink = cultureData.ActionLink ?? menu.ActionLink;
                            menu.ActionLink1 = cultureData.ActionLink1 ?? menu.ActionLink1;
                        }
                    }
                }

                return menLinks;
            }
        }

        public string GetCategoryTree(int siteID, string categoryCode)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                string searchQuery = string.Format("select IDPath From catalog.CategoryTree where SiteID = {0} and code = '{1}'", siteID, categoryCode);
                return dbContext.Database.SqlQuery<string>($@"{searchQuery}").AsNoTracking().FirstOrDefault();
            }
        }

        public List<MenuDetails> GetMenusByTypeAndCulture(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, string cultureCode, int siteID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                if (cultureCode == "en")
                {
                    var menuLinks = from menu in dbContext.MenuLinks
                                    where menu.MenuLinkTypeID == (int)menuLinkType && menu.SiteID == siteID
                                    orderby menu.SortOrder
                                    select new MenuDetails()
                                    {
                                        MenuLinkIID = menu.MenuLinkIID,
                                        MenuLinkTypeID = menu.MenuLinkTypeID,
                                        MenuIcon = menu.MenuIcon,
                                        MenuName = menu.MenuName,
                                        MenuTitle = menu.MenuTitle,
                                        ActionLink = menu.ActionLink,
                                        ActionLink1 = menu.ActionLink1,
                                        ActionLink2 = menu.ActionLink2,
                                        ActionLink3 = menu.ActionLink3,
                                        ParentMenuID = menu.ParentMenuID,
                                        SortOrder = menu.SortOrder,
                                        Parameters = menu.Parameters
                                    };

                    return menuLinks.AsNoTracking().ToList();
                }
                else
                {
                    var menuLinks = from menu in dbContext.MenuLinks
                                    join menuCulture in dbContext.MenuLinkCultureDatas on menu.MenuLinkIID equals menuCulture.MenuLinkID
                                    join cultures in dbContext.Cultures on menuCulture.CultureID equals cultures.CultureID
                                    where menu.MenuLinkTypeID == (int)menuLinkType && cultures.CultureCode == cultureCode && menu.SiteID == siteID
                                    orderby menu.SortOrder
                                    select new MenuDetails()
                                    {
                                        MenuLinkIID = menu.MenuLinkIID,
                                        MenuLinkTypeID = menu.MenuLinkTypeID,
                                        MenuIcon = menu.MenuIcon,
                                        MenuName = menuCulture.MenuName,
                                        MenuTitle = menuCulture.MenuTitle,
                                        ActionLink = menu.ActionLink,
                                        ActionLink1 = menu.ActionLink1,
                                        ActionLink2 = menu.ActionLink2,
                                        ActionLink3 = menu.ActionLink3,
                                        ParentMenuID = menu.ParentMenuID,
                                        SortOrder = menu.SortOrder,
                                        Parameters = menu.Parameters
                                    };
                    return menuLinks.AsNoTracking().ToList();
                }

            }
        }

        public List<Services.Contracts.MenuLinks.MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, int cultureID)
        {
            List<MenuDTO> MenuDTOList = new List<MenuDTO>();
            dbEduegateERPContext db = new dbEduegateERPContext();

            // get all menu
            var qryMenu = (from m in db.MenuLinks
                           join cultureData in db.MenuLinkCultureDatas on m.MenuLinkIID equals cultureData.MenuLinkID into map
                           from cultureData in map.DefaultIfEmpty()
                           where m.ParentMenuID == null && m.MenuLinkTypeID == (int)menuLinkType && cultureData.MenuLinkID == cultureID
                           select new
                           {
                               m.MenuLinkIID,
                               m.ParentMenuID,
                               MenuName = (cultureData == null ? m.MenuName : cultureData.MenuName),
                               m.MenuLinkTypeID,
                               m.ActionLink,
                               m.ActionLink1,
                               m.ActionLink2,
                               m.ActionLink3,
                               m.SortOrder,
                               m.Parameters
                           }).AsNoTracking().ToList();

            // get all Submenu
            var qrySubMenu = (from m in db.MenuLinks
                              join cultureData in db.MenuLinkCultureDatas on m.MenuLinkIID equals cultureData.MenuLinkID into map
                              from cultureData in map.DefaultIfEmpty()
                              where m.ParentMenuID != null && m.MenuLinkTypeID == (int)menuLinkType && cultureData.MenuLinkID == cultureID
                              select new
                              {
                                  m.MenuLinkIID,
                                  m.ParentMenuID,
                                  MenuName = (cultureData == null ? m.MenuName : cultureData.MenuName),
                                  m.MenuLinkTypeID,
                                  m.ActionLink,
                                  m.ActionLink1,
                                  m.ActionLink2,
                                  m.ActionLink3,
                                  m.SortOrder,
                                  m.Parameters
                              }).ToList();

            // get all category
            var qryCategory = (from c in db.Categories
                               join mc in db.MenuLinkCategoryMaps on c.CategoryIID equals mc.CategoryID
                               select new
                               {
                                   mc.MenuLinkID,
                                   mc.SortOrder,
                                   mc.ActionLink,
                                   c.CategoryIID,
                                   c.ParentCategoryID,
                                   c.CategoryName,
                                   c.CategoryCode,
                                   c.ThumbnailImageName,
                               }).ToList();

            // get all Brand(Designer)
            var qryBrand = (from b in db.Brands
                            join mb in db.MenuLinkBrandMaps on b.BrandIID equals mb.BrandID
                            orderby mb.SortOrder
                            select new
                            {
                                b.BrandIID,
                                b.BrandName,
                                mb.MenuLinkID,
                                mb.ActionLink,
                                mb.SortOrder,
                            }).ToList();

            // for each for all menu
            qryMenu.ToList().ForEach(x =>
            {
                Services.Contracts.MenuLinks.MenuDTO dto = new MenuDTO();
                dto.SubMenuList = new List<SubMenuDTO>();
                dto.MenuCategoryList = new List<MenuCategoryDTO>();
                dto.BrandList = new List<BrandDTO1>();
                dto.MenuLinkIID = x.MenuLinkIID;
                dto.ParentMenuID = x.ParentMenuID;
                dto.MenuName = x.MenuName;
                dto.MenuLinkTypeID = x.MenuLinkTypeID;
                dto.ActionLink = x.ActionLink;
                dto.ActionLink1 = x.ActionLink1;
                dto.ActionLink2 = x.ActionLink2;
                dto.ActionLink3 = x.ActionLink3;
                dto.SortOrder = x.SortOrder;
                dto.Parameters = x.Parameters;

                qrySubMenu.Where(y => y.ParentMenuID == x.MenuLinkIID).ToList().ForEach(y =>
                {
                    SubMenuDTO _SubMenuDTO = new SubMenuDTO();
                    _SubMenuDTO.MenuLinkID = (long)y.MenuLinkIID;
                    _SubMenuDTO.SortOrder = y.SortOrder;
                    _SubMenuDTO.ActionLink = y.ActionLink;
                    _SubMenuDTO.SubMenuID = (long)y.ParentMenuID;
                    _SubMenuDTO.SubMenuParentID = 0;
                    _SubMenuDTO.SubMenuName = y.MenuName;
                    // add category
                    dto.SubMenuList.Add(_SubMenuDTO);
                });

                // filter based on menuid in category
                qryCategory.Where(y => y.MenuLinkID == x.MenuLinkIID).ToList().ForEach(y =>
                {
                    MenuCategoryDTO _MenuCategoryDTO = new MenuCategoryDTO();
                    _MenuCategoryDTO.MenuLinkID = (long)y.MenuLinkID;
                    _MenuCategoryDTO.SortOrder = y.SortOrder;
                    _MenuCategoryDTO.ActionLink = y.ActionLink;
                    _MenuCategoryDTO.CategoryIID = y.CategoryIID;
                    _MenuCategoryDTO.ParentCategoryID = y.ParentCategoryID;
                    _MenuCategoryDTO.CategoryCode = y.CategoryCode;
                    _MenuCategoryDTO.CategoryName = y.CategoryName;
                    _MenuCategoryDTO.ThumbnailImageName = y.ThumbnailImageName;
                    // add category
                    dto.MenuCategoryList.Add(_MenuCategoryDTO);
                });

                // filter based on menuid in brands
                qryBrand.Where(b => b.MenuLinkID == x.MenuLinkIID).ToList().ForEach(b =>
                {
                    SubMenuDTO _SubMenuDTO = new SubMenuDTO();
                    _SubMenuDTO.MenuLinkID = (long)b.MenuLinkID;
                    _SubMenuDTO.SubMenuID = b.BrandIID;
                    _SubMenuDTO.SubMenuParentID = 0;
                    _SubMenuDTO.SubMenuName = b.BrandName;
                    _SubMenuDTO.ActionLink = b.ActionLink;
                    _SubMenuDTO.SortOrder = b.SortOrder;
                    // add brands
                    dto.SubMenuList.Add(_SubMenuDTO);
                });

                // add whole object in list of MenuDTO
                MenuDTOList.Add(dto);
            });
            return MenuDTOList;
        }

        public List<MenuLinkCategoryMap> GetMenuLinkCategoryMaps(Int64 menuLinkID)
        {
            //List<MenuDTO> menuList = new List<MenuDTO>();
            List<MenuLinkCategoryMap> menuLinkCategoryMapList = new List<MenuLinkCategoryMap>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                menuLinkCategoryMapList = db.MenuLinkCategoryMaps.Where(a => a.MenuLinkID == menuLinkID).OrderBy(b => b.SortOrder).AsNoTracking().ToList<MenuLinkCategoryMap>();
            }
            return menuLinkCategoryMapList;
        }

        public string GetCultureCode(int cultureID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Cultures.Where(x => x.CultureID == cultureID).Select(x => x.CultureCode).AsNoTracking().FirstOrDefault();
            }
        }
    }
}
