using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.SignUp.Meeting
{
    public class MeetingRequestViewModel : BaseMasterViewModel
    {
        public MeetingRequestViewModel()
        {
            ApprovedMeetingRequestStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("MEETING_REQUEST_STATUS_APPROVED", 3);
            IsSendNotification = true;
        }

        public long MeetingRequestIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Faculty")]
        public string FacultyName { get; set; }
        public long? FacultyID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Parent")]
        public string ParentName { get; set; }
        public long? ParentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Remark")]
        public string RequesterRemark { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Requested Date")]
        public string RequestedDateString { get; set; }
        public DateTime? RequestedDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Requested Time")]
        public string RequestedSlotTime { get; set; }
        public long? RequestedSignupSlotMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MeetingRequestStatuses")]
        [CustomDisplay("Status")]
        public string MeetingRequestStatus { get; set; }
        public byte? MeetingRequestStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsSendNotification")]
        public bool? IsSendNotification { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "", attribs: "ng-change='SameDateBoxClick($event, $element,CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.MeetingRequestStatus != CRUDModel.ViewModel.ApprovedMeetingRequestStatusID'")]
        [CustomDisplay("Is Approval Date Same As Requested")]
        public bool IsDateSame { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "", attribs: "ng-change='SameTimeBoxClick($event, $element,CRUDModel.ViewModel)' ng-disabled='CRUDModel.ViewModel.MeetingRequestStatus != CRUDModel.ViewModel.ApprovedMeetingRequestStatusID'")]
        [CustomDisplay("Is Approval Time Same As Requested")]
        public bool IsTimeSame { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='CRUDModel.ViewModel.MeetingRequestStatus != CRUDModel.ViewModel.ApprovedMeetingRequestStatusID'")]
        [CustomDisplay("Approved Date")]
        public string ApprovedDateString { get; set; }
        public DateTime? ApprovedDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='FillTimeSlots(CRUDModel.ViewModel)'")]
        [CustomDisplay("Fill Slots")]
        public string FillSlots { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ApprovedSlotMap", "String", false, "", optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.MeetingRequestStatus != CRUDModel.ViewModel.ApprovedMeetingRequestStatusID'")]
        [LookUp("LookUps.AvailableMeetingSlots")]
        [CustomDisplay("Approved Time")]
        public KeyValueViewModel ApprovedSlotMap { get; set; }
        public long? ApprovedSignupSlotMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remark")]
        public string FacultyRemark { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public long? ParentLoginID { get; set; }
        public string GuardianEmailID { get; set; }

        public byte? OldMeetingRequestStatusID { get; set; }

        public string MeetingRequestStatusName { get; set; }

        public byte? ApprovedMeetingRequestStatusID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MeetingRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MeetingRequestViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MeetingRequestDTO, MeetingRequestViewModel>.CreateMap();
            var reqDTO = dto as MeetingRequestDTO;
            var vm = Mapper<MeetingRequestDTO, MeetingRequestViewModel>.Map(dto as MeetingRequestDTO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.MeetingRequestIID = reqDTO.MeetingRequestIID;
            vm.RequestedDateString = reqDTO.RequestedDate.HasValue ? reqDTO.RequestedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ApprovedDateString = reqDTO.ApprovedDate.HasValue ? reqDTO.ApprovedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StudentID = reqDTO.StudentID;
            vm.StudentName = reqDTO.Student.Value;
            vm.ParentID = reqDTO.ParentID;
            vm.ParentName = reqDTO.ParentName;
            vm.ParentLoginID = reqDTO.ParentLoginID;
            vm.GuardianEmailID = reqDTO.GuardianEmailID;
            vm.FacultyID = reqDTO.FacultyID;
            vm.FacultyName = reqDTO.Faculty.Value;
            vm.RequestedSignupSlotMapID = reqDTO.RequestedSignupSlotMapID;
            //vm.ApprovedSlotMap = reqDTO.ApprovedSignupSlotMapID.HasValue ? reqDTO.ApprovedSignupSlotMapID.ToString() : null;
            vm.ApprovedSlotMap = reqDTO.ApprovedSignupSlotMapID.HasValue ? new KeyValueViewModel()
            {
                Key = reqDTO.ApprovedSignupSlotMapID.ToString(),
                Value = reqDTO.ApprovedSlotTime
            } : new KeyValueViewModel();
            vm.RequestedSlotTime = reqDTO.RequestedSlotTime;
            vm.MeetingRequestStatus = reqDTO.MeetingRequestStatusID.HasValue ? reqDTO.MeetingRequestStatusID.ToString() : null;
            vm.ClassID = reqDTO.ClassID;
            vm.SectionID = reqDTO.SectionID;
            vm.SchoolID = reqDTO.SchoolID;
            vm.AcademicYearID = reqDTO.AcademicYearID;
            vm.OldMeetingRequestStatusID = reqDTO.OldMeetingRequestStatusID;
            vm.RequesterRemark = reqDTO.RequesterRemark;
            vm.FacultyRemark = reqDTO.FacultyRemark;
            vm.MeetingRequestStatusName = reqDTO.MeetingRequestStatusName;
            vm.CreatedBy = reqDTO.CreatedBy;
            vm.CreatedDate = reqDTO.CreatedDate;
            vm.UpdatedBy = reqDTO.UpdatedBy;
            vm.UpdatedDate = reqDTO.UpdatedDate;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MeetingRequestViewModel, MeetingRequestDTO>.CreateMap();
            var dto = Mapper<MeetingRequestViewModel, MeetingRequestDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.MeetingRequestIID = this.MeetingRequestIID;
            dto.RequestedDate = string.IsNullOrEmpty(this.RequestedDateString) ? (DateTime?)null : DateTime.ParseExact(this.RequestedDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ApprovedDate = string.IsNullOrEmpty(this.ApprovedDateString) ? (DateTime?)null : DateTime.ParseExact(this.ApprovedDateString, dateFormat, CultureInfo.InvariantCulture);
            //dto.ApprovedSignupSlotMapID = string.IsNullOrEmpty(this.ApprovedSlotMap) ? (long?)null : long.Parse(this.ApprovedSlotMap);
            dto.ApprovedSignupSlotMapID = this.ApprovedSlotMap != null && !string.IsNullOrEmpty(this.ApprovedSlotMap.Key) ? long.Parse(this.ApprovedSlotMap.Key) : (long?)null;
            dto.MeetingRequestStatusID = string.IsNullOrEmpty(this.MeetingRequestStatus) ? (byte?)null : byte.Parse(this.MeetingRequestStatus);
            dto.RequestedSignupSlotMapID = this.RequestedSignupSlotMapID;
            dto.StudentID = this.StudentID;
            dto.ParentID = this.ParentID;
            dto.ParentLoginID = this.ParentLoginID;
            dto.GuardianEmailID = this.GuardianEmailID;
            dto.FacultyID = this.FacultyID;
            dto.Faculty = this.FacultyID.HasValue ? new KeyValueDTO()
            {
                Key = this.FacultyID.ToString(),
                Value = this.FacultyName,
            } : new KeyValueDTO();
            dto.ApprovedSlotTime = this.ApprovedSlotMap?.Value;
            dto.MeetingRequestStatusName = this.MeetingRequestStatusName;
            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;
            dto.ClassID = this.ClassID;
            dto.SectionID = this.SectionID;
            dto.RequesterRemark = this.RequesterRemark;
            dto.FacultyRemark = this.FacultyRemark;
            dto.CreatedBy = this.CreatedBy;
            dto.CreatedDate = this.CreatedDate;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MeetingRequestDTO>(jsonString);
        }

    }
}