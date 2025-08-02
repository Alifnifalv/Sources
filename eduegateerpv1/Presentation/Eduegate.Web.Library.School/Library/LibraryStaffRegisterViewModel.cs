using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Extensions;
using System.Globalization;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryStaffRegisterViewModel : BaseMasterViewModel
    {
        public LibraryStaffRegisterViewModel()
        {
            //Employee = new KeyValueViewModel();
        }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("LibraryStaffResiterIID")]
        public long  LibraryStaffResiterIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Employee", "String", false, "")]
        [DisplayName("Staff")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Employee", "LookUps.Employee")]
        public KeyValueViewModel Employee { get; set; }

        public long?  EmployeeID { get; set; }

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
            return JsonConvert.SerializeObject(vm as LibraryStaffRegisterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryStaffRegisterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryStaffRegisterDTO, LibraryStaffRegisterViewModel>.CreateMap();
            var staffDTO = dto as LibraryStaffRegisterDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LibraryStaffRegisterDTO, LibraryStaffRegisterViewModel>.Map(staffDTO);
            vm.RegistrationDateString = staffDTO.RegistrationDate.HasValue ? staffDTO.RegistrationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Employee = new KeyValueViewModel() { Key = staffDTO.EmployeeID.ToString(), Value = staffDTO.StaffName };
            vm.LibraryCardNumber = staffDTO.LibraryCardNumber;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryStaffRegisterViewModel, LibraryStaffRegisterDTO>.CreateMap();
            var dto = Mapper<LibraryStaffRegisterViewModel, LibraryStaffRegisterDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.RegistrationDate = string.IsNullOrEmpty(this.RegistrationDateString) ? (DateTime?)null : DateTime.ParseExact(this.RegistrationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (int?)null : int.Parse(this.Employee.Key);
            dto.LibraryCardNumber = this.LibraryCardNumber;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryStaffRegisterDTO>(jsonString);
        }
    }
}

