using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Transports
{
    public class StudentPickupRequestViewModel : BaseMasterViewModel
    {
        public StudentPickupRequestViewModel()
        {
            Student = new KeyValueViewModel();
            RequestStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_PICKUP_STATUS_NEW");
        }

        public long StudentPickupRequestIID { get; set; }
       
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("PickDate")]
        public string PickedDateString { get; set; }
        public DateTime? PickedDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker, attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("FromTime")]
        public string FromTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TimePicker, attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("ToTime")]
        public string ToTimeString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "StudentChanges(CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }
    
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", attribs: "ng-change=PickedByChanges(CRUDModel.ViewModel) ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [LookUp("LookUps.StudentPickedBy")]
        [CustomDisplay("PickedBy")]
        public string PickedBy { get; set; }
        public byte? PickedByID { get; set; }
        public KeyValueViewModel PickedByVM { get; set; }



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "texlleft", attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("FirstName")]
        public string FirstName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "texlleft", attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("MiddleName")]
        public string MiddleName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "texlleft", attribs: "ng-disabled=CRUDModel.ViewModel.StudentPickupRequestIID != 0")]
        [CustomDisplay("LastName")]
        public string LastName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("AdditionalDetails")]
        public string AdditionalInfo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StudentPickupRequestStatus")]
        [CustomDisplay("RequestStatus")]
        public string RequestStatus { get; set; }
        public byte? RequestStatusID { get; set; }

        public string RequestDateString { get; set; }
        public DateTime? RequestDate { get; set; }

        public string RequestCode { get; set; }

        public byte[] RequestImageCode { get; set; }

        public long? PhotoContentID { get; set; }

        public string QRCode { get; set; }

        public string ClassSection { get; set; }

        public long StudentPickerStudentMapIID { get; set; }

        public long ParentID { get; set; }
        

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentPickupRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentPickupRequestViewModel>(jsonString);
        }

        public StudentPickupRequestViewModel ToVM(StudentPickupRequestDTO dto)
        {
            Mapper<StudentPickupRequestDTO, StudentPickupRequestViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var requestDTO = dto as StudentPickupRequestDTO;
            var vm = Mapper<StudentPickupRequestDTO, StudentPickupRequestViewModel>.Map(requestDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            vm.StudentPickupRequestIID = Convert.ToInt64(requestDTO.StudentPickupRequestIID);
            vm.StudentID = requestDTO.StudentID;
            vm.Student = requestDTO.StudentID.HasValue ? new KeyValueViewModel() { Key = requestDTO.Student.Key, Value = requestDTO.Student.Value } : null;
            vm.RequestStatusID = requestDTO.RequestStatusID;
            vm.RequestStatus = requestDTO.RequestStatusID.HasValue ? requestDTO.RequestStatusID.ToString() : null;
            vm.PickedByID = requestDTO.PickedByID;
            vm.PickedBy = requestDTO.PickedByID.HasValue ? requestDTO.PickedByID.ToString() : null;
            vm.RequestDateString = requestDTO.RequestDate.HasValue ? requestDTO.RequestDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PickedDateString = requestDTO.PickedDate.HasValue ? requestDTO.PickedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.RequestDate = requestDTO.RequestDate;
            vm.FirstName = requestDTO.FirstName;
            vm.MiddleName = requestDTO.MiddleName;
            vm.LastName = requestDTO.LastName;
            vm.PhotoContentID = requestDTO.PhotoContentID;
            vm.RequestImageCode = requestDTO.RequestCodeImage;
            vm.RequestCode = requestDTO.RequestCode;
            vm.FromTimeString = requestDTO.FromTime.HasValue ? DateTime.Today.Add(requestDTO.FromTime.Value).ToString(timeFormat) : null;
            vm.ToTimeString = requestDTO.ToTime.HasValue ? DateTime.Today.Add(requestDTO.ToTime.Value).ToString(timeFormat) : null;

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentPickupRequestDTO, StudentPickupRequestViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var requestDTO = dto as StudentPickupRequestDTO;
            var vm = Mapper<StudentPickupRequestDTO, StudentPickupRequestViewModel>.Map(requestDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            vm.StudentPickupRequestIID = Convert.ToInt64(requestDTO.StudentPickupRequestIID);
            vm.StudentID = requestDTO.StudentID;
            vm.Student = requestDTO.StudentID.HasValue ? new KeyValueViewModel() { Key = requestDTO.Student.Key, Value = requestDTO.Student.Value } : null;
            vm.RequestStatusID = requestDTO.RequestStatusID;
            vm.RequestStatus = requestDTO.RequestStatusID.HasValue ? requestDTO.RequestStatusID.ToString() : null;
            vm.PickedBy = requestDTO.PickedByID.HasValue ? requestDTO.PickedByID.ToString() : null;
            vm.RequestDateString = requestDTO.RequestDate.HasValue ? requestDTO.RequestDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.PickedDateString = requestDTO.PickedDate.HasValue ? requestDTO.PickedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.RequestDate = requestDTO.RequestDate;
            vm.FirstName = requestDTO.FirstName;
            vm.MiddleName = requestDTO.MiddleName;
            vm.LastName = requestDTO.LastName;
            vm.PhotoContentID = requestDTO.PhotoContentID;
            vm.RequestImageCode = requestDTO.RequestCodeImage;
            vm.RequestCode = requestDTO.RequestCode;
            vm.FromTimeString = requestDTO.FromTime.HasValue ? DateTime.Today.Add(requestDTO.FromTime.Value).ToString(timeFormat) : null;
            vm.ToTimeString = requestDTO.ToTime.HasValue ? DateTime.Today.Add(requestDTO.ToTime.Value).ToString(timeFormat) : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentPickupRequestViewModel, StudentPickupRequestDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<StudentPickupRequestViewModel, StudentPickupRequestDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.StudentPickupRequestIID = Convert.ToInt64(this.StudentPickupRequestIID);
            dto.StudentID = this.StudentID.HasValue ? this.StudentID : string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.RequestStatusID = string.IsNullOrEmpty(this.RequestStatus) ? (byte?)null : byte.Parse(this.RequestStatus);
            dto.PickedByID = this.PickedByID.HasValue ? this.PickedByID : string.IsNullOrEmpty(this.PickedBy) ? (byte?)null : byte.Parse(this.PickedBy);
            dto.RequestDate = string.IsNullOrEmpty(this.RequestDateString) ? (DateTime?)null : DateTime.ParseExact(this.RequestDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.PickedDate = string.IsNullOrEmpty(this.PickedDateString) ? (DateTime?)null : DateTime.ParseExact(this.PickedDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FirstName = this.FirstName;
            dto.MiddleName = this.MiddleName;
            dto.LastName = this.LastName;
            dto.PhotoContentID = this.PhotoContentID;
            dto.RequestCodeImage = this.RequestImageCode;
            dto.RequestCode = this.RequestCode;
            dto.FromTime = string.IsNullOrEmpty(this.FromTimeString) ? (TimeSpan?)null : DateTime.Parse(this.FromTimeString).TimeOfDay;
            dto.ToTime = string.IsNullOrEmpty(this.ToTimeString) ? (TimeSpan?)null : DateTime.Parse(this.ToTimeString).TimeOfDay;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentPickupRequestDTO>(jsonString);
        }

    }
}