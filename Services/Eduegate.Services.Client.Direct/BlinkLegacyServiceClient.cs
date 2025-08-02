using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Service.Client
{
    public class BlinkLegacyServiceClient : BaseClient, IBlinkLegacyService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.BLINKLEGACY_SERVICE_NAME);

        public BlinkLegacyServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public long GetTotalKeywords()
        {
            var countProductUri = string.Format("{0}/{1}", productService, "GetTotalKeywords");
            return ServiceHelper.HttpGetRequest<long>(countProductUri, _callContext, _logger);
        }

        public long GetTotalProducts(int companyID)
        {
            var countProductUri = string.Format("{0}/{1}?companyID={2}", productService, "GetTotalProducts", companyID);
            return ServiceHelper.HttpGetRequest<long>(countProductUri, _callContext, _logger);
        }


        public List<Eduegate.Services.Contracts.ProductDTO> GetProducts(int pageNo, int pageSize, int country, int cultureID = 1, int companyID = 1,int siteID=1)
        {
            var uri = string.Format("{0}/{1}?pageNumber={2}&pageSize={3}&country={4}&cultureID={5}&companyID={6}&siteID={7}", productService, "GetProducts", pageNo, pageSize, country, cultureID, companyID, siteID);
            return ServiceHelper.HttpGetRequest<List<Eduegate.Services.Contracts.ProductDTO>>(uri, _callContext, _logger);
        }

        public Eduegate.Services.Contracts.ProductCategoryDTO GetCategory(long categoryID)
        {
            var categoryUri = string.Format("{0}/{1}?categoryID={2}", productService, "GetCategory", categoryID);
            return ServiceHelper.HttpGetRequest<Eduegate.Services.Contracts.ProductCategoryDTO>(categoryUri, _callContext, _logger);
        }

        public Eduegate.Services.Contracts.ProductDTO GetProduct(long productID, int country, int cultureID = 1, int companyID = 1,int siteID=1)
        {
            var uri = string.Format("{0}/{1}?productID={2}&country={3}&cultureID={4}&companyID={5}&siteID={6}", productService, "GetProduct", productID, country, cultureID, companyID, siteID);
            return ServiceHelper.HttpGetRequest<Eduegate.Services.Contracts.ProductDTO>(uri, _callContext, _logger);
        }

        public SearchCatalogDTO GetProductCatalog(long productID)
        {
            var uri = string.Format("{0}/{1}?productID={2}", productService, "GetProductCatalog", productID);
            return ServiceHelper.HttpGetRequest<SearchCatalogDTO>(uri, _callContext, _logger);
        }

        public List<SearchCatalogDTO> GetProductCatalogs(int pageNo, int pageSize)
        {
            var uri = string.Format("{0}/{1}?pageNumber={2}&pageSize={3}", productService, "GetProductCatalogs", pageNo, pageSize);
            return ServiceHelper.HttpGetRequest<List<SearchCatalogDTO>>(uri, _callContext, _logger);
        }

        public List<KeywordsDTO> GetKeywords(int pageNumber, int pageSize,string lng,int country)
        {
            var uri = string.Format("{0}/{1}?pageNumber={2}&pageSize={3}&lng={4}&country={5}", productService, "GetKeywords", pageNumber, pageSize, lng, country);
            return ServiceHelper.HttpGetRequest<List<KeywordsDTO>>(uri, _callContext, _logger);
        }

        public long GetTotalCategories()
        {
            var countProductUri = string.Format("{0}/{1}", productService, "GetTotalCategories");
            return ServiceHelper.HttpGetRequest<long>(countProductUri, _callContext, _logger);
        }

        public List<ProductCategoryDTO> GetCategories(int pageNumber, int pageSize)
        {
            var uri = string.Format("{0}/{1}?pageNumber={2}&pageSize={3}", productService, "GetCategories", pageNumber, pageSize);
            return ServiceHelper.HttpGetRequest<List<ProductCategoryDTO>>(uri, _callContext, _logger);
        }

        public Eduegate.Services.Contracts.ProductCategoryDTO GetCategoryProductLevel(long categoryID, long productID,int country)
        {
            var categoryUri = string.Format("{0}/{1}?categoryID={2}&productID={3}&country={4}", productService, "GetCategoryProductLevel", categoryID, productID, country);
            return ServiceHelper.HttpGetRequest<Eduegate.Services.Contracts.ProductCategoryDTO>(categoryUri, _callContext, _logger);
        }

        public bool UpdateEntityChangeTrackerLog()
        {
            var uri = string.Format("{0}/{1}", productService, "UpdateEntityChangeTrackerLog");
            return ServiceHelper.HttpGetRequest<bool>(uri, null, WriteLog);
        }

        public string CategoryAllList(long productID,int siteID)
        {
            var uri = string.Format("{0}/{1}?productID={2}&siteID={3}", productService, "CategoryAllList", productID, siteID);
            return ServiceHelper.HttpGetRequest<string>(uri, null, WriteLog);
        }

        public Eduegate.Services.Contracts.ProductCategoryDTO GetCategoryCulture(long categoryID, int cultureID)
        {
            var categoryUri = string.Format("{0}/{1}?categoryID={2}&cultureID={3}", productService, "GetCategoryCulture", categoryID, cultureID);
            return ServiceHelper.HttpGetRequest<Eduegate.Services.Contracts.ProductCategoryDTO>(categoryUri, _callContext, _logger);
        }
    }
}
