using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class QuickCatalogViewModel : BaseMasterViewModel
    {
        public QuickCatalogViewModel()
        {
            Prices = new List<QuickProductPriceViewModel>() { new QuickProductPriceViewModel() };
            ProductBundles = new List<ProductBundleviewModel>() { new ProductBundleviewModel() };
            //Brand = new KeyValueViewModel();
            //Unit = new KeyValueViewModel();
            //ProductFamily = new KeyValueViewModel();
            Categories = new List<KeyValueViewModel>();
            Rack = new List<KeyValueViewModel>();
            SellingUnit = new KeyValueViewModel();
            PurchasingUnit = new KeyValueViewModel();
            GLAccount = new KeyValueViewModel();
            ProductImageUrls = new List<DowloadFileViewModel>();
            NewProductImageUrls = new List<DowloadFileViewModel>();
            Allergies = new List<KeyValueViewModel>();

        }

        public long ProductID { get; set; }
        public long ProductSKUMapID { get; set; }
        public string ProductSKUCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-change='ProductCodeChanges(CRUDModel.ViewModel)'")]
        [CustomDisplay("ProductCode")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string ProductCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string SkuName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='ValidateField($event,$element,\"PartNumber\",\"Frameworks/CRUD\")'")]
        [CustomDisplay("PartNumber")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string PartNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='ValidateField($event,$element,\"BarCode\",\"Frameworks/CRUD\")'")]
        [CustomDisplay("BarCode")]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        public string BarCode { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.TaxTemplates")]
        [CustomDisplay("TaxTemplate")]
        public string TaxTemplate { get; set; }

        public int? TaxTempleteID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Brand", "Numeric", false, "")]
        [LookUp("LookUps.Brand")]
        [CustomDisplay("Brand")]
        public KeyValueViewModel Brand { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ProductFamily", "Numeric", false, "")]
        [LookUp("LookUps.ProductFamily")]
        [CustomDisplay("ProductFamily")]
        public KeyValueViewModel ProductFamily { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Categories", "String", true, "")]
        [CustomDisplay("Categories")]
        [LookUp("LookUps.Categories")]
        public List<KeyValueViewModel> Categories { get; set; }

         //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Rack", "String", true, "")]
        [CustomDisplay("Rack")]
        [LookUp("LookUps.Rack")]
        public List<KeyValueViewModel> Rack { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Unit", "Numeric", false, "")]
        //[LookUp("LookUps.Unit")]
        //[CustomDisplay("Unit")]
        //public KeyValueViewModel Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='PurchasingUnitGroupChange(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.UnitGroup")]
        [CustomDisplay("Purchasing Unit Group")]
        public string PurchaseUnitGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "")]
        [LookUp("LookUps.PurchasingUnits")]
        [CustomDisplay("Purchasing Unit")]
        public KeyValueViewModel PurchasingUnit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SellingUnitGroupChanges(CRUDModel.ViewModel)'")]
        [LookUp("LookUps.UnitGroup")]
        [CustomDisplay("Selling Unit Group")]
        public string SellingUnitGroup { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Unit", "Numeric", false, "")]
        [LookUp("LookUps.SellingUnits")]
        [CustomDisplay("Selling Unit")]
        public KeyValueViewModel SellingUnit{ get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.ProductTypesName")]
        [CustomDisplay("Type")]
        public string ProductType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUploadCrop)]
        [CustomDisplay("Product Image")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.Products, "ProductImageUrl", "", true, 1080, 1080)]

        public string ProductImageUploadFile { get; set; }
        public string ProductImageUrl { get; set; }

        public List<DowloadFileViewModel> ProductImageUrls { get; set; }
        public List<DowloadFileViewModel> NewProductImageUrls { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Stock GL Account")]
        [LookUp("LookUps.StockGLAccount")]
        [Select2("StockGLAccount", "Numeric", false, "")]
        public KeyValueViewModel GLAccount { get; set; }

        public long? GLAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.Categories[0].Key!=92")]
        [CustomDisplay("Calorie (in Kcal)")]
        //[MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        public decimal? Calorie { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled = CRUDModel.ViewModel.Categories[0].Key!=92")]
        [CustomDisplay("Weight (in KG)")]
        //[MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        public decimal? Weight { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left", attribs: "ng-disabled = CRUDModel.ViewModel.Categories[0].Key!=92")]
        [Select2("Allergies", "String", true, "")]
        [CustomDisplay("Allergies")]
        [LookUp("LookUps.Allergies")]
        public List<KeyValueViewModel> Allergies { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("PriceSettings")]
        public List<QuickProductPriceViewModel> Prices { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("BundleSetting")]
        public List<ProductBundleviewModel> ProductBundles { get; set; }

        public override BaseMasterDTO ToDTO()
        {
            var dto = new SKUDTO()
            {
                BarCode = this.BarCode == null ? this.ProductCode :  this.BarCode,
                PartNumber = this.PartNumber,
                ProductSKUCode = this.ProductSKUCode?? this.SkuName,
                ProductSKUMapID = this.ProductSKUMapID,
                //SKU = this.SKU ?? this.SkuName,
                ProductCode = this.ProductCode,
                SkuName = this.SkuName,
                ProductID = this.ProductID,
                TaxTempleteID = string.IsNullOrEmpty(this.TaxTemplate) ? (int?)null : int.Parse(this.TaxTemplate),
                ProductFamilyID = this == null || this.ProductFamily == null || this.ProductFamily.Key == null ? (long?)null : long.Parse(this.ProductFamily.Key),
                //UnitID = this == null || this.Unit == null || this.Unit.Key == null ? (long?)null : long.Parse(this.Unit.Key),
                PurchaseUnitID = this == null || this.PurchasingUnit == null || this.PurchasingUnit.Key == null ? (long?)null : long.Parse(this.PurchasingUnit.Key),
                SellingUnitID = this == null || this.SellingUnit == null || this.SellingUnit.Key == null ? (long?)null : long.Parse(this.SellingUnit.Key),
                BrandID = this == null || this.Brand == null || this.Brand.Key == null ? (long?)null : long.Parse(this.Brand.Key),
                ProductTypeID = this == null || string.IsNullOrEmpty(this.ProductType)  ? (long?)null : long.Parse(this.ProductType),
                //SellingUnitGroupID = this == null || this.SellingUnitGroup == null || this.SellingUnitGroup.Key == null ? (long?)null : long.Parse(this.SellingUnitGroup.Key),
                //PurchaseUnitGroupID = this == null || this.PurchaseUnitGroup == null || this.PurchaseUnitGroup.Key == null ? (long?)null : long.Parse(this.PurchaseUnitGroup.Key),
                SellingUnitGroupID = this == null || string.IsNullOrEmpty(this.SellingUnitGroup) ? (long?)null : long.Parse(this.SellingUnitGroup),
                PurchaseUnitGroupID = this == null || string.IsNullOrEmpty(this.PurchaseUnitGroup) ? (long?)null : long.Parse(this.PurchaseUnitGroup),
                GLAccountID = this == null || this.GLAccount == null || this.GLAccount.Key == null ? (long?)null : long.Parse(this.GLAccount.Key),

                IsActive =this.IsActive,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                ProductImageUrl = this.ProductImageUrl,
                ProductImageUploadFile = this.ProductImageUploadFile,
                Calorie = this.Calorie,
                Weight = this.Weight,
            };

            dto.ProductImageUrls = new List<DowloadFileDTO>();
            if (this.NewProductImageUrls.Count > 0)
            {
                dto.ProductImageUrls.AddRange(this.NewProductImageUrls.Select(x => new DowloadFileDTO() { FileMapID = x.FileMapID, FilePath = x.FilePath }).ToList());
            }
            else
            {
                dto.ProductImageUrls.AddRange(this.ProductImageUrls.Select(x => new DowloadFileDTO() { FileMapID = x.FileMapID, FilePath = x.FilePath }).ToList());
            }

            dto.ProductPrices = new List<ProductPriceSettingDTO>();

            foreach (var price in this.Prices)
            {
                dto.ProductPrices.Add(new ProductPriceSettingDTO()
                {
                    ProductPriceListID = price.ProductPriceListID,
                    ProductID = this.ProductID,
                    Discount = price.Discount,
                    DiscountPercentage = price.DiscountPercentage,
                    Price = price.Price,
                    PriceDescription = price.PriceDescription,
                    ProductPriceListProductMapIID = price.ProductPriceListProductMapIID
                });
            }

            dto.ProductBundle = new List<ProductBundleDTO>();

            foreach (var bundle in this.ProductBundles)
            {
                if (bundle.FromProduct != null && !string.IsNullOrEmpty(bundle.FromProduct.Key))
                {
                    dto.ProductBundle.Add(new ProductBundleDTO()
                    {
                        BundleIID = bundle.BundleIID,
                        ToProductID = bundle.ToProductID,
                        FromProductID = string.IsNullOrEmpty(bundle.FromProduct.Key) ? (long?)null : long.Parse(bundle.FromProduct.Key),
                        Quantity = bundle.Quantity,
                        FromProductSKUMapID = bundle.FromProductSKUMapID,
                        ToProductSKUMapID = bundle.ToProductSKUMapID,
                        CostPrice = bundle.CostPrice,
                        SellingPrice = bundle.SellingPrice,
                        Remarks = bundle.Remarks,
                        AvailableQuantity = bundle.Quantity,
                    });
                }
            }

            if (this.Categories.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Categories)
                {
                    dto.Categories.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            if (this.Allergies.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Allergies)
                {
                    dto.Allergies.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            if (this.Rack.Count > 0)
            {
                foreach (KeyValueViewModel kvm in this.Rack)
                {
                    dto.Rack.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            
            return dto;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var actualDto = dto as SKUDTO;
            var vm = new QuickCatalogViewModel()
            {
                BarCode = actualDto.BarCode,
                SkuName = actualDto.SkuName,
                PartNumber = actualDto.PartNumber,
                ProductID = actualDto.ProductID,
                //SKU = actualDto.SKU,
                ProductCode = actualDto.ProductCode,
                ProductSKUCode = actualDto.ProductSKUCode,
                TaxTemplate = actualDto.TaxTempleteID.HasValue ? actualDto.TaxTempleteID.ToString() : null,
                Prices = new List<QuickProductPriceViewModel>(),
                ProductBundles = new List<ProductBundleviewModel>(),
                ProductSKUMapID = actualDto.ProductSKUMapID,
                IsActive = actualDto.IsActive,
                //SellingUnitGroup = actualDto.SellingUnitGroup,
                //PurchaseUnitGroup = actualDto.PurchaseUnitGroup,
                ProductFamily = actualDto.ProductFamily.Key != null ? new KeyValueViewModel()
                {
                    Key = actualDto.ProductFamily.Key.ToString(),
                    Value = actualDto.ProductFamily.Value
                } : new KeyValueViewModel(),
                Brand = actualDto.Brand.Key != null ? new KeyValueViewModel()
                {
                    Key = actualDto.Brand.Key.ToString(),
                    Value = actualDto.Brand.Value
                } : new KeyValueViewModel(),
                //Unit = actualDto.Unit.Key != null ? new KeyValueViewModel()
                //{
                //    Key = actualDto.Unit.Key.ToString(),
                //    Value = actualDto.Unit.Value
                //} : new KeyValueViewModel(),

                PurchasingUnit = actualDto.PurchasingUnit.Key != null ? new KeyValueViewModel()
                {
                    Key = actualDto.PurchasingUnit.Key.ToString(),
                    Value = actualDto.PurchasingUnit.Value
                } : new KeyValueViewModel(),

                SellingUnit = actualDto.SellingUnit.Key != null ? new KeyValueViewModel()
                {
                    Key = actualDto.SellingUnit.Key.ToString(),
                    Value = actualDto.SellingUnit.Value
                } : new KeyValueViewModel(),


                GLAccount = actualDto.GLAccount.Key != null ? new KeyValueViewModel()
                {
                    Key = actualDto.GLAccount.Key.ToString(),
                    Value = actualDto.GLAccount.Value
                } : new KeyValueViewModel(),

                //PurchaseUnitGroup = actualDto.PurchaseUnitGroup.Key != null ? new KeyValueViewModel()
                //{
                //    Key = actualDto.PurchaseUnitGroup.Key.ToString(),
                //    Value = actualDto.PurchaseUnitGroup.Value
                //} : new KeyValueViewModel(),

                //SellingUnitGroup = actualDto.SellingUnitGroup.Key != null ? new KeyValueViewModel()
                //{
                //    Key = actualDto.SellingUnitGroup.Key.ToString(),
                //    Value = actualDto.SellingUnitGroup.Value
                //} : new KeyValueViewModel(),

                ProductType = actualDto.ProductTypeID.HasValue ? actualDto.ProductTypeID.Value.ToString() : null,
                PurchaseUnitGroup = actualDto.PurchaseUnitGroupID.HasValue ? actualDto.PurchaseUnitGroupID.Value.ToString() : null,
                SellingUnitGroup = actualDto.SellingUnitGroupID.HasValue ? actualDto.SellingUnitGroupID.Value.ToString() : null,

                ProductImageUploadFile = actualDto.ProductImageUploadFile,
                ProductImageUrl = actualDto.ProductImageUrl,
                Calorie = actualDto.Calorie,
                Weight= actualDto.Weight,
                ProductImageUrls = actualDto.ProductImageUrls.Select(x => new DowloadFileViewModel() { FileMapID = x.FileMapID, FilePath = x.FilePath }).ToList(),
            };

            foreach (var price in actualDto.ProductPrices)
            {
                vm.Prices.Add(new QuickProductPriceViewModel()
                {
                    Price = price.Price,
                    PriceDescription = price.PriceDescription,
                    ProductPriceListID = price.ProductPriceListID,
                    ProductPriceListProductMapIID = price.ProductPriceListProductMapIID,
                    DiscountPercentage = price.DiscountPercentage,
                    Discount = price.Discount,
                });
            }

            foreach (var productbundles in actualDto.ProductBundle)
            {
                vm.ProductBundles.Add(new ProductBundleviewModel()
                {
                    CostPrice = productbundles.CostPrice,
                    ToProductID = productbundles.ToProductID,
                    FromProductSKUMapID = productbundles.FromProductSKUMapID,
                    Quantity = productbundles.Quantity,
                    Remarks = productbundles.Remarks,
                    SellingPrice = productbundles.SellingPrice,
                    BundleIID = productbundles.BundleIID,
                    FromProduct = productbundles.FromProductID.HasValue ? new KeyValueViewModel()
                    {
                        Key = productbundles.FromProductID.ToString(),
                        Value = productbundles.FromProduct.Value
                    } : new KeyValueViewModel(),
                    ToProductSKUMapID = productbundles.ToProductSKUMapID
                });
            }

            if (actualDto.Categories.Count > 0)
            {
                foreach (KeyValueDTO kvm in actualDto.Categories)
                {
                    vm.Categories.Add(new KeyValueViewModel()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
            }
            if (actualDto.Allergies.Count > 0)
            {
                foreach (KeyValueDTO kvm in actualDto.Allergies)
                {
                    vm.Allergies.Add(new KeyValueViewModel()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
            }

            if (actualDto.Rack.Count > 0)
            {
                foreach (KeyValueDTO kvm in actualDto.Rack)
                {
                    vm.Rack.Add(new KeyValueViewModel()
                    {
                        Key = kvm.Key,
                        Value = kvm.Value
                    });
                }
            }
            

            return vm;
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SKUDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<QuickCatalogViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SKUDTO>(jsonString);
        }
    }
}