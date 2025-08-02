using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Setting.Settings;

namespace Eduegate.Web.Library.Setting.Settings
{
    public class MenuLinkViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("MenuLinkIID")]
        public long  MenuLinkIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxEditable)]
        [DisplayName("ParentMenuID")]
        public long?  ParentMenuID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MenuName")]
        public string  MenuName { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MenuLinkTypeID")]
        public byte?  MenuLinkTypeID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ActionType")]
        public string  ActionType { get; set; }
       
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ActionLink")]
        public string  ActionLink { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ActionLink1")]
        public string  ActionLink1 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ActionLink2")]
        public string  ActionLink2 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("ActionLink3")]
        public string  ActionLink3 { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Parameters")]
        public string  Parameters { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SortOrder")]
        public int?  SortOrder { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MenuTitle")]
        public string  MenuTitle { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MenuIcon")]
        public string  MenuIcon { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CompanyID")]
        public int?  CompanyID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SiteID")]
        public int?  SiteID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("MenuGroup")]
        public string  MenuGroup { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MenuLinkDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MenuLinkViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MenuLinkDTO, MenuLinkViewModel>.CreateMap();
            var vm = Mapper<MenuLinkDTO, MenuLinkViewModel>.Map(dto as MenuLinkDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MenuLinkViewModel, MenuLinkDTO>.CreateMap();
            var dto = Mapper<MenuLinkViewModel, MenuLinkDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MenuLinkDTO>(jsonString);
        }

    }
}