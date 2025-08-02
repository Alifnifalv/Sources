using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Setting.Settings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class ScreenMetadataViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("ScreenID")]
        public long ScreenID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewID")]
        public long? ViewID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ListActionName")]
        public string ListActionName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ListButtonDisplayName")]
        public string ListButtonDisplayName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ModelAssembly")]
        public string ModelAssembly { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ModelNamespace")]
        public string ModelNamespace { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ModelViewModel")]
        public string ModelViewModel { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MasterViewModel")]
        public string MasterViewModel { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("DetailViewModel")]
        public string DetailViewModel { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SummaryViewModel")]
        public string SummaryViewModel { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("DisplayName")]
        public string DisplayName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("JsControllerName")]
        public string JsControllerName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsCacheable")]
        public bool? IsCacheable { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsSavePanelRequired")]
        public bool? IsSavePanelRequired { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsGenericCRUDSave")]
        public bool? IsGenericCRUDSave { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("EntityMapperAssembly")]
        public string EntityMapperAssembly { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("EntityMapperViewModel")]
        public string EntityMapperViewModel { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SaveCRUDMethod")]
        public string SaveCRUDMethod { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ScreenTypeID")]
        public int? ScreenTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ScreenMetadataDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ScreenMetadataViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ScreenMetadataDTO, ScreenMetadataViewModel>.CreateMap();
            var vm = Mapper<ScreenMetadataDTO, ScreenMetadataViewModel>.Map(dto as ScreenMetadataDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ScreenMetadataViewModel, ScreenMetadataDTO>.CreateMap();
            var dto = Mapper<ScreenMetadataViewModel, ScreenMetadataDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ScreenMetadataDTO>(jsonString);
        }
    }
}

