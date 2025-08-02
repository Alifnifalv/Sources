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
    public class ViewColumnViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("ViewColumnID")]
        public long ViewColumnID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewID")]
        public long? ViewID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ColumnName")]
        public string ColumnName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("DataType")]
        public string DataType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("PhysicalColumnName")]
        public string PhysicalColumnName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsDefault")]
        public bool? IsDefault { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsVisible")]
        public bool? IsVisible { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsSortable")]
        public bool? IsSortable { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsQuickSearchable")]
        public bool? IsQuickSearchable { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SortOrder")]
        public int? SortOrder { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsExpression")]
        public bool? IsExpression { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Expression")]
        public string Expression { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("FilterValue")]
        public string FilterValue { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ViewColumnDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ViewColumnViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ViewColumnDTO, ViewColumnViewModel>.CreateMap();
            var vm = Mapper<ViewColumnDTO, ViewColumnViewModel>.Map(dto as ViewColumnDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ViewColumnViewModel, ViewColumnDTO>.CreateMap();
            var dto = Mapper<ViewColumnViewModel, ViewColumnDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ViewColumnDTO>(jsonString);
        }

    }
}