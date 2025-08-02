using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.FrontOffices;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.FrontOffices
{
    public class AdmissionEnquiryViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("AdmissionEnquiryIID")]
        public long  AdmissionEnquiryIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("PersonName")]
        public string  PersonName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        [DisplayName("PhoneNumber")]
        public string  PhoneNumber { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Email")]
        public string  Email { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Address")]
        public string  Address { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Note")]
        public string  Note { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [DisplayName("Date")]
        public System.DateTime?  Date { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("NextFollowUpDate")]
        public System.DateTime?  NextFollowUpDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Assinged")]
        public string  Assinged { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ReferenceTypeID")]
        public byte?  ReferenceTypeID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("SourceID")]
        public byte?  SourceID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ClassID")]
        public int?  ClassID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("NumberOfChild")]
        public int?  NumberOfChild { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CreatedDate")]
        public System.DateTime?  CreatedDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("UpdatedDate")]
        public System.DateTime?  UpdatedDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("CreatedBy")]
        public int?  CreatedBy { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("UpdatedBy")]
        public int?  UpdatedBy { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("TimeStamps")]
        public byte[]  TimeStamps { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("SchoolID")]
        public byte?  SchoolID { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as AdmissionEnquiryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<AdmissionEnquiryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<AdmissionEnquiryDTO, AdmissionEnquiryViewModel>.CreateMap();
            var vm = Mapper<AdmissionEnquiryDTO, AdmissionEnquiryViewModel>.Map(dto as AdmissionEnquiryDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<AdmissionEnquiryViewModel, AdmissionEnquiryDTO>.CreateMap();
            var dto = Mapper<AdmissionEnquiryViewModel, AdmissionEnquiryDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<AdmissionEnquiryDTO>(jsonString);
        }
    }
}

