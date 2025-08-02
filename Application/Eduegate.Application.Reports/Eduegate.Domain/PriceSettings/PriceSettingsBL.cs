using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Domain.Mappers.PriceSettings;

namespace Eduegate.Domain
{
    public class PriceSettingsBL
    {
        private CallContext _callContext;

        public PriceSettingsBL(CallContext context)
        {
            _callContext = context;
        }

        public List<ProductPriceSettingDTO> GetProductPriceSettings(long productIID)
        {
            List<ProductPriceListProductMap> priceSettingMaps = new List<ProductPriceListProductMap>();
            List<ProductPriceSettingDTO> priceSettings = new List<ProductPriceSettingDTO>();

            priceSettingMaps = new PriceSettingsRepository().GetProductPriceSettings(productIID);

            if(priceSettingMaps.IsNotNull() && priceSettingMaps.Count > 0)
            {
                foreach(var priceSettingMap in priceSettingMaps)
                {
                    priceSettings.Add(ProductPriceDetailsMapper.Mapper(_callContext).ToDTO(priceSettingMap));
                }
            }

            return priceSettings;
        }

        public List<ProductPriceSettingDTO> SaveProductPrice(List<ProductPriceSettingDTO> productPriceSettings, long productIID)
        {
            List<ProductPriceSettingDTO> priceSettings = new List<ProductPriceSettingDTO>();
            List<ProductPriceListProductMap> pplpMaps = new List<ProductPriceListProductMap>();
            List<ProductPriceListProductMap> deleteEntityMaps = new List<ProductPriceListProductMap>();

            foreach (ProductPriceSettingDTO dto in productPriceSettings)
            {
                pplpMaps.Add(ProductPriceDetailsMapper.Mapper(_callContext).ToEntity(dto));
            }

            //Deleting paraent item (Price setting) passing data from bl to repository
            var currentData = productPriceSettings.Where(x => x.ProductPriceListProductMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetProductPriceSettings(productIID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deleteEntityMaps = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplpm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListProductMapIID == pplpm.ProductPriceListProductMapIID).FirstOrDefault();

                        if (data == null)
                            deleteEntityMaps.Add(pplpm);
                    }
                }
            }

            List<ProductPriceListProductMap> updateProductPriceSettings = new PriceSettingsRepository().SaveProductPrice(pplpMaps, productIID, deleteEntityMaps);

            if (updateProductPriceSettings.IsNotNull() && updateProductPriceSettings.Count > 0)
            {
                foreach (var pplpMap in updateProductPriceSettings)
                {
                    priceSettings.Add(ProductPriceDetailsMapper.Mapper(_callContext).ToDTO(pplpMap));
                }
            }

