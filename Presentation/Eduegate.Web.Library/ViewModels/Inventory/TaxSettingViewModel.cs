using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class TaxSettingViewModel : BaseMasterViewModel
    {
        public TaxSettingViewModel()
        {
            IsActive = true;
            IsDefault = false;
            HasTaxInclusive = false;
            TemplateItems = new List<TaxTemplateItemsViewModel>() { new TaxTemplateItemsViewModel() };
        }

        public int TaxTemplateID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Template Name")]
        [MaxLength(255, ErrorMessage = "Maximum Length should be within 255!")]
        public string TemplateName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Tax included with selling price")]
        public bool HasTaxInclusive { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Default")]
        public bool IsDefault { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Items")]
        public List<TaxTemplateItemsViewModel> TemplateItems { get; set; }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TaxSettingViewModel, TaxSettingDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<TaxTemplateItemsViewModel, TaxTemplateItemsDTO>.CreateMap();
            var mapper = Mapper<TaxSettingViewModel, TaxSettingDTO>.Map(this);
            return mapper;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            return FromDTO(dto as TaxSettingDTO);
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TaxSettingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TaxSettingViewModel>(jsonString);
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TaxSettingDTO>(jsonString);
        }

        public static TaxSettingViewModel FromDTO(TaxSettingDTO dto)
        {
            Mapper<TaxSettingDTO, TaxSettingViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<TaxTemplateItemsDTO, TaxTemplateItemsViewModel>.CreateMap();
            return Mapper<TaxSettingDTO, TaxSettingViewModel>.Map(dto);
        }
    }
}
