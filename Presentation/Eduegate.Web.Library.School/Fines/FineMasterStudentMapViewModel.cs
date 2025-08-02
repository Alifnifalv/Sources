using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fines
{
    public class FineMasterStudentMapViewModel : BaseMasterViewModel
    {
       
        public long FineMasterStudentMapIID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", false, "")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [DisplayName("Student Name")]
        public long? StudentId { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Fine Master")]
        [LookUp("LookUps.FineMaster")]
        public string FineMasterName { get; set; }
        public int? FineMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Fine Map Date")]
        public DateTime? FineMapDate { get; set; }

        //[Required]
     
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        //[Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Remark")]
        //[StringLength(250)]
        public string Remarks { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FineMasterStudentMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterStudentMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FineMasterStudentMapDTO, FineMasterStudentMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as FineMasterStudentMapDTO;
            var vm = Mapper<FineMasterStudentMapDTO, FineMasterStudentMapViewModel>.Map(feeDto);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FineMasterStudentMapViewModel, FineMasterStudentMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<FineMasterStudentMapViewModel, FineMasterStudentMapDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterStudentMapDTO>(jsonString);
        }

        private class MaxlegthAttribute : Attribute
        {
        }
    }
}