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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetCategory", "CRUDModel.ViewModel")]
    [DisplayName("Asset Category")]
    public class AssetCategoryViewModel : BaseMasterViewModel
    {
        public AssetCategoryViewModel()
        {
            IsActive = true;
        }

        public long AssetCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Name")]
        public string CategoryName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Rate of Depreciation (Percentage)")]
        public decimal? DepreciationRate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Category Prefix")]
        public string CategoryPrefix { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Last Sequence Number")]
        public long? LastSequenceNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        public int? DepreciationPeriodID { get; set; }
        public string OldCategoryPrefix { get; set; }

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
            Mapper<AssetCategoryDTO, AssetCategoryViewModel>.CreateMap();
            var catDTO = dto as AssetCategoryDTO;
            var vm = Mapper<AssetCategoryDTO, AssetCategoryViewModel>.Map(dto as AssetCategoryDTO);

            vm.AssetCategoryID = catDTO.AssetCategoryID;
            vm.CategoryName = catDTO.CategoryName;
            vm.IsActive = catDTO.IsActive;
            vm.DepreciationRate = catDTO.DepreciationRate;
            vm.DepreciationPeriodID = catDTO.DepreciationPeriodID;
            vm.CategoryPrefix = catDTO.CategoryPrefix;
            vm.OldCategoryPrefix = catDTO.OldCategoryPrefix;
            vm.LastSequenceNumber = catDTO.LastSequenceNumber;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssetCategoryViewModel, AssetCategoryDTO>.CreateMap();
            var dto = Mapper<AssetCategoryViewModel, AssetCategoryDTO>.Map(this);

            dto.AssetCategoryID = this.AssetCategoryID;
            dto.CategoryName = this.CategoryName;
            dto.IsActive = this.IsActive;
            dto.DepreciationRate = this.DepreciationRate;
            dto.DepreciationPeriodID = this.DepreciationPeriodID;
            dto.CategoryPrefix = this.CategoryPrefix;
            dto.OldCategoryPrefix = this.OldCategoryPrefix;
            dto.LastSequenceNumber = this.LastSequenceNumber;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetCategoryDTO>(jsonString);
        }

    }
}