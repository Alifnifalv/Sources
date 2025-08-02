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
    public class ViewViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("ViewID")]
        public long ViewID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewTypeID")]
        public byte? ViewTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewName")]
        public string ViewName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewFullPath")]
        public string ViewFullPath { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsMultiLine")]
        public bool? IsMultiLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsRowCategory")]
        public bool? IsRowCategory { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("PhysicalSchemaName")]
        public string PhysicalSchemaName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("HasChild")]
        public bool? HasChild { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsRowClickForMultiSelect")]
        public bool? IsRowClickForMultiSelect { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ChildViewID")]
        public long? ChildViewID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ChildFilterField")]
        public string ChildFilterField { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ControllerName")]
        public string ControllerName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsMasterDetail")]
        public bool? IsMasterDetail { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsEditable")]
        public bool? IsEditable { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsGenericCRUDSave")]
        public bool? IsGenericCRUDSave { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsReloadSummarySmartViewAlways")]
        public bool? IsReloadSummarySmartViewAlways { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("JsControllerName")]
        public string JsControllerName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ViewDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ViewViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ViewDTO, ViewViewModel>.CreateMap();
            var vm = Mapper<ViewDTO, ViewViewModel>.Map(dto as ViewDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ViewViewModel, ViewDTO>.CreateMap();
            var dto = Mapper<ViewViewModel, ViewDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ViewDTO>(jsonString);
        }

    }
}