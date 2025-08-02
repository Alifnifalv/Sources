using System.Collections.Generic;
using System.ServiceModel;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProduct" in both code and config file together.
    
    public interface IProduct
    {
        List<ProductDTO> GetProducts(string searchText);

        List<ProductDTO> GetProductGroups(string searchText);

        ProductDTO GetProduct(long productID);

        ProductDTO GetProductGroupDetails(string productGroup);

        List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, string sortBy, ProductStatuses productStatus);

        List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize);

        List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy);

        ProductCategoryDTO GetProductCategory(string categoryID);

        ProductCategoryDTO SaveProductCategory(ProductCategoryDTO customerDTO);

        POSProductDTO GetProductAndSKUByID(long productSKUMapID);

        List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID);

        List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize);

        POSProductDTO GetProductSKUInventoryDetail(long skuIID, DocumentReferenceTypes referenceType, long branchID);

        decimal GetProductSkuIDInventoryBranchWise(long SkuID, long BranchID);

        List<ProductInventoryBranchDTO> GetProductInventoryOnline(long SkuID);

        List<ProductMapDTO> ProductSearch(string searchText, int dataSize);

        void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive);

        long SOSearch(string searchText);
    }
}