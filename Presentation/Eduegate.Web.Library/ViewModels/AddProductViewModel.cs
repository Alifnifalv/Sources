using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    public class AddProductViewModel
    {
        public AddProductViewModel()
        {
            SeoMetadata = new SeoMetadataViewModel();
            SKUMappings = new List<SKUViewModel>();
        }

        public string DefaultLanguage { get; set; }
        public QuickCreateViewModel QuickCreate { get; set; }
        public List<SKUViewModel> SKUMappings { get; set; }
        public ProductFeatureViewModel ProductFeature { get; set; }
        public List<ProductCategoryViewModel> SelectedCategory { get; set; }
        public string ImageSourceTempPath { get; set; }
        public string ImageSourceDesignationPath { get; set; }
        public List<UploadedFileDetailsViewModel> UploadedFiles { get; set; }
        public List<UploadedFileDetailsViewModel> UploadVideoFiles { get; set; }
        public ProductToProductMapViewModel ProductMaps { get; set; }
        public List<ProductTagViewModel> SelectedTags { get; set; }
        public SeoMetadataViewModel SeoMetadata { get; set; }
        public ProductBundlesViewModel BundleMaps { get; set; }

        // For getting key value of employee
        public List<KeyValueViewModel> KeyValueOwners { get; set; }
        public KeyValueViewModel SKUOwner { get; set; } 

        public static AddProductViewModel ToVM(SKUDTO dto)  
        {
            var vm = new AddProductViewModel();
            foreach (var config in dto.ProductInventorySKUConfigMaps)
            {
                vm.SKUOwner = new KeyValueViewModel();
                vm.SKUOwner.Key = config.ProductInventoryConfig.EmployeeID.ToString();
                vm.SKUOwner.Value = config.ProductInventoryConfig.EmployeeName;
            }
            return vm;
        }
    }
}