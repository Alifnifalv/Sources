using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Services;
using WB.SearchServer.Core.Model;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.SearchData;
using System.Web;
using Eduegate.Framework.Helper;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Domain.Mappers;

namespace Eduegate.Domain
{
    public class SearchDataBL
    {
        private CallContext _callContext { get; set; }

        public SearchDataBL(CallContext context)
        {
            _callContext = context;          
        }

        private int GetCultureID()
        {
            var cultureID = 1;
            if (_callContext != null && !string.IsNullOrEmpty(_callContext.LanguageCode))
            {
                var languageDetails = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode);
                if (languageDetails != null)
                {
                    cultureID = languageDetails.CultureID;
                }
            }

            return cultureID;
        }

        public SearchResultDTO GetProductCatalog(string searchText)
        {
            var result = new SearchResultDTO() { Catalogs = new List<SearchCatalogDTO>() };
            var products = (new WB.SearchServer.Core.Handlers.CatalogHandler(null)).Search(searchText);
            foreach (var product in products)
            {
                var productInventory = new ProductInventoryBranchDTO();
                result.Catalogs.Add(ToDTO(product, 1, productInventory, GetCultureID()));
            }
            return result;
        }

        public SearchResultDTO GetProductCatalog(SearchParameterDTO parameter)
        {
            parameter.CustomerID = _callContext != null && !string.IsNullOrEmpty(_callContext.UserId) ? long.Parse(_callContext.UserId) : 0;

            var result = new SearchResultDTO()
            {
                Catalogs = new List<SearchCatalogDTO>(),
                FacetsDetails = new List<FacetsDetail>()
            };

            var products = (dynamic)null;
            result.SearchedText = parameter.FreeSearch;

            //Build catalog
            switch (parameter.SiteID)
            {
                case 1: //Kuwait
                default:
                    products = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandler(null)).SearchView(parameter)
                        : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerAr(null)).SearchView(parameter);

