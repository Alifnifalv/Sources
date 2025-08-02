using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Catalog;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Domain.Mappers.ShoppingCartMapper
{
    public class CartMapper : IDTOEntityMapper<CartDTO, ShoppingCart>
    {
        private CallContext _context; 
        private string ImageRootUrl = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageRootUrl");

        public static CartMapper Mapper(CallContext context)
        {
            var mapper = new CartMapper();
            mapper._context = context;
            return mapper;
        }
        public CartDTO ToDTO(ShoppingCart entity)
        {
            if (entity != null)
            {
                var voucherMap = entity.ShoppingCartVoucherMaps != null ? entity.ShoppingCartVoucherMaps.FirstOrDefault() : null;
                return new CartDTO()
                {
                    ShoppingCartID = entity.ShoppingCartIID,
                    CustomerID = long.Parse(entity.CartID),
                    BillingAddressID = entity.BillingAddressID,
                    ShippingAddressID = entity.ShippingAddressID,
                    VoucherID = voucherMap == null ? (long?)null : voucherMap.VoucherID,
                };
            }
            else return new CartDTO();
        }

        public ShoppingCart ToEntity(CartDTO dto)
        {
            if (dto != null)
            {
                return new ShoppingCart()
                {

                };
            }
            else return new ShoppingCart();
        }

        public Services.Contracts.SearchData.SearchResultDTO OnlineStoreProductSKUSearch(int pageIndex, int pageSize, string searchText, string searchVal,
        string searchBy, string sortBy, string pageType, bool isCategory, long? customerID, short? statusID = 2)
        {
            //long totalRecords = 0;
            var searchMethod = new SettingBL(_context).GetSettingValue<string>("SEARCHMETHOD", _context.CompanyID.Value, "db");

            var productSKUs = searchMethod == "db" ? new ProductCatalogRepository().OnlineStoreProductSKUSearch(pageIndex, pageSize, searchText, searchVal,
                searchBy, sortBy, pageType, isCategory, out int totalRecords, _context != null ? _context.LanguageCode : null, customerID, statusID) : null;

            totalRecords = productSKUs.Count();
            var searchDTO = new Services.Contracts.SearchData.SearchResultDTO();
            searchDTO.Catalogs = new List<SearchCatalogDTO>();
            searchDTO.CatalogGroups = new List<SearchCatalogGroupDTO>();

            string skuString = productSKUs.Count == 0 ? null : productSKUs.AsEnumerable()
                     .Select(row => row.ProductSKUMapIID.ToString())
                     .Aggregate((s1, s2) => string.Concat(s1, "," + s2));

            var isIncludeOutOfStock = new SettingBL(_context).GetSettingValue<bool>("INCLUDEOUTOFSTOCK",
                _context.CompanyID.Value, true);

            var productInventories = skuString == null ? null : new ProductDetailBL(_context)
                .GetProductInventoryOnline(skuString, _context == null || _context.UserId.IsNull()
                ? (long?)null : Convert.ToInt64(_context.UserId));

            var skus = productSKUs.Count == 0 ? null : productSKUs.AsEnumerable()
                     .Select(row => long.Parse(row.ProductSKUMapIID.ToString())).ToList();

            var allImages = skus != null && skus.Count() > 0 && searchMethod == "db" ? OnlineStoreGetProductImages(skus) : null;

            foreach (var sku in productSKUs)
            {
                var inventorySKU = productInventories == null ? null : productInventories
                    .Where(a => a.ProductSKUMapID == long.Parse(sku.ProductSKUMapIID.ToString()))
                    .FirstOrDefault();

                bool skuWhishlits = new ProductBL(_context).GetProductWishList(sku.SKUID,customerID);

                if (!isIncludeOutOfStock)
                {
                    if (inventorySKU == null || inventorySKU.Quantity == 0 || inventorySKU.ProductPricePrice <= 0) continue;
                }

                string thumbNail;
                string listImage;
                string largeImage;

                if (string.IsNullOrEmpty(sku.ProductThumbnail))
                {
                    var productImages = allImages
                        .Where(x => x.ProductSKUMapID == long.Parse(sku.ProductSKUMapIID.ToString()))
                        .FirstOrDefault();
                    thumbNail = productImages == null ? null : productImages.ThumbnailImage;
                    listImage = productImages == null ? null : productImages.ListingImage;
                    largeImage = productImages == null ? null : productImages.ZoomImage;
                }
                else
                {
                    thumbNail = sku.ProductThumbnail;
                    listImage = sku.ProductListingImage;
                    largeImage = sku.ProductLargeImage;
                }

                var catalog = new SearchCatalogDTO()
                {
                    IsActive = sku.IsActive,
                    ProductName = sku.ProductName,
                    ProductID = sku.ProductID,
                    SKUID = sku.SKUID,
                    ProductThumbnail = thumbNail == null ? "img/noimage5.png" : string.Format("{0}/Products/{1}", ImageRootUrl, thumbNail),
                    ProductListingImage = listImage == null ? "img/noimage5.png" : string.Format("{0}/Products/{1}", ImageRootUrl, listImage),
                    ProductLargeImage = largeImage == null ? "img/noimage5.png" : string.Format("{0}/Products/{1}", ImageRootUrl, largeImage),
                    CurrencyCode = _context == null ? null : _context.CurrencyCode,
                    BrandCode = sku.BrandCode,
                    BrandID = sku.BrandID == null ? 0 : (long)sku.BrandID,
                    CategoryCode = sku.CategoryCode,
                    CategoryID = sku.CategoryID,
                    AdditionalInfo1 = sku.AdditionalInfo1,
                    AdditionalInfo2 = sku.AdditionalInfo2,
                    ProductDescription = sku.ProductDescription,
                    AlertMessage = sku.AlertMessage,
                    IsWishList = skuWhishlits,
                };

                if (inventorySKU != null)
                {
                    catalog.ProductPrice = inventorySKU.ProductPricePrice;
                    catalog.ProductDiscountPrice = inventorySKU.ProductDiscountPrice;
                    catalog.ProductAvailableQuantity = int.Parse(inventorySKU.Quantity.ToString());
                }

                searchDTO.CatalogGroups.Add(new SearchCatalogGroupDTO()
                {
                    Catalogs = new List<SearchCatalogDTO>() {
                       catalog
                    },

                    SelectedCatalog = catalog,
                    CatalogCount = 1,
                });
            }

            if (pageIndex == 1)
            {
                searchDTO.FacetsDetails = new List<FacetsDetail>();
                var filterCategory = searchVal == null || searchVal.Contains("skutags")
                    || !searchVal.Contains("Category") || string.IsNullOrEmpty(searchVal)
                    ? null : searchVal.Split(':')[1].Split(',')
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(x => (long?)Convert.ToInt64(x));

                var studFacet = new FacetsDetail() { FaceItems = new List<FacetItem>(), Name = "Student" };

                var students = new ProductDetailRepository().GetStudentsSiblings((long)_context.LoginID);

                foreach (var stud in students)
                {
                    var facet = new FacetItem()
                    {
                        code = stud.AdmissionNumber,
                        key = stud.FirstName + " " + stud.MiddleName + " " + stud.LastName,
                        value = Convert.ToInt32(stud.StudentIID),
                        FaceItems = new List<FacetItem>()
                    };

                    studFacet.FaceItems.Add(facet);
                }

                searchDTO.FacetsDetails.Add(studFacet);


                var categoryFacet = new FacetsDetail() { FaceItems = new List<FacetItem>(), Name = "Category" };
                var categories = GetCachedCategories();

                var childCategories = filterCategory == null ? categories.Where(x => !x.ParentCategoryID.HasValue)
                    : categories.Where(x => filterCategory.Contains(x.ParentCategoryID));

                var parentCategories = filterCategory == null ? categories.Where(x => !x.ParentCategoryID.HasValue)
                  : categories.Where(x => filterCategory.Contains(x.CategoryIID));

                parentCategories.ToList().ForEach(x => x.CategoryName = !x.CategoryName.Contains("All") ? "All(" + x.CategoryName + ")" : x.CategoryName);

                var allCategories = new List<CategoryDetailDTO>(parentCategories);
                allCategories.AddRange(childCategories);

                foreach (var category in allCategories)
                {
                    var categoryImage = category.CategoryImageMapList
                        .Where(x => x.ImageTypeID == 13)
                        .FirstOrDefault();

                    var imageType = "Category_sliding";

                    if (categoryImage == null)
                    {
                        categoryImage = category.CategoryImageMapList
                            .FirstOrDefault();

                        if (categoryImage != null)
                        {
                            imageType = "ThumbnailImage";
                        }
                    }

                    var categoryCultureData = _context == null || string.IsNullOrEmpty(_context.LanguageCode) || category.CategoryCultureDatas == null ? null : category.CategoryCultureDatas
                        .Where(x => x.CultureCode == _context.LanguageCode)
                        .FirstOrDefault();
                    var categoryName = categoryCultureData == null || string.IsNullOrEmpty(categoryCultureData.CategoryName) ? category.CategoryName : categoryCultureData.CategoryName;
                    var imageFile = categoryImage != null && !string.IsNullOrEmpty(categoryImage.ImageFile)
                            ? string.Format("{0}/Category/{1}/{2}/{3}", ImageRootUrl, category.CategoryIID, imageType, categoryImage.ImageFile) : null;

                    var facet = new FacetItem()
                    {
                        code = category.CategoryIID.ToString(),
                        key = categoryName,
                        value = int.Parse(category.CategoryIID.ToString()),
                        ItemImage = imageFile,
                        FaceItems = new List<FacetItem>()
                    };

                    BuildCategoryTree(facet, categories.Where(x => x.ParentCategoryID == category.CategoryIID).ToList());
                    categoryFacet.FaceItems.Add(facet);
                }

                searchDTO.FacetsDetails.Add(categoryFacet);
                var brands = GetCachedBrands();
                var catalogGroup = searchDTO.CatalogGroups.FirstOrDefault();
                if (catalogGroup != null && catalogGroup.Catalogs != null)
                {
                    brands = brands.Where(x => catalogGroup.Catalogs.Select(y => y.BrandID).Contains(x.BrandIID)).ToList();
                }

                var brandFacet = new FacetsDetail() { FaceItems = new List<FacetItem>(), Name = "Brand" };

                foreach (var brand in brands.Take(20))
                {
                    var brandImage = brand.ImageMaps.Where(x => x.ImageTypeID == 13).FirstOrDefault();
                    var facet = new FacetItem()
                    {
                        code = brand.BrandCode,
                        key = brand.BrandName,
                        value = int.Parse(brand.BrandIID.ToString()),
                        ItemImage = brandImage != null ? string.Format("{0}/Brand/Sliding/{1}", ImageRootUrl, brandImage.ImageFile) : null,
                        FaceItems = new List<FacetItem>()
                    };

                    brandFacet.FaceItems.Add(facet);
                }

                searchDTO.FacetsDetails.Add(brandFacet);
            }

            searchDTO.TotalProductsCount = Convert.ToInt32(totalRecords);
            return searchDTO;
        }

        private List<CategoryDetailDTO> GetCachedCategories()
        {
            var categories = Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>.Get("CATEGORIES");

            if (categories == null)
            {
                categories = CategoryDetailMapper.Mapper(_context).ToDTOList(new ProductCatalogRepository().OnlineStoreGetCategory(), true);
                Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>.Add(categories, "CATEGORIES");
            }

            return categories;
        }

        private void BuildCategoryTree(FacetItem facet, List<CategoryDetailDTO> categories)
        {
            foreach (var category in categories)
            {
                var categoryCultureData = _context == null ||
                         string.IsNullOrEmpty(_context.LanguageCode) || category.CategoryCultureDatas == null ? null : category.CategoryCultureDatas
                        .Where(x => x.CultureCode == _context.LanguageCode)
                        .FirstOrDefault();
                var categoryName = categoryCultureData == null || string.IsNullOrEmpty(categoryCultureData.CategoryName) ? category.CategoryName : categoryCultureData.CategoryName;

                var facetItem = new FacetItem()
                {
                    code = category.CategoryCode,
                    key = categoryName,
                    value = int.Parse(category.CategoryIID.ToString())
                };

                facet.FaceItems.Add(facetItem);
            }
        }

        private List<BrandDTO> GetCachedBrands()
        {
            var brands = Framework.CacheManager.MemCacheManager<List<BrandDTO>>.Get("BRANDS");

            if (brands == null)
            {
                brands = BrandMapper.Mapper(_context).ToDTO(new ProductCatalogRepository().GetActiveBrands());
                Framework.CacheManager.MemCacheManager<List<BrandDTO>>.Add(brands, "BRANDS");
            }

            return brands;
        }

        public List<ProductImageMapDTO> OnlineStoreGetProductImages(List<long> skuIDs)
        {
            var missingInCachSkuIds = new List<long>();
            var imageMaps = new List<ProductImageMapDTO>();

            foreach (var sku in skuIDs)
            {
                var map = Framework.CacheManager.MemCacheManager<ProductImageMapDTO>.Get("PRODUCTIMAGES_" + sku.ToString());

                if (map == null)
                {
                    missingInCachSkuIds.Add(sku);
                }
                else
                {
                    imageMaps.Add(map);
                }
            }

            if (missingInCachSkuIds.Count > 0)
            {
                var dbImages = ProductImageMapMapper.Mapper(_context)
                    .ToDTO( new ProductDetailRepository().OnlineStoreGetProductImages(missingInCachSkuIds));

                imageMaps.AddRange(dbImages);
                foreach (var map in dbImages)
                {
                    Framework.CacheManager.MemCacheManager<ProductImageMapDTO>.Add(map, "PRODUCTIMAGES_" + map.ProductSKUMapID.ToString());
                }
            }

            return imageMaps;
        }

        public bool MailSendAfterCanteenOrderGeneration(string admissionNumber, string emailID, string transactioNo)
        {
            MutualRepository mutualRepository = new MutualRepository();

            using (var dbContext = new dbEduegateERPContext())
            {
                var emaildata = new EmailNotificationDTO();

                var studentDetails = dbContext.Students.FirstOrDefault(s => s.AdmissionNumber == admissionNumber);

                var EmailIDs = new[]
                {
                                new { ToEmailID = studentDetails.Parent?.GaurdianEmail }
                                }.ToList();

                foreach (var email in EmailIDs)
                {
                    if (email.ToEmailID != null)
                    {
                        emailID = email.ToEmailID;

                        String emailDetails = "";
                        String emailSub = "";

                        var TCEmailBody = new SettingRepository().GetSettingDetail("CANTEEN_CONFIRMATION_MAIL").SettingValue;
                        var emailSubject = new SettingRepository().GetSettingDetail("CANTEEN_CONFIRMATION_MAIL_SUBJECT").SettingValue;

                        emailSub = emailSubject.Replace("{OrderNo}", transactioNo);

                        emailDetails = TCEmailBody;
                        var className = studentDetails.Class?.ClassDescription + ' ' + studentDetails.Section?.SectionName;

                        var student = studentDetails.AdmissionNumber + "-" + studentDetails.FirstName + " " + studentDetails.MiddleName + " " + studentDetails.LastName;

                        emailDetails = emailDetails.Replace("{student}", student);
                        emailDetails = emailDetails.Replace("{Order Number}", transactioNo);
                        emailDetails = emailDetails.Replace("{class}", className);


                        try
                        {
                            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
                            String replaymessage = PopulateBody(emailID, emailDetails);

                            var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                            string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                            if (emailDetails != "")
                            {
                                if (hostDet == "Live")
                                {
                                    SendMail(emailID, emailSub, replaymessage, hostUser);
                                }
                                else
                                {
                                    SendMail(defaultMail, emailSub, replaymessage, hostUser);
                                }
                            }

                        }
                        catch { }
                    }
                }

            }

            return true;

        }

        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader("http://erp.eduegate.com/emailtemplate.html"))
            //{
            //    body = reader.ReadToEnd();
            //}
            body = "<!DOCTYPE html> <html> <head> <title></title> <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1'> <meta http-equiv='X-UA-Compatible' content='IE=edge' /> <style type='text/css'> </style> </head> <body style='background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;'> <!-- HIDDEN PREHEADER TEXT --> <table border='0' cellpadding='0' cellspacing='0' width='100%'> <!-- LOGO --> <tr> <td bgcolor='#bd051c' align='center'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td align='center' valign='top' style='padding: 40px 10px 40px 10px;'> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#bd051c' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='center' valign='top' style='padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;'> <div align='center' style='width:100%;display:inline-block;text-align:center;'><img src='https://parent.pearlschool.org/img/podarlogo_mails.png' style='height:70px;margin:1rem;' /></div> </td> </tr> </table> </td> </tr> <tr> <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;'> <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'> <tr> <td bgcolor='#ffffff' align='left' style='padding-left: 1rem; color: #666666; font-family: Lato, Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 25px;'>{HTMLMESSAGE}</td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='black' align='center' style='padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #fffefe; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;'> <div class='copyrightdiv' style='color: white;padding: 30px 30px 30px 30px;' >Copyright &copy; {YEAR}<a style='text-decoration: none' target='_blank' href='http://pearlschool.org/' > <span style='color: white; font-weight: bold;' >&nbsp;&nbsp; PEARL SCHOOL</span > </a > </div > </td > </tr > </table > </td > </tr > <tr > <td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px;' > <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;' > <tr > <td bgcolor='#f4f4f4' align='left' style='padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;' > <br > <div class='PoweredBy' style='text-align:center;' > <div style='padding-bottom:1rem;' > Powered By: <a style='text-decoration: none; color: #0c7aec;' id='eduegate' href='https://softopsolutionpvtltd.com/' target='_blank' > SOFTOP SOLUTIONS PVT LTD</a > </div > <a href='https://www.facebook.com/pearladmin1/' > <img src='https://parent.pearlschool.org/Images/logo/fb-logo.png' alt='facebook' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.linkedin.com/company/pearl-school-qatar/?viewAsMember=true' > <img src='https://parent.pearlschool.org/Images/logo/linkedin-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.instagram.com/pearlschool_qatar/' > <img src='https://parent.pearlschool.org/Images/logo/insta-logo.png' alt='instagram' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > <a href='https://www.youtube.com/channel/UCFQKYMivtaUgeSifVmg79aQ' > <img src='https://parent.pearlschool.org/Images/logo/youtube-logo.png' alt='twitter' width='30' height='25' border='0' style='margin-right:.5rem;' / > </a > </div > </td > </tr > </table > </td > </tr > </table > </body > </html >";
            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }

        public void SendMail(string email, string subject, string msg, string mailname)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();
                var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

                SmtpClient ss = new SmtpClient();
                ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailname);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {

                //lb_error.Visible = true;
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


            //return Json("ok", JsonRequestBehavior.AllowGet);
        }

    }
}
