using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Mutual
{
    public class CompanyViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("CompanyID")]
        public int  CompanyID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Company name")]
        public string  CompanyName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CompanyGroup")]
        [DisplayName("Company group")]
        public string CompanyGroup { get; set; }

        public int?  CompanyGroupID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Country")]
        public int?  CountryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Currency")]
        [DisplayName("Currency")]
        public string Currency { get; set; }

        public int?  BaseCurrencyID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Language")]
        [DisplayName("Language")]
        public string Language { get; set; }

        public int?  LanguageID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Registraion no")]
        public string  RegistraionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Registration date")]
        public System.DateTime?  RegistrationDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Expiry date")]
        public System.DateTime?  ExpiryDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Address")]
        public string  Address { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CompanyStatus")]
        [DisplayName("Status")]
        public string CompanyStatus { get; set; }

        public byte?  StatusID { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CompanyDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CompanyViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CompanyDTO, CompanyViewModel>.CreateMap();
            var vm = Mapper<CompanyDTO, CompanyViewModel>.Map(dto as CompanyDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CompanyViewModel, CompanyDTO>.CreateMap();
            var dto = Mapper<CompanyViewModel, CompanyDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CompanyDTO>(jsonString);
        }
    }
}

