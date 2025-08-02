using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.Setting.Settings;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class FilterColumnViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("FilterColumnID")]
        public long FilterColumnID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SequenceNo")]
        public int? SequenceNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ViewID")]
        public long? ViewID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ColumnCaption")]
        public string ColumnCaption { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ColumnName")]
        public string ColumnName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("DataTypeID")]
        public byte? DataTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("UIControlTypeID")]
        public byte? UIControlTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("DefaultValues")]
        public string DefaultValues { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsQuickFilter")]
        public bool? IsQuickFilter { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("LookupID")]
        public int? LookupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Attribute1")]
        public string Attribute1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Attribute2")]
        public string Attribute2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("IsLookupLazyLoad")]
        public bool? IsLookupLazyLoad { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FilterColumnDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FilterColumnViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FilterColumnDTO, FilterColumnViewModel>.CreateMap();
            var vm = Mapper<FilterColumnDTO, FilterColumnViewModel>.Map(dto as FilterColumnDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FilterColumnViewModel, FilterColumnDTO>.CreateMap();
            var dto = Mapper<FilterColumnViewModel, FilterColumnDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FilterColumnDTO>(jsonString);
        }

    }
}