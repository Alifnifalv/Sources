using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Accounts.Assets;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetDepreciation", "CRUDModel.ViewModel")]
    [DisplayName("Asset Depreciation")]
    public class AssetDepreciationEntryViewModel : BaseMasterViewModel
    {
        public AssetDepreciationEntryViewModel()
        {
        }

        public long AssetDepreciationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Asset", "String", false)]
        [CustomDisplay("Asset")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Assets", "LookUps.Assets")]
        public KeyValueViewModel Asset { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Number of Days in the Accounting Period")]
        public string AccountPeriodDays { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Dep. Accumulated Till Date")]
        public string AccumulatedTillDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Dep From")]
        public string DepreciationFrom { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Dep Till Date")]
        public string DepTillDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Dep for the above period")]
        public string BillNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Dep Booked")]
        public string BillDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Dep Provided till date")]
        public string FirstUseDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Net Value")]
        public string Quantity { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssetCategoryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetCategoryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssetDTO, AssetDepreciationEntryViewModel>.CreateMap();
            var assetDTO = dto as AssetDTO;
            var vm = Mapper<AssetDTO, AssetDepreciationEntryViewModel>.Map(dto as AssetDTO);

            vm.AssetDepreciationIID = assetDTO.AssetIID;
            //vm.AssetCategoryID = assetDTO.AssetCategoryID;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssetDepreciationEntryViewModel, AssetDTO>.CreateMap();
            var dto = Mapper<AssetDepreciationEntryViewModel, AssetDTO>.Map(this);

            dto.AssetIID = this.AssetDepreciationIID;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetCategoryDTO>(jsonString);
        }

    }
}