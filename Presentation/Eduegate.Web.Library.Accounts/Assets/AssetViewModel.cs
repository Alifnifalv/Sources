using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Eduegate.Services.Contracts.ErrorCodes;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Asset", "CRUDModel.ViewModel")]
    [DisplayName("Asset")]
    public class AssetViewModel : BaseMasterViewModel
    {
        public AssetViewModel()
        {
            IsRequiredSerialNumber = false;
            ProductSKU = new KeyValueViewModel();
            AccountInformations = new AssetAccountsInfoViewModel();
        }

        public long AssetIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Type")]
        [LookUp("LookUps.AssetTypes")]
        public string AssetType { get; set; }
        public int? AssetTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Group")]
        [LookUp("LookUps.AssetGroups")]
        public string AssetGroup { get; set; }
        public int? AssetGroupID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='AssetCategoryChanges(CRUDModel.ViewModel)'")]
        [CustomDisplay("Category")]
        [LookUp("LookUps.AssetCategories")]
        public string AssetCategory { get; set; }
        public long? AssetCategoryID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Sub Category")]
        [LookUp("LookUps.AssetCategories")]
        public string AssetSubCategory { get; set; }
        public long? AssetSubCategoryID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Asset Code")]
        public string AssetCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Asset Prefix")]
        public string AssetPrefix { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [CustomDisplay("Last Sequence No")]
        public long? LastSequenceNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Serial no is Required")]
        public bool? IsRequiredSerialNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Unit")]
        [LookUp("LookUps.Unit")]
        public string Unit { get; set; }
        public long? UnitID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Products", "String", false)]
        [CustomDisplay("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ActiveProductSKUs")]
        public KeyValueViewModel ProductSKU { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "AccountInformations", "AccountInformations")]
        [CustomDisplay("Account Information")]
        public AssetAccountsInfoViewModel AccountInformations { get; set; }

        public string AssetCategoryPrefix { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssetDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssetDTO, AssetViewModel>.CreateMap();
            var assetDTO = dto as AssetDTO;
            var vm = Mapper<AssetDTO, AssetViewModel>.Map(dto as AssetDTO);

            vm.AssetIID = assetDTO.AssetIID;
            vm.AssetType = assetDTO.AssetTypeID.HasValue ? assetDTO.AssetTypeID.ToString() : null;
            vm.AssetGroup = assetDTO.AssetGroupID.HasValue ? assetDTO.AssetGroupID.ToString() : null;
            vm.AssetCategory = assetDTO.AssetCategoryID.HasValue ? assetDTO.AssetCategoryID.ToString() : null;
            vm.AssetSubCategory = assetDTO.AssetSubCategoryID.HasValue ? assetDTO.AssetSubCategoryID.ToString() : null;
            vm.AssetCode = assetDTO.AssetCode;
            vm.Description = assetDTO.Description;
            vm.AssetPrefix = assetDTO.AssetPrefix;
            vm.LastSequenceNumber = assetDTO.LastSequenceNumber;
            vm.IsRequiredSerialNumber = assetDTO.IsRequiredSerialNumber;
            vm.Unit = assetDTO.UnitID.HasValue ? assetDTO.UnitID.ToString() : null;
            vm.AssetCategoryPrefix = assetDTO.AssetCategoryPrefix;
            
            vm.AccountInformations = new AssetAccountsInfoViewModel()
            {
                GLAccount = assetDTO.AssetGlAccID.HasValue ? new KeyValueViewModel()
                { 
                    Key = assetDTO.AssetGlAccount.Key,
                    Value = assetDTO.AssetGlAccount.Value
                } : new KeyValueViewModel(),
                DepreciationGLAccount = assetDTO.AccumulatedDepGLAccID.HasValue ? new KeyValueViewModel()
                {
                    Key = assetDTO.AccumulatedDepGLAccount.Key,
                    Value = assetDTO.AccumulatedDepGLAccount.Value
                } : new KeyValueViewModel(),
                ExpenseGLAccount = assetDTO.DepreciationExpGLAccID.HasValue ? new KeyValueViewModel()
                {
                    Key = assetDTO.DepreciationExpGLAccount.Key,
                    Value = assetDTO.DepreciationExpGLAccount.Value
                } : new KeyValueViewModel(),
            };

            vm.ProductSKU = new KeyValueViewModel()
            {
                Key = assetDTO.AssetProductMapDTOs?.FirstOrDefault()?.ProductSKUMapID?.ToString(),
                Value = assetDTO.AssetProductMapDTOs?.FirstOrDefault()?.ProductSKUName,
            };

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssetViewModel, AssetDTO>.CreateMap();
            var dto = Mapper<AssetViewModel, AssetDTO>.Map(this);

            dto.AssetIID = this.AssetIID;
            dto.AssetTypeID = string.IsNullOrEmpty(this.AssetType) ? (int?)null : int.Parse(this.AssetType);
            dto.AssetGroupID = string.IsNullOrEmpty(this.AssetGroup) ? (int?)null : int.Parse(this.AssetGroup);
            dto.AssetCategoryID = string.IsNullOrEmpty(this.AssetCategory) ? (long?)null : long.Parse(this.AssetCategory);
            dto.AssetSubCategoryID = string.IsNullOrEmpty(this.AssetSubCategory) ? (long?)null : long.Parse(this.AssetSubCategory);
            dto.UnitID = string.IsNullOrEmpty(this.Unit) ? (long?)null : long.Parse(this.Unit);
            dto.AssetCode = this.AssetCode;
            dto.Description = this.Description;
            dto.AssetPrefix = this.AssetPrefix;
            dto.LastSequenceNumber = this.LastSequenceNumber;
            dto.IsRequiredSerialNumber = this.IsRequiredSerialNumber;
            dto.AssetCategoryPrefix = this.AssetCategoryPrefix;

            dto.AssetGlAccID = this.AccountInformations != null && this.AccountInformations.GLAccount != null && !string.IsNullOrEmpty(this.AccountInformations.GLAccount.Key) ? long.Parse(this.AccountInformations.GLAccount.Key) : (long?)null;
            dto.AccumulatedDepGLAccID = this.AccountInformations != null && this.AccountInformations.DepreciationGLAccount != null && !string.IsNullOrEmpty(this.AccountInformations.DepreciationGLAccount.Key) ? long.Parse(this.AccountInformations.DepreciationGLAccount.Key) : (long?)null;
            dto.DepreciationExpGLAccID = this.AccountInformations != null && this.AccountInformations.ExpenseGLAccount != null && !string.IsNullOrEmpty(this.AccountInformations.ExpenseGLAccount.Key) ? long.Parse(this.AccountInformations.ExpenseGLAccount.Key) : (long?)null;

            dto.AssetProductMapDTOs = new List<AssetProductMapDTO>();

            if (!string.IsNullOrEmpty(this.ProductSKU.Key))
            {
                dto.AssetProductMapDTOs.Add(new AssetProductMapDTO()
                {
                    AssetProductMapIID = 0,
                    AssetID = this.AssetIID,
                    ProductSKUMapID = long.Parse(this.ProductSKU.Key)
                });
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetDTO>(jsonString);
        }

    }
}
