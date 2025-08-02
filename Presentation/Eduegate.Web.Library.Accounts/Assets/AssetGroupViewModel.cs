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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetGroup", "CRUDModel.ViewModel")]
    [DisplayName("Asset Group")]
    public class AssetGroupViewModel : BaseMasterViewModel
    {
        public AssetGroupViewModel()
        {
            IsActive = true;
        }

        public int AssetGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Name")]
        public string AssetGroupName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AssetGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AssetGroupDTO, AssetGroupViewModel>.CreateMap();
            var grpDTO = dto as AssetGroupDTO;
            var vm = Mapper<AssetGroupDTO, AssetGroupViewModel>.Map(dto as AssetGroupDTO);

            vm.AssetGroupID = grpDTO.AssetGroupID;
            vm.AssetGroupName = grpDTO.AssetGroupName;
            vm.IsActive = grpDTO.IsActive;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AssetGroupViewModel, AssetGroupDTO>.CreateMap();
            var dto = Mapper<AssetGroupViewModel, AssetGroupDTO>.Map(this);

            dto.AssetGroupID = this.AssetGroupID;
            dto.AssetGroupName = this.AssetGroupName;
            dto.IsActive = this.IsActive;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AssetGroupDTO>(jsonString);
        }

    }
}