                    if (products.Products.Count == 0)
                    {
                        //get the suggestions
                        var suggestions = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandler(null)).AutoSearchView(parameter.FreeSearch)
                            : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerAr(null)).AutoSearchView(parameter.FreeSearch);

                        if (suggestions.Count > 0)
                        {
                            parameter.FreeSearch = suggestions[0];
                            products = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandler(null)).SearchView(parameter)
                                : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerAr(null)).SearchView(parameter);

                            if (products.Products.Count > 0)
                            {
                                result.SearchedText = parameter.FreeSearch;
                            }
                        }
                    }
                    break;
                case 2: //KSA
                    products = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsa(null)).SearchView(parameter)
                        : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsaAr(null)).SearchView(parameter);

                    if (products.Products.Count == 0)
                    {
                        //get the suggestions
                        var suggestions = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsa(null)).AutoSearchView(parameter.FreeSearch)
                        : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsaAr(null)).AutoSearchView(parameter.FreeSearch);

                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                        {
                            parameter.FreeSearch = suggestions[0];
                            products = parameter.Language == "en" ? (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsa(null)).SearchView(parameter)
                                : (dynamic)(new WB.SearchServer.Core.Handlers.CatalogHandlerKsaAr(null)).SearchView(parameter);

                            if (products.Products.Count > 0)
                            {
                                result.SearchedText = parameter.FreeSearch;
                            }
                        }
                    }

                    break;
            }
            var cultureID = GetCultureID();
          
            var skuString = "";
            foreach (var product in products.Products)
            {
                var productList = (Product)product;
                skuString += productList.SKU + ",";
            }

            if (skuString.Length > 0)
            {
                skuString = skuString.Remove(skuString.Length - 1, 1);
            }

            var productInventoryList = new ProductDetailBL(_callContext).GetProductInventoryOnline(skuString, parameter.CustomerID);

            foreach (var product in products.Products)
            {
                Int64 SKUID = Convert.ToInt64(product.SKU);
                var productInventory = (from productInventoryDetails in productInventoryList
                                        where productInventoryDetails.ProductSKUMapID == SKUID
                                        select productInventoryDetails).FirstOrDefault();
                if (productInventory !=null)
                result.Catalogs.Add(ToDTO((Product)product, parameter.ConversionRate, productInventory, cultureID));
            }
                        
            foreach (var facets in products.Facets)
            {
                try
                {
                    var view = new FacetsDetail()
                    {
                        Name = facets.Key,
                        FaceItems = new List<FacetItem>(),
                    };

                    foreach (var item in facets.Value)
                    {
                        if (item.Key.Contains("_"))
                        {
                            var values = item.Key.Split('_');

                            view.FaceItems.Add(new FacetItem()
                            {
                                key = values.GetValue(1).ToString(),
                                value = item.Value,
                                code = values.GetValue(0).ToString(),
                            });
                        }
                        else
                        {
                            view.FaceItems.Add(new FacetItem()
                            {
                                key = item.Key,
                                value = item.Value,
                                code = "",
                            });
                        }
                    }

                    result.FacetsDetails.Add(view);
                }
                catch { }
            }

            result.TotalProductsCount = products.TotalCount;
            result.SliderMaxPrice = Math.Round(products.SliderMaxPrice);
            return result;
        }

        public SearchCatalogDTO ToDTO(WB.SearchServer.Core.Model.Product entity, decimal BaseCurrencyConversion, ProductInventoryBranchDTO productInventory, int cultureID = 1)
        {
            return new SearchCatalogDTO()
            {
                ProductCode = entity.Code,
                ProductName = entity.Name,
                ProductID = long.Parse(entity.Id),
                ProductPrice = productInventory.ProductPricePrice * BaseCurrencyConversion,
                BrandCode = entity.BrandCode,
                BrandID = long.Parse(entity.BrandID),
                BrandName = entity.BrandName,
                BrandNameEn = entity.BrandName,
                BrandPosition = short.Parse(entity.BrandPosition),
                DisableListing = entity.DisableListing,
                NewArrival = entity.NewArriaval,
             
                ProductAvailableQuantity = (int)productInventory.Quantity,
                ProductCategoryAll = entity.CategoryAll,
                ProductColor = entity.Color == null ? "" : entity.Color,
                ProductDiscountPrice = productInventory.ProductDiscountPrice * BaseCurrencyConversion,
                ProductListingImage = entity.ListingImage,
               
                ProductSoldQty = entity.SoldQuantity,
                ProductThumbnail = entity.ThumbNail,
               
                ProductWeight = entity.Weight == null ? 0 : long.Parse(entity.Weight),
                QuantityDiscount = entity.DiscountQuantity,
                ProductNameAr = entity.Name,
                BrandNameAr = entity.BrandName,
                SKUID = Convert.ToInt64(entity.SKU),
            };
        }

        public SearchCatalogDTO ToDTOResolver(WB.SearchServer.Core.Model.Product entity, decimal BaseCurrencyConversion)
        {
            var legacy = new BlinkLegacyBL(null).GetProductCatalog(long.Parse(entity.Id));
            return new SearchCatalogDTO()
            {
                ProductID = legacy.ProductID,
                ProductCode = legacy.ProductCode,
                ProductName = legacy.ProductName,
                ProductPrice = legacy.ProductPrice * BaseCurrencyConversion,
                ProductDiscountPrice = legacy.ProductDiscountPrice * BaseCurrencyConversion,
                BrandNameEn = legacy.BrandNameEn,
                ProductThumbnail = legacy.ProductThumbnail,
                ProductListingImage = legacy.ProductListingImage,
                ProductColor = legacy.ProductColor,
                BrandID = legacy.BrandID,
                BrandCode = legacy.BrandCode,
                ProductCategoryAll = legacy.ProductCategoryAll,
                BrandPosition = legacy.BrandPosition,
                ProductWeight = legacy.ProductWeight,
                ProductAvailableQuantity = legacy.ProductAvailableQuantity,
                DeliveryDays = legacy.DeliveryDays,
                ProductSoldQty = legacy.ProductSoldQty,
                BrandName = legacy.BrandName,
                NewArrival = legacy.NewArrival,
                ProductMadeIn = legacy.ProductMadeIn,
                ProductModel = legacy.ProductModel,
                ProductWarranty = legacy.ProductWarranty,
                QuantityDiscount = legacy.QuantityDiscount,
                DisableListing = legacy.DisableListing,
                ProductActive = legacy.ProductActive,
                ProductActiveAr = legacy.ProductActiveAr,
            };
        }

        public List<KeywordsDTO> GetBlinkKeywords(string searchtext, string lng, int countryID)
        {
            if (lng == "en")
            {
                var r = new System.Text.RegularExpressions.Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.Compiled);
                searchtext = r.Replace(searchtext, string.Empty);
            }

            string[] array = searchtext.Split(null);
            string finalSearchText = "";
            if (array.Length > 1)
            {
                for (int i = 0; i <= array.Length - 1; i++)
                {
                    if (finalSearchText == "")
                    {
                        finalSearchText = "searchkeyword:*" + array[i] + "*";
                    }
                    else
                    {
                        finalSearchText = finalSearchText + " AND " + "searchkeyword:*" + array[i] + "*";
                    }
                }
            }
            else
            {
                finalSearchText = "searchkeyword:*" + searchtext + "*";
            }

            if (countryID == 10000)
            {
                if (lng == "en")
                {
                    var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandler(null).Search(finalSearchText);

                    //correct if there is no result
                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandler(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsKsa() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTO(result);
                }
                else if (lng == "ar")
                {
                    var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerAr(null).Search(finalSearchText);

                    //correct if there is no result
                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerAr(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsAr() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTOAr(result);
                }
                else { var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandler(null).Search(finalSearchText);

                     //correct if there is no result
                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerAr(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsAr() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTO(result);
                }
            }
            else
            {
                if (lng == "en")
                {
                    var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsa(null).Search(finalSearchText);

                    //correct if there is no result
                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsa(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsKsa() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTOIntl(result);
                }
                else if (lng == "ar")
                {
                    var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsaAr(null).Search(finalSearchText);

                    //correct if there is no result
                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsaAr(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsKsaAr() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTOArIntl(result);
                }
                else {
                    var result = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsa(null).Search(finalSearchText);

                    if (result.Count == 0)
                    {
                        var suggestions = new WB.SearchServer.Core.Handlers.SearchKeywordsHandlerKsaAr(null).AutoSearchView(searchtext);
                        if (suggestions.Count > 0 && suggestions[0].IndexOf(" ") == -1)
                            result.Add(new KeyWordsKsa() { SearchKeywords = suggestions[0] });
                    }

                    return ToKeywordDTOIntl(result); 
                }

            }
        }


        public List<SearchKeywordsDictionaryDTO> GetBlinkKeywordsDictionary(string searchtext, string lng, bool isFallBack)
        {
            lng = _callContext.LanguageCode;
            if (lng == "en")
            {
                System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.Compiled);
                searchtext = r.Replace(searchtext, string.Empty);
            }

            var mapper = KeywordDictionaryMapper.Mapper(this._callContext);
            var listKeywords = new List<SearchKeywordsDictionaryDTO>();

            if (isFallBack)
            {
                var fallBackBlinkKeywords = GetBlinkKeywords(searchtext, lng, 10003);
                foreach (var key in fallBackBlinkKeywords)
                {
                    var dto = new SearchKeywordsDictionaryDTO();
                    dto.Keywords = key.Keyword;
                    listKeywords.Add(dto);
                }
            }
            else
            {
                var result = new WB.SearchServer.Core.Handlers.CatalogHandler(null).SearchKeywords(searchtext);
                foreach (var key in result)
                {
                    listKeywords.Add(mapper.ToDTO(key));
                }
            }

            return listKeywords;
        }


        public List<KeywordsDTO> GetProductsByKeywords(string searchtext)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.Compiled);
            searchtext = r.Replace(searchtext, string.Empty);
            string[] array = searchtext.Split(null);
            string finalSearchText = "";
            string finalSearchTextBrand = "";
            if (array.Length > 1)
            {
                for (int i = 0; i <= array.Length - 1; i++)
                {
                    if (finalSearchText == "")
                    {
                        finalSearchText = "productkeywordssearch:*" + array[i] + "*";
                        finalSearchTextBrand = "brandname:*" + array[i] + "*";
                    }
                    else
                    {
                        finalSearchText = finalSearchText + " AND " + "productkeywordssearch:*" + array[i] + "*";
                        finalSearchTextBrand = finalSearchTextBrand + " AND " + "brandname:*" + array[i] + "*";
                    }
                }
            }
            else
            {
                finalSearchText = "productkeywordssearch:*" + searchtext + "*";
                finalSearchTextBrand = "brandname:*" + searchtext + "*";
            }
            return ToAutoKeywordDTO(new WB.SearchServer.Core.Handlers.CatalogHandler(null).Search(finalSearchText), searchtext);
        }


        public List<KeywordsDTO> GetProductsByKeywordsAutoSuggest(string keywordtext)
        {
            return ToKeywordDTOAutoSearch(new WB.SearchServer.Core.Handlers.CatalogHandler(null).AutoSearchView(keywordtext));
        }

        public List<KeywordsDTO> ToKeywordDTO(List<KeyWords> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.SearchKeywords,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOAr(List<KeyWordsAr> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.SearchKeywords,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOIntl(List<KeyWordsKsa> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.SearchKeywords,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOIntl(List<string> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword,
                    //LogID = keyword
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOArIntl(List<KeyWordsKsaAr> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword.SearchKeywords,
                    LogID = keyword.LogID
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOArIntl(List<string> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword,
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToKeywordDTOAutoSearch(List<string> keywords)
        {
            var dtos = new List<KeywordsDTO>();
            foreach (var keyword in keywords)
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword,
                    LogID = 0
                });
            }

            return dtos;
        }

        public List<KeywordsDTO> ToAutoKeywordDTO(List<Product> keywords, string searchkeyword)
        {
            var dtos = new List<KeywordsDTO>();
            var KeyWordList = new List<string>();
            var result = new List<string>();

            foreach (var keyword in KeyWordList.Distinct())
            {
                dtos.Add(new KeywordsDTO()
                {
                    Keyword = keyword,
                    LogID = 0
                });
            }

            return dtos;
        }
    }
}
