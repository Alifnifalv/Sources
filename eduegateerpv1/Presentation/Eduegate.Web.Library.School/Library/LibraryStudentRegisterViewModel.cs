using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryStudentRegisterViewModel : BaseMasterViewModel
    {
        public LibraryStudentRegisterViewModel()
        {
            //Student = new KeyValueViewModel();
        }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("LibraryStudentRegisterIID")]
        public long  LibraryStudentRegisterIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false)]
        [DisplayName("Student")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Registration Date")]
        public string RegistrationDateString { get; set; }

        public System.DateTime?  RegistrationDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Library Card No.")]
        public string LibraryCardNumber { get; set; }
        
        //[Required]
        [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Notes")]
        public string  Notes { get; set; }
        
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryStudentRegisterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryStudentRegisterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryStudentRegisterDTO, LibraryStudentRegisterViewModel>.CreateMap();
            var studentDto = dto as LibraryStudentRegisterDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LibraryStudentRegisterDTO, LibraryStudentRegisterViewModel>.Map(studentDto);
            vm.RegistrationDateString = studentDto.RegistrationDate.HasValue ? studentDto.RegistrationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Student = new KeyValueViewModel() { Key = studentDto.StudentID.ToString(), Value = studentDto.StudentName };
            vm.LibraryCardNumber = studentDto.LibraryCardNumber;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryStudentRegisterViewModel, LibraryStudentRegisterDTO>.CreateMap();
            var dto = Mapper<LibraryStudentRegisterViewModel, LibraryStudentRegisterDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.RegistrationDate = string.IsNullOrEmpty(this.RegistrationDateString) ? (DateTime?)null : DateTime.ParseExact(this.RegistrationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.LibraryCardNumber = this.LibraryCardNumber;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryStudentRegisterDTO>(jsonString);
        }
    }
}

