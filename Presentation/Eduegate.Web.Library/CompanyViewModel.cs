using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library
{
    public class CompanyViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Company ID")]
        public long CompanyID { get; set; }

        [Required]
        [DisplayName("Company Name")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string CompanyName { get; set; }

        public Nullable<int> CountryID { get; set; }
        public Nullable<int> BaseCurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        [Required]
        [DisplayName("Registration No")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string RegistraionNo { get; set; }
        [Required]
        [DisplayName("Registration Date")]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        [Required]
        [DisplayName("Registration expiry date")]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Currency")]
        [DisplayName("Currency")]
        public string Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Language")]
        [DisplayName("Language")]
        public string Language { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Country")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.CompanyStatus")]
        [DisplayName("Status")]
        public string CompanyStatus { get; set; } 
        public byte StatusID { get; set; }

        [Required]
        [DisplayName("Company Address")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]

        public string Address { get; set; }

        public string LogoUrl { get; set; }

        [Required]
        [DisplayName("Logo")]
        [FileUploadInfo("Mutual/UploadImages", EduegateImageTypes.CompanyLogo, "LogoUrl", "")]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        public string Logo { get; set; }

        public static List<KeyValueViewModel> FromDTO(List<CompanyDTO> dtos)
        {
            var vms = new List<KeyValueViewModel>();

            foreach (var dto in dtos)
            {
                vms.Add(new KeyValueViewModel() { Value = dto.CompanyName, Key = dto.CompanyID.ToString() });
            }

            return vms;
        }

        public static CompanyViewModel FromDTO(CompanyDTO dto)
        {
            Mapper<CompanyDTO, CompanyViewModel>.CreateMap();
            var mapper = Mapper<CompanyDTO, CompanyViewModel>.Map(dto);
            mapper.Country = mapper.CountryID.ToString();
            mapper.Language = mapper.LanguageID.ToString();
            mapper.Currency = mapper.BaseCurrencyID.ToString();
            mapper.CompanyStatus = mapper.StatusID.ToString();
            return mapper;
        }

        public static List<CompanyDTO> ToDTO(List<CompanyViewModel> vms)
        {
            var dtos = new List<CompanyDTO>();

            foreach (var vm in vms)
            {
                dtos.Add(ToDTO(vm));
            }

            return dtos;
        }

        public static CompanyDTO ToDTO(CompanyViewModel vm)
        {
            Mapper<CompanyViewModel, CompanyDTO>.CreateMap();
            var mapper = Mapper<CompanyViewModel, CompanyDTO>.Map(vm);
            mapper.BaseCurrencyID = int.Parse(vm.Currency);
            mapper.LanguageID = int.Parse(vm.Language);
            mapper.CountryID = int.Parse(vm.Country);
            mapper.StatusID = Convert.ToByte(vm.CompanyStatus);
            return mapper;
        }
    }
}
