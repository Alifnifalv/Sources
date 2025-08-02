using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Service.Client
{
    public class ProductCatalogServiceClient : BaseClient, IProduct
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.PRODUCT_SERVICE_NAME);


        public ProductCatalogServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public List<ProductDTO> GetProducts(string searchText)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProductGroups(string searchText)
        {
            throw new NotImplementedException();
        }

        public ProductDTO GetProduct(long productID)
        {
            throw new NotImplementedException();
        }

        public ProductDTO GetProductGroupDetails(string productGroup)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, string sortBy, ProductStatuses productStatus)
        {
            throw new NotImplementedException();
        }

        public List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public ProductCategoryDTO GetProductCategory(string categoryID)
        {
            return ServiceHelper.HttpGetRequest<ProductCategoryDTO>(Service + "GetProductCategory/" + categoryID, this._callContext);
        }

        public ProductCategoryDTO SaveProductCategory(ProductCategoryDTO customerDTO)
        {
            return ServiceHelper.HttpPostGetRequest<ProductCategoryDTO>(Service + "SaveProductCategory", customerDTO, this._callContext);
        }

        public POSProductDTO GetProductAndSKUByID(long productSKUMapID)
        {
            string uri = string.Concat(Service + "GetProductAndSKUByID?productSKUMapID=" + productSKUMapID);
            return ServiceHelper.HttpGetRequest < POSProductDTO>(uri);
        }

        public List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize)
        {
            var uri = string.Format("{0}/ProductSKUByCategoryID?categoryID={1}&dataSize={2}", Service, categoryID, dataSize);
            return ServiceHelper.HttpGetRequest<List<POSProductDTO>>(uri, _callContext, _logger);
        }

        public List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID)
        {
            var uri = string.Format("{0}/ProductSKUSearch?searchText={1}&dataSize={2}", Service, searchText, dataSize, documentTypeID);
            return ServiceHelper.HttpGetRequest<List<POSProductDTO>>(uri, _callContext, _logger);
        }

        public POSProductDTO GetProductSKUInventoryDetail(long skuIID, DocumentReferenceTypes referenceType = 0, long branchID = 0)
        {
            var uri = string.Format("{0}/GetProductSKUInventoryDetail?skuIID={1}&referenceType={2}&branchID={3}", Service, skuIID, referenceType, branchID);
            return ServiceHelper.HttpGetRequest<POSProductDTO>(uri, _callContext, _logger);
        }
        public decimal GetProductSkuIDInventoryBranchWise(long SkuID, long BranchID)
        {
            var uri = string.Format("{0}/GetProductSkuIDInventoryBranchWise?SkuID={1}&BranchID={2}", Service,SkuID,BranchID);
            return ServiceHelper.HttpGetRequest<decimal>(uri, _callContext, _logger);
        }

        public List<ProductInventoryBranchDTO> GetProductInventoryOnline(long SkuID)
        {
            var uri = string.Format("{0}/GetProductInventoryOnline?SkuID={1}", Service, SkuID);
            return ServiceHelper.HttpGetRequest <List<ProductInventoryBranchDTO>>(uri, _callContext, _logger);
        }

        public List<ProductMapDTO> ProductSearch(string searchText, int dataSize)
        {
            var uri = string.Format("{0}/ProductSearch?searchText={1}&dataSize={2}", Service, searchText, dataSize);
            return ServiceHelper.HttpGetRequest<List<ProductMapDTO>>(uri, _callContext, _logger);
        }

        public void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive)
        {
            var uri = string.Format("{0}/SetProductSKUSiteMap?productSKUMapID={1}&siteID={2}&isActive={3}", Service, productSKUMapID, siteID, isActive);
            ServiceHelper.HttpVoidGetRequest(uri, _callContext, _logger);
        }

        public long SOSearch(string searchText)
        {
            var uri = string.Format("{0}/SOSearch?searchText={1}", Service, searchText);
            return ServiceHelper.HttpGetRequest<long>(uri, _callContext, _logger);
        }

    } 
}
