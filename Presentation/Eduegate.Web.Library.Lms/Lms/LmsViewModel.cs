using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.Lms.Lms
{
    //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Signup", "CRUDModel.ViewModel")]
    //[DisplayName("Signup")]
    public class LmsViewModel : BaseMasterViewModel
    {
        public LmsViewModel()
        {
            SignupSlotMaps = new List<LmsSlotMapViewModel>() { new LmsSlotMapViewModel() };
            SignupFilter = new LmsFilterViewModel();
            Class = new KeyValueViewModel();
            SignupPublishedStatusID = new Domain.Setting.SettingBL().GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);
            IsActive = true;
            IsSendNotification = false;
            IsSlotShowToUser = false;
            SignupMeetingRequestTypeID = new Domain.Setting.SettingBL().GetSettingValue<byte>("SIGNUP_TYPEID_MEETING_REQUEST", 2);
        }

        public long SignupIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("Name")]
        public string SignupName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='GroupChanges(CRUDModel.ViewModel)' ng-disabled=CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID")]
        [CustomDisplay("Group")]
        [LookUp("LookUps.SignupGroups")]
        public string SignupGroup { get; set; }
        public int? SignupGroupID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("GroupDateFrom")]
        public string GroupDateFromString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("GroupDateTo")]
        public string GroupDateToString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change=SignupTypeChanges(CRUDModel.ViewModel) ng-disabled=CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID")]
        [CustomDisplay("Type")]
        [LookUp("LookUps.SignupTypes")]
        public string SignupType { get; set; }
        public byte? SignupTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("OrganizerEmployee", "String", false, "", optionalAttribute1: "ng-disabled='(CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID) || (CRUDModel.ViewModel.SignupType == CRUDModel.ViewModel.SignupMeetingRequestTypeID && CRUDModel.ViewModel.OrganizerEmployee.Key)'")]
        [CustomDisplay("OrganizerEmployee")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        public KeyValueViewModel OrganizerEmployee { get; set; }
        public long? OrganizerEmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID")]
        [CustomDisplay("Category")]
        [LookUp("LookUps.SignupCategories")]
        public string SignupCategory { get; set; }
        public byte? SignupCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.SignupStatuses")]
        public string SignupStatus { get; set; }
        public byte? SignupStatusID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2, "")]
        [Select2("Class", "String", false, "ClassSectionChanges(CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("Class")]
        [LookUp("LookUps.Classes")]
        public KeyValueViewModel Class { get; set; }
        public int? ClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='ClassSectionChanges(CRUDModel.ViewModel)' ng-disabled=CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID")]
        [CustomDisplay("Section")]
        [LookUp("LookUps.Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "")]
        [Select2("Student", "String", false, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.SignupOldStatusID==CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("Student")]
        [LookUp("LookUps.Students")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSlotShowToUser")]
        public bool? IsSlotShowToUser { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LocationInfo")]
        public string LocationInfo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Message")]
        public string Message { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendNotification")]
        public bool? IsSendNotification { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change='DateChanges(CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("FromDate")]
        public string FromDateString { get; set; }
        public DateTime? DateFrom { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change='DateChanges(CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.SignupOldStatusID == CRUDModel.ViewModel.SignupPublishedStatusID'")]
        [CustomDisplay("ToDate")]
        public string ToDateString { get; set; }
        public DateTime? DateTo { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [CustomDisplay("AutoFill")]
        public LmsFilterViewModel SignupFilter { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        public byte? SignupOldStatusID { get; set; }
        public byte? SignupPublishedStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("SlotDetails")]
        public List<LmsSlotMapViewModel> SignupSlotMaps { get; set; }

        public byte? SignupMeetingRequestTypeID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SignUpDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LmsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SignUpDTO, LmsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var signDTO = dto as SignUpDTO;
            var vm = Mapper<SignUpDTO, LmsViewModel>.Map(dto as SignUpDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            vm.SignupName = signDTO.SignupName;
            vm.IsActive = signDTO.IsActive ?? false;
            vm.SignupGroup = signDTO.SignupGroupID.HasValue ? signDTO.SignupGroupID.ToString() : null;
            vm.SignupStatus = signDTO.SignupStatusID.HasValue ? signDTO.SignupStatusID.ToString() : null;
            vm.SignupCategory = signDTO.SignupCategoryID.HasValue ? signDTO.SignupCategoryID.ToString() : null;
            vm.SignupType = signDTO.SignupTypeID.HasValue ? signDTO.SignupTypeID.ToString() : null;
            vm.Class = signDTO.ClassID.HasValue ? new KeyValueViewModel()
            {
                Key = signDTO.ClassID.ToString(),
                Value = signDTO.ClassName
            } : new KeyValueViewModel();
            vm.OrganizerEmployee = signDTO.OrganizerEmployeeID.HasValue ? new KeyValueViewModel()
            {
                Key = signDTO.OrganizerEmployeeID.ToString(),
                Value = signDTO.OrganizerEmployeeName
            } : new KeyValueViewModel();
            vm.Section = signDTO.SectionID.HasValue ? signDTO.SectionID.ToString() : null;
            vm.FromDateString = signDTO.DateFrom.HasValue ? signDTO.DateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = signDTO.DateTo.HasValue ? signDTO.DateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.Remarks = signDTO.Remarks;
            vm.Message = signDTO.Message;
            vm.LocationInfo = signDTO.LocationInfo;
            vm.SignupOldStatusID = signDTO.SignupOldStatusID;
            vm.GroupDateFromString = signDTO.GroupDateFrom.HasValue ? signDTO.GroupDateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.GroupDateToString = signDTO.GroupDateTo.HasValue ? signDTO.GroupDateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.IsSendNotification = signDTO.IsSendNotification;
            vm.Student = signDTO.StudentID.HasValue ? new KeyValueViewModel()
            {
                Key = signDTO.StudentID.ToString(),
                Value = signDTO.StudentName
            } : new KeyValueViewModel();
            vm.IsSlotShowToUser = signDTO.IsSlotShowToUser ?? false;

            vm.SignupSlotMaps = new List<LmsSlotMapViewModel>();
            foreach (var map in signDTO.SignupSlotMaps)
            {
                vm.SignupSlotMaps.Add(new LmsSlotMapViewModel
                {
                    SignupSlotMapIID = map.SignupSlotMapIID,
                    SignupSlotType = map.SignupSlotTypeID.ToString(),
                    SlotMapStatus = map.SlotMapStatusID.ToString(),
                    SlotDateString = map.SlotDate.HasValue ? map.SlotDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    StartTimeString = map.StartTime.HasValue ? DateTime.Parse(map.StartTime.Value.ToString()).ToString(timeFormat) : null,
                    EndTimeString = map.EndTime.HasValue ? DateTime.Parse(map.EndTime.Value.ToString()).ToString(timeFormat) : null,
                    Duration = map.Duration,
                    CreatedBy = map.CreatedBy,
                    UpdatedBy = map.UpdatedBy,
                    CreatedDate = map.CreatedDate,
                    UpdatedDate = map.UpdatedDate,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LmsViewModel, SignUpDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<LmsViewModel, SignUpDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            dto.IsActive = this.IsActive ?? false;
            dto.SignupGroupID = string.IsNullOrEmpty(this.SignupGroup) ? (int?)null : int.Parse(this.SignupGroup);
            dto.SignupStatusID = string.IsNullOrEmpty(this.SignupStatus) ? (byte?)null : byte.Parse(this.SignupStatus);
            dto.SignupCategoryID = string.IsNullOrEmpty(this.SignupCategory) ? (byte?)null : byte.Parse(this.SignupCategory);
            dto.SignupTypeID = string.IsNullOrEmpty(this.SignupType) ? (byte?)null : byte.Parse(this.SignupType);
            dto.ClassID = this.Class != null && !string.IsNullOrEmpty(this.Class.Key) ? (int?)long.Parse(this.Class.Key) : (int?)null;
            dto.SectionID = string.IsNullOrEmpty(this.Section) ? (int?)null : int.Parse(this.Section);
            dto.OrganizerEmployeeID = this.OrganizerEmployee != null && !string.IsNullOrEmpty(this.OrganizerEmployee.Key) ? long.Parse(this.OrganizerEmployee.Key) : (long?)null;
            dto.DateFrom = string.IsNullOrEmpty(this.FromDateString) ? (DateTime?)null : DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateTo = string.IsNullOrEmpty(this.ToDateString) ? (DateTime?)null : DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.Remarks = this.Remarks;
            dto.Message = this.Message;
            dto.SignupName = this.SignupName;
            dto.LocationInfo = this.LocationInfo;
            dto.IsSendNotification = this.IsSendNotification;
            dto.StudentID = this.Student != null && !string.IsNullOrEmpty(this.Student.Key) ? long.Parse(this.Student.Key) : (long?)null;
            dto.IsSlotShowToUser = this.IsSlotShowToUser ?? false;

            dto.SignupSlotMaps = new List<SignupSlotMapDTO>();
            foreach (var map in this.SignupSlotMaps)
            {
                if (!string.IsNullOrEmpty(map.SlotDateString))
                {
                    dto.SignupSlotMaps.Add(new SignupSlotMapDTO
                    {
                        SignupSlotMapIID = map.SignupSlotMapIID,
                        SignupSlotTypeID = byte.Parse(map.SignupSlotType),
                        SlotDate = string.IsNullOrEmpty(map.SlotDateString) ? (DateTime?)null : DateTime.ParseExact(map.SlotDateString, dateFormat, CultureInfo.InvariantCulture),
                        StartTime = string.IsNullOrEmpty(map.StartTimeString) ? (TimeSpan?)null : DateTime.Parse(map.StartTimeString).TimeOfDay,
                        EndTime = string.IsNullOrEmpty(map.EndTimeString) ? (TimeSpan?)null : DateTime.Parse(map.EndTimeString).TimeOfDay,
                        Duration = map.Duration,
                        SlotMapStatusID = string.IsNullOrEmpty(map.SlotMapStatus) ? (byte?)null : byte.Parse(map.SlotMapStatus),
                        CreatedBy = map.CreatedBy,
                        UpdatedBy = map.UpdatedBy,
                        CreatedDate = map.CreatedDate,
                        UpdatedDate = map.UpdatedDate,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SignUpDTO>(jsonString);
        }

    }
}