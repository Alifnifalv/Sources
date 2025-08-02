using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System;
using System.Globalization;
using Eduegate.Framework.Enums;

namespace Eduegate.Web.Library.School.Mutual
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "School", "CRUDModel.ViewModel")]
    [DisplayName("School")]
    public class SchoolsViewModel : BaseMasterViewModel
    {

        public SchoolsViewModel()
        {
            SchoolBankDetails = new SchoolBankDetailsViewModel();
            PayerGrid = new List<SchoolBankPayerGridViewModel>() { new SchoolBankPayerGridViewModel() };
        }

        public byte  SchoolID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("SchoolName")]
        public string  SchoolName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(750, ErrorMessage = "Maximum Length should be within 750!")]
        [CustomDisplay("Description")]
        public string  Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Address1")]
        public string  Address1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [CustomDisplay("Address2")]
        public string  Address2 { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Registration")]
        public string  RegistrationID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Company")]
        [LookUp("LookUps.Company")]
        public string  Company { get; set; }

        public int? CompanyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Place")]
        public string Place { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SchoolCode")]
        public string SchoolCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Sponsors")]
        [LookUp("LookUps.SponsoredBy")]

        public string Sponsor { get; set; }
        public long? SponsorID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Short Name")]
        public string SchoolShortName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("SchoolProfile")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.UserProfile, "ProfileUrl", "")]
        public string ProfileUrl { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("SchoolSeal")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Social, "SignatureUrl", "")]
        public string SignatureUrl { get; set; }   
        
        
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("Logo")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.CompanyLogo, "LogoUrl", "")]
        public string LogoUrl { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SchoolBankDetails", "SchoolBankDetails")]
        [CustomDisplay("Bank Details")]
        public SchoolBankDetailsViewModel SchoolBankDetails { get; set; }

        public List<SchoolBankPayerGridViewModel> PayerGrid { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SchoolsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SchoolsDTO, SchoolsViewModel>.CreateMap();
            Mapper<SchoolsDTO, SchoolBankDetailsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var schoolDTO = dto as SchoolsDTO;
            var vm = Mapper<SchoolsDTO, SchoolsViewModel>.Map(schoolDTO);

            vm.SchoolID = schoolDTO.SchoolID;
            vm.Sponsor = schoolDTO.SponsorID.ToString();
            vm.Company = schoolDTO.CompanyID.ToString();
            vm.Place = schoolDTO.Place;
            vm.SchoolCode = schoolDTO.SchoolCode;
            vm.SchoolBankDetails.EmployerEID = schoolDTO.EmployerEID;
            vm.SchoolBankDetails.PayerEID = schoolDTO.PayerEID;
            vm.SchoolBankDetails.PayerQID = schoolDTO.PayerQID;
            vm.ProfileUrl = schoolDTO.ProfileContentID.HasValue ? schoolDTO.ProfileContentID.ToString() : null;
            vm.SignatureUrl = schoolDTO.SchoolSealContentID.HasValue ? schoolDTO.SchoolSealContentID.ToString() : null;
            vm.LogoUrl = schoolDTO.SchoolLogoContentID.HasValue ? schoolDTO.SchoolLogoContentID.ToString() : null;
            vm.SchoolShortName = schoolDTO.SchoolShortName;

            vm.PayerGrid = new List<SchoolBankPayerGridViewModel>();
            foreach (var bnk in schoolDTO.PayerBankDTO)
            {
                vm.PayerGrid.Add(new SchoolBankPayerGridViewModel()
                {
                    PayerBankDetailIID = bnk.PayerBankDetailIID,
                    Bank = bnk.BankID.HasValue ? new KeyValueViewModel() { Key = bnk.Bank.Key.ToString(), Value = bnk.Bank.Value } : new KeyValueViewModel(),
                    PayerIBAN = bnk.PayerIBAN,
                    IsMainOperating = bnk.IsMainOperating,
                    PayerBankShortName = bnk.PayerBankShortName,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SchoolsViewModel, SchoolsDTO>.CreateMap();
            Mapper<SchoolBankDetailsViewModel, SchoolsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SchoolsViewModel, SchoolsDTO>.Map(this);

            dto.SchoolID = this.SchoolID;
            dto.SponsorID= string.IsNullOrEmpty(this.Sponsor) ? (int?)null : int.Parse(this.Sponsor);
            dto.CompanyID = string.IsNullOrEmpty(this.Company) ? (int?)null : int.Parse(this.Company);
            dto.Place = this.Place;
            dto.SchoolShortName = this.SchoolShortName;
            dto.SchoolCode = this.SchoolCode;
            dto.EmployerEID = this.SchoolBankDetails.EmployerEID;
            dto.PayerEID = this.SchoolBankDetails?.PayerEID;
            dto.PayerQID = this.SchoolBankDetails?.PayerQID;
            dto.ProfileContentID = string.IsNullOrEmpty(this.ProfileUrl) ? (long?)null : long.Parse(this.ProfileUrl);
            dto.SchoolSealContentID = string.IsNullOrEmpty(this.SignatureUrl) ? (long?)null : long.Parse(this.SignatureUrl);
            dto.SchoolLogoContentID = string.IsNullOrEmpty(this.LogoUrl) ? (long?)null : long.Parse(this.LogoUrl);
            dto.PayerBankDTO = new List<SchoolPayerBankDTO>();

            foreach (var bnk in this.PayerGrid)
            {
                if (bnk.Bank != null)
                {
                    dto.PayerBankDTO.Add(new SchoolPayerBankDTO()
                    {
                        PayerBankDetailIID = bnk.PayerBankDetailIID,
                        BankID = bnk.Bank == null || string.IsNullOrEmpty(bnk.Bank.Key) ? (long?)null : long.Parse(bnk.Bank.Key),
                        IsMainOperating = bnk.IsMainOperating.HasValue ? bnk.IsMainOperating : false,
                        PayerIBAN = bnk.PayerIBAN,
                        PayerBankShortName = bnk.PayerBankShortName,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SchoolsDTO>(jsonString);
        }
    }
}

