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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class ProductCatalogServiceClient : IProduct
    {
        Product service = new Product();


        public ProductCatalogServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public List<ProductDTO> GetProducts(string searchText)
        {
            return service.GetProducts(searchText);
        }

        public List<ProductDTO> GetProductGroups(string searchText)
        {
            return service.GetProductGroups(searchText);
        }

        public ProductDTO GetProduct(long productID)
        {
            return service.GetProduct(productID);
        }

        public ProductDTO GetProductGroupDetails(string productGroup)
        {
            return service.GetProductGroupDetails(productGroup);
        }

        public List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, string sortBy, ProductStatuses productStatus)
        {
            return service.GetProductByCategory(CategoryId, MenuId, pageNumber, pageSize, sortBy, productStatus);
        }

        public List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize)
        {
            return service.GetProductListWithSKU(searchText, dataSize);
        }

        public List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy)
        {
            return service.GetProductsBySearchCriteria(searchText, pageNumber, pageSize, sortBy);
        }

        public ProductCategoryDTO GetProductCategory(string categoryID)
        {
            return service.GetProductCategory(categoryID);
        }

        public ProductCategoryDTO SaveProductCategory(ProductCategoryDTO customerDTO)
        {
            return service.SaveProductCategory(customerDTO);
        }

        public POSProductDTO GetProductAndSKUByID(long productSKUMapID)
        {
            return service.GetProductAndSKUByID(productSKUMapID);
        }

        public List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID)
        {
            return service.ProductSKUSearch(searchText, dataSize, documentTypeID);
        }

        public List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize)
        {
            return service.ProductSKUByCategoryID(categoryID, dataSize);
        }

        public POSProductDTO GetProductSKUInventoryDetail(long skuIID, DocumentReferenceTypes referenceType = 0, long branchID = 0)
        {
            return service.GetProductSKUInventoryDetail(skuIID, referenceType, branchID);
        }
        public decimal GetProductSkuIDInventoryBranchWise(long SkuID, long BranchID)
        {
            return service.GetProductSkuIDInventoryBranchWise(SkuID, BranchID);
        }

        public List<ProductInventoryBranchDTO> GetProductInventoryOnline(long SkuID)
        {
            return service.GetProductInventoryOnline(SkuID);
        }

        public List<ProductMapDTO> ProductSearch(string searchText, int dataSize)
        {
            return service.ProductSearch(searchText, dataSize);
        }

        public void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive)
        {
            service.SetProductSKUSiteMap(productSKUMapID, siteID, isActive);
        }

        public long SOSearch(string searchText)
        {
            return service.SOSearch(searchText);
        }
    } 
}
