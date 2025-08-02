using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
   public class ProductListViewModel : BaseViewModel
    {
       public ProductListViewModel(string filterBy = "")
       {
           Catalogs = new List<CatalogListViewModel>() { new CatalogListViewModel() };
           FacetItems = new List<FacetListViewModel>();
           DidYouMean = false;
           IsCategoryPage = false;

           string[] additionalFilters = null;

           if(!string.IsNullOrEmpty(filterBy) && filterBy.Contains("addFilter"))
               additionalFilters = filterBy.Substring(filterBy.IndexOf("addFilter:") + 10).Split(',');

           if (new Domain.Setting.SettingBL().GetSettingValue<int>("SiteID") == 1)
           {
               AdditionalFilters = new List<AdditionalQuickFilter>() { 
               new AdditionalQuickFilter() { Field = "deliverytypes", FilterName = "supexpr", FilterLabel = Eduegate.Globalization.ResourceHelper.GetValue("SuperExpressDeliveryCaption"), Value = GetFilterDefaultValue(additionalFilters, "supexpr"), FilterValue="2_Super Express", FilterType = "OR"} ,
               new AdditionalQuickFilter() { Field = "deliverytypes", FilterName = "expr", FilterLabel = Eduegate.Globalization.ResourceHelper.GetValue("ExpressDeliveryCaption"), Value = GetFilterDefaultValue(additionalFilters, "expr"), FilterValue="1_Express", FilterType = "OR"},
               new AdditionalQuickFilter() { Field="discount", FilterName = "offer", FilterLabel =  Eduegate.Globalization.ResourceHelper.GetValue("OffersCaption"), Value = GetFilterDefaultValue(additionalFilters, "offer"), FilterValue="true", FilterType = "OR"},
               //new AdditionalQuickFilter() {Field="isdeal", FilterName = "deal", FilterLabel = Eduegate.Globalization.ResourceHelper.GetValue("DealsCaption"), Value = GetFilterDefaultValue(additionalFilters, "deal"), FilterValue="true", FilterType = "OR"},
               new AdditionalQuickFilter() { Field = "stockcheck", FilterName = "xoos", FilterLabel = Eduegate.Globalization.ResourceHelper.GetValue("OOSCaption"), 
                   FilterValue="1", FilterType = "AND", Value = GetFilterDefaultValue(additionalFilters, "xoos") , 
                   IsDirty = GetFilterDefaultValue(additionalFilters, "xoos") ? true : false},
               };
           }
           else
           {
               AdditionalFilters = new List<AdditionalQuickFilter>() { 
               new AdditionalQuickFilter() { Field="discount", FilterName = "offer", FilterLabel = "Offers", Value = GetFilterDefaultValue(additionalFilters, "offer"), FilterValue="true", FilterType = "OR"},
               //new AdditionalQuickFilter() {Field="isdeal", FilterName = "deal", FilterLabel = "Deals", Value = GetFilterDefaultValue(additionalFilters, "deal"), FilterValue="true", FilterType = "OR"},
               new AdditionalQuickFilter() { Field = "stockcheck", FilterName = "xoos", FilterLabel = "Exclude sold-out", 
                   FilterValue="1", FilterType = "AND", Value = GetFilterDefaultValue(additionalFilters, "xoos"), 
                   IsDirty = GetFilterDefaultValue(additionalFilters, "xoos") ? true : false },
               };
           }

       }
       
       public  string SearchText { get; set; }
       public string FilterBy { get; set; }
       public string SortBy { get; set; }
       public bool IsCategoryPage { get; set; }

       public List<CatalogListViewModel> Catalogs { get; set; }
       public List<FacetListViewModel> FacetItems {get; set;}
       public List<AdditionalQuickFilter> AdditionalFilters { get; set; }

       public Int32 TotalProductsCount { get; set; }
       public decimal SliderMaxPrice { get; set; }
       public bool DidYouMean { get; set; }
       public string SearchedText { get; set; }

       private bool GetFilterDefaultValue(string[] filters, string name){

           if (filters == null) return false;

           var filterExists = filters.FirstOrDefault(a => a == name);
           return filterExists == null ? false : true;
       }

       public static ProductListViewModel FromDTO(SearchResultDTO dto, string filterBy)
       {
           var productVM = new ProductListViewModel(filterBy)
            {
                Catalogs = CatalogListViewModel.FromDTO(dto.Catalogs),
                FacetItems = FacetListViewModel.FromDTO(dto.FacetsDetails),
                SliderMaxPrice = dto.SliderMaxPrice,
                SearchedText = dto.SearchedText
            };          

           //setting the default selection when it's populate for the first time.
           if (filterBy.IsNotNullOrEmpty())
           {
               foreach (var filterFacet in filterBy.Split('|'))
               {
                   if (filterFacet.IsNotNullOrEmpty() && filterFacet.IndexOf(':') > -1)
                   {
                       var facetName = filterFacet.Substring(0, filterFacet.IndexOf(':'));
                       facetName = facetName == "brands" ? "brandcode" : facetName;

                       foreach (var facetCode in filterFacet.Substring(filterFacet.IndexOf(':') + 1).Split(','))
                       {
                           if (facetCode.IsNotNullOrEmpty())
                           {
                               var facet = productVM.FacetItems.FirstOrDefault(a => a.FacetCode == facetName);

                               if(facet != null)
                               {
                                   var facetItem = facet.FacetItems.FirstOrDefault(a=> a.Code == facetCode);

                                   if (facetItem != null)
                                       facetItem.IsSelected = true;
                               }
                           }
                       }
                   }
               }
           }

           return productVM;
       }

       public static SearchParameterDTO ToDTO(int pageIndex, int pageSize, string searchText, string filterBy, string sortBy, string priceTag, CallContext _callContext)
       {
           pageIndex = pageIndex <= 0 ? 1 : pageIndex;
           searchText = string.IsNullOrEmpty(searchText) ? "*:*" : searchText;

           var searchSolrParameter = new SearchParameterDTO()
           {
               FreeSearch = searchText,
               PageIndex = pageIndex,
               PageSize = pageSize,
               Language = _callContext.LanguageCode,
               Sort = sortBy,
               ConversionRate = 1,
               SiteID = new Domain.Setting.SettingBL().GetSettingValue<int>("SiteID"),
               Facets = new Dictionary<string, string>(),
           };

           if (filterBy.IsNotNullOrEmpty())
           {
               foreach (var filterType in filterBy.Split('|'))
               {
                   if (filterType.IsNotNullOrEmpty() && filterType.IndexOf(':') > -1)
                   {
                       var filterName = filterType.Substring(0, filterType.IndexOf(':'));

                       if (filterName.Equals("addFilter"))
                       {
                           var clientFilters = filterType.Substring(filterType.IndexOf(':') + 1).Split(',');
                           var prdVM = new ProductListViewModel();

                           foreach (var addFilter in prdVM.AdditionalFilters)
                           {
                               var additioanlFilter = clientFilters.FirstOrDefault(a => a == addFilter.FilterName);

                               if (!string.IsNullOrEmpty(additioanlFilter) || addFilter.IsMandatory)
                               {
                                   switch (addFilter.FilterType)
                                   {
                                       case "AND":
                                           {
                                               var extraParam = searchSolrParameter.ExtraAndParams.FirstOrDefault(a => a.Key.Equals(addFilter.Field));

                                               if (extraParam.Key.IsNull())
                                                   searchSolrParameter.ExtraAndParams.Add(addFilter.Field, addFilter.FilterValue); //assume that all are boolean for now as an integer
                                               else
                                                   searchSolrParameter.ExtraAndParams[extraParam.Key] = extraParam.Value + "," + addFilter.FilterValue;
                                           }
                                           break;
                                       case "OR":
                                           {
                                               var extraParam = searchSolrParameter.ExtraOrParams.FirstOrDefault(a => a.Key.Equals(addFilter.Field));

                                               if (extraParam.Key.IsNull())
                                                   searchSolrParameter.ExtraOrParams.Add(addFilter.Field, addFilter.FilterValue); //assume that all are boolean for now as an integer
                                               else
                                                   searchSolrParameter.ExtraOrParams[extraParam.Key] = extraParam.Value + "," + addFilter.FilterValue;
                                           }
                                           break;
                                   }
                               }
                           }

                           continue;
                       }
                      
                       filterName = filterName == "brands" ? "brandcode" : filterName == "cat" ? "categorycode" : filterName;

                       foreach (var facetCode in filterType.Substring(filterType.IndexOf(':') + 1).Split(','))
                       {
                           if (facetCode.IsNotNullOrEmpty())
                           {
                               var facet = searchSolrParameter.Facets.FirstOrDefault(a => a.Key == filterName);

                               if (facet.Equals(default(KeyValuePair<string, string>)))
                                   searchSolrParameter.Facets.Add(filterName, facetCode);
                               else
                                   searchSolrParameter.Facets[facet.Key] = facet.Value + "," + facetCode;
                           }
                       }
                   }
               }
           }

           if (priceTag.IsNotNullOrEmpty())
           {
               searchSolrParameter.RangeParameters.Add("discountprice", priceTag);
           }

           return searchSolrParameter;
       }
    }
}
