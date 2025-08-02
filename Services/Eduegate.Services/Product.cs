using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services
{
    public class Product : BaseService, IProduct
    {

        public List<ProductDTO> GetProducts(string searchText)
        {
            try
            {
                var products = ProductBL.GetProductList(searchText);
                Eduegate.Logger.LogHelper<ProductDTO>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<ProductDTO> GetProductGroups(string searchText)
        {
            try
            {
                var groups = ProductBL.GetProductGroups(searchText);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + groups.ToString());
                return groups;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ProductDTO GetProduct(long productID)
        {
            try
            {
                var product = ProductBL.GetProduct(productID);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + product.ToString());
                return product;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public ProductDTO GetProductGroupDetails(string productGroup)
        {
            try
            {
                var group = ProductBL.GetProductGroupDetails(productGroup);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + group.ToString());
                return group;

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, string sortBy, Eduegate.Services.Contracts.Enums.ProductStatuses productStatus)
        {
            return new ProductBL(this.CallContext).GetProductByCategory(Convert.ToInt64(CategoryId), MenuId, pageNumber, pageSize, CallContext, sortBy, productStatus);
        }

        public List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize)
        {
            try
            {
                var products = new ProductBL(this.CallContext).GetProductListWithSKU(searchText, dataSize);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy)
        {
            try
            {
                var products = new ProductBL(this.CallContext).GetProductsBySearchCriteria(searchText, pageNumber, pageSize, CallContext, sortBy);
                Eduegate.Logger.LogHelper<ProductDTO>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ProductCategoryDTO GetProductCategory(string categoryID)
        {
            try
            {
                return new ProductBL(this.CallContext).GetProductCategory(long.Parse(categoryID));
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ProductCategoryDTO SaveProductCategory(ProductCategoryDTO dto)
        {
            try
            {
                return new ProductBL(this.CallContext).SaveProductCategory(dto);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
                throw new FaultException(exception.Message);
            }
        }

        public POSProductDTO GetProductAndSKUByID(long productSKUMapID)
        {
            return new ProductBL(this.CallContext).GetProductAndSKUByID(productSKUMapID);
        }

        #region product sku and quantity view

        public List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID)
        {
            try
            {
                var products = new ProductBL(this.CallContext).ProductSKUSearch(searchText, dataSize, documentTypeID);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize)
        {
            try
            {
                var products = new ProductBL(this.CallContext).ProductSKUByCategoryID(categoryID, dataSize);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public POSProductDTO GetProductSKUInventoryDetail(long skuIID, Services.Contracts.Enums.DocumentReferenceTypes referenceType, long branchID)
        {
            try
            {
                var productSkuDTO = new ProductBL(this.CallContext).GetProductSKUInventoryDetail(skuIID, referenceType, branchID);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + productSkuDTO.ToString());
                return productSkuDTO;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        #endregion


        public decimal GetProductSkuIDInventoryBranchWise(long SkuID,long BranchID)
        {
            var result = 0;
            try
            {
                return new ProductDetailBL(this.CallContext).GetProductInventoryByBranch(Convert.ToInt64(SkuID), Convert.ToInt64(BranchID));
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                result = 0;
            }

            return result;
        }

        public List<ProductInventoryBranchDTO> GetProductInventoryOnline(long SkuID)
        {
            var result = new List<ProductInventoryBranchDTO>();
            try
            {
                return new ProductDetailBL(this.CallContext).GetProductInventoryOnline(SkuID.ToString());
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCart>.Fatal(exception.Message, exception);
                result = null;
            }

            return result;
        }

        public List<ProductMapDTO> ProductSearch(string searchText, int dataSize)
        {
            try
            {
                var products = new ProductBL(this.CallContext).ProductSearch(searchText, dataSize);
                Eduegate.Logger.LogHelper<Product>.Info("Service Result : " + products.ToString());
                return products; 
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive)
        {
            try
            {
                new ProductBL(this.CallContext).SetProductSKUSiteMap(productSKUMapID, siteID, isActive);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long SOSearch(string searchText)
        {
            try
            {
                var IID = new ProductBL(this.CallContext).SOSearch(searchText);
                return IID;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