            return priceSettings;
        }

        public List<BrandPriceDTO> SaveBrandPrice(List<BrandPriceDTO> brandPriceSetting)
        {
            List<BrandPriceDTO> priceSettings = new List<BrandPriceDTO>();
            BrandPriceMapper priceMapper = new BrandPriceMapper();
            PriceSettingsRepository repo = new PriceSettingsRepository();
            foreach (BrandPriceDTO priceSetting in brandPriceSetting)
            {
                priceSettings.Add(priceMapper.ToDTO(repo.SaveBrandPrice(priceMapper.ToEntity(priceSetting))));

            }
            return priceSettings;
        }

        public List<BranchMapDTO> SaveBranchMaps(List<BranchMapDTO> branchMapSetting)
        {
            List<BranchMapDTO> priceSettings = new List<BranchMapDTO>();
            BranchMapMapper branchMapper = BranchMapMapper.Mapper(_callContext);

            PriceSettingsRepository repo = new PriceSettingsRepository();
            var results = repo.SaveBranchMaps(branchMapSetting.Select(x=> branchMapper.ToEntity(x)).ToList());

            if (results != null)
            {
                priceSettings = results.Select(x => branchMapper.ToDTO(x)).ToList();
            }

            return priceSettings;
        }

        public List<CategoryPriceDTO> SaveCategoryPrice(List<CategoryPriceDTO> categoryPriceSetting)
        {
            List<CategoryPriceDTO> priceSettings = new List<CategoryPriceDTO>();
            CategoryPriceMapper priceMapper = new CategoryPriceMapper();
            PriceSettingsRepository repo = new PriceSettingsRepository();
            foreach (CategoryPriceDTO priceSetting in categoryPriceSetting)
            {
                priceSettings.Add(priceMapper.ToDTO(repo.SaveCategoryPrice(priceMapper.ToEntity(priceSetting))));

            }
            return priceSettings;
        }

        public List<ProductPriceSKUDTO> GetProductPriceListForSKU(long skuID,int companyID = 0)
        {
            List<ProductPriceSKUDTO> productPriceSkus = new List<ProductPriceSKUDTO>();

            List<ProductPriceListSKUMap> productPriceListSKUMaps = new PriceSettingsRepository().GetProductPriceListForSKU(skuID, companyID > 0 ?companyID : _callContext.IsNotNull() ? _callContext.CompanyID.Value : default(int));

            if (productPriceListSKUMaps.IsNotNull() && productPriceListSKUMaps.Count > 0)
            {
                foreach (var pplsm in productPriceListSKUMaps)
                {
                    var productPriceSKUDTO = ProductSKUPriceMapper.Mapper(_callContext).ToDTO(pplsm);

                    if (productPriceSKUDTO.IsNotNull())
                    {
                        productPriceSkus.Add(productPriceSKUDTO);
                    }
                }
            }

            return productPriceSkus;
        }
        public List<ProductPriceCategoryDTO> GetCategoryPriceList(long categoryID) 
        {
            List<ProductPriceCategoryDTO> categoryPrices = new List<ProductPriceCategoryDTO>();
            List<ProductPriceListCategoryMap> PriceListCategoryMaps = new PriceSettingsRepository().GetCategoryPriceSettings(categoryID);

            if (PriceListCategoryMaps.IsNotNull() && PriceListCategoryMaps.Count > 0)
            {
                foreach (var plcm in PriceListCategoryMaps)
                {
                    categoryPrices.Add(PriceListCategoryMapper.Mapper(_callContext).ToDTO(plcm));
                }
            }

            return categoryPrices;
        }
        //ToDo:: SKU price saving code already exists, we have to move that code here
        public List<ProductPriceSKUDTO> SaveSKUPrice(List<ProductPriceSKUDTO> skuPriceSetting, long skuIID)
        {
            List<ProductPriceSKUDTO> priceSettings = new List<ProductPriceSKUDTO>();
            List<ProductPriceListSKUMap> priceSettingSkuEntitylst = new List<ProductPriceListSKUMap>();
            List<ProductPriceListSKUMap> deletePriceSettings = new List<ProductPriceListSKUMap>();

            foreach (var sps in skuPriceSetting)
            {
                priceSettingSkuEntitylst.Add(ProductSKUPriceMapper.Mapper(_callContext).ToEntity(sps));
            }

            //Deleting paraent item (Price setting) passing data from bl to repository
            var currentData = skuPriceSetting.Where(x => x.ProductPriceListItemMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetProductPriceListForSKU(skuIID, (int)_callContext.CompanyID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deletePriceSettings = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplsm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListItemMapIID == pplsm.ProductPriceListItemMapIID).FirstOrDefault();

                        if (data == null)
                            deletePriceSettings.Add(pplsm);
                    }
                }
            }


            // get supplier login info
            var supplier = new SupplierBL(_callContext).GetSupplierByLoginID(Convert.ToInt64(_callContext.LoginID));
            foreach (var productPriceListSKUMap in priceSettingSkuEntitylst)
            {
                var categories = new CategoryRepository().GetCategoryBySkuId(productPriceListSKUMap.ProductSKUID.Value);
                var category = categories.Where(x => x.Profit > 0).FirstOrDefault();

                if (supplier.IsNotNull() && productPriceListSKUMap.Cost > 0 && (productPriceListSKUMap.Price == 0 || productPriceListSKUMap.Price.IsNull()))
                {
                    // calculate price
                    productPriceListSKUMap.Price = (category != null && category.Profit != null) ? productPriceListSKUMap.Cost + productPriceListSKUMap.Cost * (category.Profit / 100)
                                           : (supplier != null && supplier.Profit != null) ? productPriceListSKUMap.Cost + productPriceListSKUMap.Cost * (supplier.Profit / 100) 
                                           : productPriceListSKUMap.Price.IsNotNull() ? productPriceListSKUMap.Price : productPriceListSKUMap.Cost;
                }
            }


            List<ProductPriceListSKUMap> updatedPriceSettingSkus = new PriceSettingsRepository().SaveSKUPrice(priceSettingSkuEntitylst, skuIID, deletePriceSettings);

            if (updatedPriceSettingSkus.IsNotNull() && updatedPriceSettingSkus.Count > 0)
            {
                foreach (var pplsm in updatedPriceSettingSkus)
                { 
                    var productPriceSKU = ProductSKUPriceMapper.Mapper(_callContext).ToDTO(pplsm);
                    productPriceSKU.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(productPriceSKU.ProductPriceListID));
                    priceSettings.Add(productPriceSKU);
                }
            }

            return priceSettings;
        }

        public List<ProductPriceCategoryDTO> SaveCategoryPriceSettings(List<ProductPriceCategoryDTO> categoryPriceSetting,long categoryIID)
        {
            List<ProductPriceCategoryDTO> PriceSettings = new List<ProductPriceCategoryDTO>();
            List<ProductPriceListCategoryMap> priceSettingcategoryEntitylst = new List<ProductPriceListCategoryMap>();
            List<ProductPriceListCategoryMap> deletePriceSettings = new List<ProductPriceListCategoryMap>();

             foreach (var cps in categoryPriceSetting)
              {
                priceSettingcategoryEntitylst.Add(PriceListCategoryMapper.Mapper(_callContext).ToEntity(cps));
              }

            //Deleting paraent item (Price setting) passing data from bl to repository
            var currentData = categoryPriceSetting.Where(x => x.ProductPriceListCategoryMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetCategoryPriceSettings(categoryIID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deletePriceSettings = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplcm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListCategoryMapIID == pplcm.ProductPriceListCategoryMapIID).FirstOrDefault();

                        if (data == null)
                            deletePriceSettings.Add(pplcm);
                    }
                }
            }

            List<ProductPriceListCategoryMap> updatedPriceSettings = new PriceSettingsRepository().SaveCategoryPriceSettings(priceSettingcategoryEntitylst, categoryIID, deletePriceSettings);

            if (updatedPriceSettings.IsNotNull() && updatedPriceSettings.Count > 0)
            {
                foreach (var pplcm in updatedPriceSettings)
                {
                    var categoryPrice = PriceListCategoryMapper.Mapper(_callContext).ToDTO(pplcm);
                    categoryPrice.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(categoryPrice.ProductPriceListID));
                    PriceSettings.Add(categoryPrice);
                }
            }

            return PriceSettings;
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForSKU(long IID)
        {
            List<CustomerGroupDTO> customerGroups = new List<CustomerGroupDTO>();
            CustomerGroupDTO cgDTO = null;
            CustomerGroupPriceDTO cgpDTO = null;

            List<CustomerGroup> customerGroupMaps = new CustomerRepository().GetCustomerGroups();
            List<ProductPriceList> productpriceLists = new PriceSettingsRepository().GetProductPriceListBySku(IID);
            List<ProductPriceListCustomerGroupMap> pplcgMaps = new PriceSettingsRepository().GetCustomerGroupPriceSettingsForSKU(IID);

            if(pplcgMaps.IsNotNull() && pplcgMaps.Count > 0)
            {
                if(customerGroupMaps.IsNotNull() && customerGroupMaps.Count > 0)
                {
                    foreach(var cgm in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cgm.CustomerGroupIID;
                        cgDTO.GroupName = cgm.GroupName;

                        var cgMaps = pplcgMaps.Where(x => x.CustomerGroupID == Convert.ToInt32(cgm.CustomerGroupIID)).ToList();

                        foreach (var cgms in cgMaps)
                        {
                            cgDTO.CustomerGroupPrices.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToDTO(cgms));
                        }

                        customerGroups.Add(cgDTO);
                    }
                }
            }
            else
            {
                if (productpriceLists.IsNotNull() && productpriceLists.Count > 0)
                {
                    foreach (var cg in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cg.CustomerGroupIID;
                        cgDTO.GroupName = cg.GroupName;

                        foreach (var ppl in productpriceLists)
                        {
                            cgpDTO = new CustomerGroupPriceDTO();

                            cgpDTO.ProductPriceListID = ppl.ProductPriceListIID;
                            cgpDTO.PriceListName = ppl.PriceDescription;
                            cgpDTO.CustomerGroupID = cg.CustomerGroupIID;
                            cgpDTO.GroupName = cg.GroupName;
                            cgpDTO.ProductSKUMapID = IID;

                            cgDTO.CustomerGroupPrices.Add(cgpDTO);
                        }

                        customerGroups.Add(cgDTO);
                    }
                }
                else
                {
                    foreach (var cgm in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cgm.CustomerGroupIID;
                        cgDTO.GroupName = cgm.GroupName;

                        customerGroups.Add(cgDTO);
                    }
                }
            }

            return customerGroups;
        }

        public List<CustomerGroupPriceDTO> SaveCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSetting, long IID)
        {
            List<CustomerGroupPriceDTO> priceSettings = new List<CustomerGroupPriceDTO>();
            List<ProductPriceListCustomerGroupMap> entityList = new List<ProductPriceListCustomerGroupMap>();
            List<ProductPriceListCustomerGroupMap> deleteCustomerGroupSettings = new List<ProductPriceListCustomerGroupMap>();

                foreach (var cgps in customerGroupPriceSetting)
                {
                    entityList.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToEntity(cgps));
                }

            //Deleting parent item (CustomerGroup setting) passing data from bl to repository
            var currentData = customerGroupPriceSetting.Where(x => x.ProductPriceListCustomerGroupMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetCustomerGroupPriceSettingsForSKU(IID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deleteCustomerGroupSettings = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplcgm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListCustomerGroupMapIID == pplcgm.ProductPriceListCustomerGroupMapIID).FirstOrDefault();

                        if (data == null)
                            deleteCustomerGroupSettings.Add(pplcgm);
                    }
                }
            }
            List<ProductPriceListCustomerGroupMap> pplcgMaps = new PriceSettingsRepository().SaveCustomerGroupPrice(entityList, IID, deleteCustomerGroupSettings);

            if (pplcgMaps.IsNotNull() && pplcgMaps.Count > 0)
            {
                foreach (var pplcgMap in pplcgMaps)
                {
                    priceSettings.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToDTO(pplcgMap));
                }
            }

            return priceSettings;
        }

        public List<ProductPriceDTO> GetProductPriceLists(int IID)
        {
            return Mappers.PriceSettings.ProductPriceListMapper.Mapper(_callContext).ToDTO(new PriceSettingsRepository().GetProductPriceLists((int) IID));
        }

        public List<BranchMapDTO> GetProductBranchLists()
        {
            return Mappers.BranchMapMapper.Mapper(_callContext).ToDTO(new PriceSettingsRepository().GetProductBranchLists(Convert.ToInt32(_callContext.CompanyID))); 
        } 

        public List<ProductPriceBrandDTO> GetBrandPriceList(long brandID)
        {
            List<ProductPriceBrandDTO> brandPrices = new List<ProductPriceBrandDTO>();
            List<ProductPriceListBrandMap> PriceListBrandMaps = new PriceSettingsRepository().GetBrandPriceSettings(brandID);

            if (PriceListBrandMaps.IsNotNull() && PriceListBrandMaps.Count > 0)
            {
                foreach (var plbm in PriceListBrandMaps)
                {
                    brandPrices.Add(PriceListBrandMapper.Mapper(_callContext).ToDTO(plbm));
                }
            }

            return brandPrices;
        }

        public List<ProductPriceBrandDTO> SaveBrandPriceSettings(List<ProductPriceBrandDTO> brandPriceSetting, long brandIID)
        { 
            List<ProductPriceBrandDTO> PriceSettings = new List<ProductPriceBrandDTO>();
            List<ProductPriceListBrandMap> priceSettingBrandEntitylst = new List<ProductPriceListBrandMap>();
            List<ProductPriceListBrandMap> deletePriceSettings = new List<ProductPriceListBrandMap>();

            foreach (var bps in brandPriceSetting)
             {
               priceSettingBrandEntitylst.Add(PriceListBrandMapper.Mapper(_callContext).ToEntity(bps));
             }

            //Deleting parent item (Price setting) passing data from bl to repository
            var currentData = brandPriceSetting.Where(x => x.ProductPriceListBrandMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetBrandPriceSettings(brandIID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deletePriceSettings = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplbm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListBrandMapIID == pplbm.ProductPriceListBrandMapIID).FirstOrDefault();

                        if (data == null)
                            deletePriceSettings.Add(pplbm);
                    }
                }
            }

            List<ProductPriceListBrandMap> updatedPriceSettings = new PriceSettingsRepository().SaveBrandPriceSettings(priceSettingBrandEntitylst, brandIID, deletePriceSettings);

            if (updatedPriceSettings.IsNotNull() && updatedPriceSettings.Count > 0)
            {
                foreach (var pplbm in updatedPriceSettings)
                {
                    var brandPrice = PriceListBrandMapper.Mapper(_callContext).ToDTO(pplbm);
                    brandPrice.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(brandPrice.ProductPriceListID));
                    PriceSettings.Add(brandPrice);
                }
            }

            return PriceSettings;
        }

        public List<CustomerGroupDTO> GetCustomerGroupPriceSettingsForProduct(long IID)
        {
            List<CustomerGroupDTO> customerGroups = new List<CustomerGroupDTO>();
            CustomerGroupDTO cgDTO = null;
            CustomerGroupPriceDTO cgpDTO = null;

            List<CustomerGroup> customerGroupMaps = new CustomerRepository().GetCustomerGroups();
            List<ProductPriceList> productpriceLists = new PriceSettingsRepository().GetProductPriceListByProduct(IID);
            List<ProductPriceListCustomerGroupMap> pplcgMaps = new PriceSettingsRepository().GetCustomerGroupPriceSettingsForProduct(IID);

            if (pplcgMaps.IsNotNull() && pplcgMaps.Count > 0)
            {
                if (customerGroupMaps.IsNotNull() && customerGroupMaps.Count > 0)
                {
                    foreach (var cgm in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cgm.CustomerGroupIID;
                        cgDTO.GroupName = cgm.GroupName;

                        var cgMaps = pplcgMaps.Where(x => x.CustomerGroupID == Convert.ToInt32(cgm.CustomerGroupIID)).ToList();

                        foreach (var cgms in cgMaps)
                        {
                            cgDTO.CustomerGroupPrices.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToDTO(cgms));
                        }

                        customerGroups.Add(cgDTO);
                    }
                }
            }
            else
            {
                if (productpriceLists.IsNotNull() && productpriceLists.Count > 0)
                {
                    foreach (var cg in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cg.CustomerGroupIID;
                        cgDTO.GroupName = cg.GroupName;

                        foreach (var ppl in productpriceLists)
                        {
                            cgpDTO = new CustomerGroupPriceDTO();

                            cgpDTO.ProductPriceListID = ppl.ProductPriceListIID;
                            cgpDTO.PriceListName = ppl.PriceDescription;
                            cgpDTO.CustomerGroupID = cg.CustomerGroupIID;
                            cgpDTO.GroupName = cg.GroupName;
                            cgpDTO.ProductID = IID;

                            cgDTO.CustomerGroupPrices.Add(cgpDTO);
                        }

                        customerGroups.Add(cgDTO);
                    }
                }
                else
                {
                    foreach (var cgm in customerGroupMaps)
                    {
                        cgDTO = new CustomerGroupDTO();
                        cgDTO.CustomerGroupPrices = new List<CustomerGroupPriceDTO>();

                        cgDTO.CustomerGroupIID = cgm.CustomerGroupIID;
                        cgDTO.GroupName = cgm.GroupName;

                        customerGroups.Add(cgDTO);
                    }
                }
            }

            return customerGroups;
        }

        public List<CustomerGroupPriceDTO> SaveProductCustomerGroupPrice(List<CustomerGroupPriceDTO> customerGroupPriceSetting, long productID)
        {
            List<CustomerGroupPriceDTO> priceSettings = new List<CustomerGroupPriceDTO>();
            List<ProductPriceListCustomerGroupMap> entityList = new List<ProductPriceListCustomerGroupMap>();
            List<ProductPriceListCustomerGroupMap> deletePriceSettings = new List<ProductPriceListCustomerGroupMap>();

            //Deleting parent item (Price setting) passing data from bl to repository
            var currentData = customerGroupPriceSetting.Where(x => x.ProductPriceListCustomerGroupMapIID > 0).ToList();
            var dbData = new PriceSettingsRepository().GetCustomerGroupPriceSettingsForProduct(productID);

            if (currentData.IsNotNull() && currentData.Count == 0)
            {
                deletePriceSettings = dbData;
            }
            else
            {
                if (dbData.IsNotNull() && dbData.Count > 0)
                {
                    foreach (var pplcgm in dbData)
                    {
                        var data = currentData.Where(x => x.ProductPriceListCustomerGroupMapIID == pplcgm.ProductPriceListCustomerGroupMapIID).FirstOrDefault();

                        if (data == null)
                            deletePriceSettings.Add(pplcgm);
                    }
                }
            }

            if (customerGroupPriceSetting.IsNotNull() && customerGroupPriceSetting.Count > 0)
            {
                foreach (var cgps in customerGroupPriceSetting)
                {
                    entityList.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToEntity(cgps));
                }
            }

            List<ProductPriceListCustomerGroupMap> pplcgMaps = new PriceSettingsRepository().SaveProductCustomerGroupPrice(entityList, productID,deletePriceSettings);

            if (pplcgMaps.IsNotNull() && pplcgMaps.Count > 0)
            {
                foreach (var pplcgMap in pplcgMaps)
                {
                    priceSettings.Add(CustomerGroupPriceMapper.Mapper(_callContext).ToDTO(pplcgMap));
                }
            }

            return priceSettings;
        }
         

    }
}
