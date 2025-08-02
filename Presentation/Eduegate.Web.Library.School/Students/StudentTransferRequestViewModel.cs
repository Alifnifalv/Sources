using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentTransferRequestViewModel : BaseMasterViewModel
    {
        public StudentTransferRequestViewModel()
        {
            TransferRequestReason = new KeyValueViewModel();
            IsTransferRequested = false;
            IsTCCollected = false;
            IsChequeIssued = false;
            TransferRequestStatus = new Domain.Setting.SettingBL().GetSettingValue<string>("STUD_TRANSFER_STATUS_REQUESTED", "1");
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //ExpectingRelivingDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
        }
        public long StudentTransferRequestIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TCApplicationNo.")]
        public string TCAppNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "StudentChanges($event, $element, CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class")]
        public string Class { get; set; }

        public int? ClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "texlright")]
        [CustomDisplay("Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ExpectingRelivingDate")]
        public string ExpectingRelivingDateString { get; set; }
        public DateTime? ExpectingRelivingDate { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("ChangeOfSchool")]
        public bool IsSchoolChange { get; set; }


        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("LeavingCountry")]
        public bool IsLeavingCountry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine09 { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Parent-Concern")]
        public string Concern { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Parent-PositiveAspect")]
        public string PositiveAspect { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("SchoolRemarks")]
        public string SchoolRemarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StudentTransferRequestReasons")]
        [CustomDisplay("Reason")]
        public string StudentTransferRequestReasons { get; set; }
        public byte? TransferRequestReasonID { get; set; }

        public KeyValueViewModel TransferRequestReason { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled='CRUDModel.ViewModel.StudentTransferRequestReasons != 7'")]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [CustomDisplay("OtherReason")]
        public string OtherReason { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled = CRUDModel.ViewModel.StudentTransferRequestIID==0")]
        [LookUp("LookUps.StudentTransferRequestStatus")]
        [CustomDisplay("TransferStatus")]
        public string TransferRequestStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsTransferRequested")]
        public bool IsTransferRequested { get; set; }
        public byte? TransferRequestStatusID { get; set; }

        public string CreatedDateString { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsChequeIssued")]
        public bool? IsChequeIssued { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsTCCollected")]
        public bool? IsTCCollected { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentTransferRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentTransferRequestViewModel>(jsonString);
        }

        public StudentTransferRequestViewModel ToVM(StudentTransferRequestDTO dto)
        {
            Mapper<StudentTransferRequestDTO, StudentTransferRequestViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var applicationDto = dto as StudentTransferRequestDTO;
            var vm = Mapper<StudentTransferRequestDTO, StudentTransferRequestViewModel>.Map(applicationDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StudentTransferRequestIID = Convert.ToInt64(applicationDto.StudentTransferRequestIID);
            vm.StudentID = applicationDto.StudentID;
            vm.Student = applicationDto.StudentID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StudentID.ToString(), Value = applicationDto.StudentName } : null;
            vm.OtherReason = applicationDto.OtherReason;
            vm.TCAppNumber = applicationDto.TCAppNumber;
            vm.IsTransferRequested = applicationDto.IsTransferRequested ?? false;
            vm.ExpectingRelivingDateString = applicationDto.ExpectingRelivingDate.HasValue ? applicationDto.ExpectingRelivingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.TransferRequestStatusID = applicationDto.TransferRequestStatusID;
            vm.TransferRequestStatus = applicationDto.TransferRequestStatusID.HasValue ? applicationDto.TransferRequestStatusID.ToString() : null;
            vm.CreatedDateString = applicationDto.CreatedDate.HasValue ? applicationDto.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.CreatedDate = applicationDto.CreatedDate;
            vm.TransferRequestReasonID = applicationDto.TransferRequestReasonID;
            vm.StudentTransferRequestReasons = applicationDto.TransferRequestReasonID.ToString();
            vm.IsSchoolChange = applicationDto.IsSchoolChange ?? false;
            vm.IsLeavingCountry = applicationDto.IsLeavingCountry ?? false;
            vm.SchoolRemarks = applicationDto.SchoolRemarks;
            vm.PositiveAspect = applicationDto.PositiveAspect;
            vm.Concern = applicationDto.Concern;
            vm.Section = applicationDto.Section;
            vm.Class = applicationDto.Class;
            vm.IsChequeIssued = applicationDto.IsChequeIssued ?? false;
            vm.IsTCCollected = applicationDto.IsTCCollected ?? false;
            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentTransferRequestDTO, StudentTransferRequestViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as StudentTransferRequestDTO;
            var vm = Mapper<StudentTransferRequestDTO, StudentTransferRequestViewModel>.Map(stDtO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StudentTransferRequestIID = stDtO.StudentTransferRequestIID;
            vm.Student = stDtO.StudentID.HasValue ? new KeyValueViewModel() { Key = stDtO.StudentID.ToString(), Value = stDtO.StudentName } : null;
            vm.OtherReason = stDtO.OtherReason;
            vm.TCAppNumber = stDtO.TCAppNumber;
            vm.IsTransferRequested = stDtO.IsTransferRequested ?? false;
            vm.TransferRequestStatus = stDtO.TransferRequestStatusID.HasValue ? stDtO.TransferRequestStatusID.ToString() : null;
            vm.TransferRequestStatusID = null;
            vm.ExpectingRelivingDateString = stDtO.ExpectingRelivingDate.HasValue ? stDtO.ExpectingRelivingDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.TransferRequestReasonID = null;
            vm.StudentTransferRequestReasons = stDtO.TransferRequestReasonID.HasValue ? stDtO.TransferRequestReasonID.ToString() : null;
            vm.IsSchoolChange = stDtO.IsSchoolChange ?? false;
            vm.IsLeavingCountry = stDtO.IsLeavingCountry ?? false;
            vm.SchoolRemarks = stDtO.SchoolRemarks;
            vm.PositiveAspect = stDtO.PositiveAspect;
            vm.Concern = stDtO.Concern;
            vm.Class = stDtO.Class;
            vm.Section = stDtO.Section;
            vm.IsChequeIssued = stDtO.IsChequeIssued ?? false;
            vm.IsTCCollected = stDtO.IsTCCollected ?? false;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentTransferRequestViewModel, StudentTransferRequestDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<StudentTransferRequestViewModel, StudentTransferRequestDTO>.Map(this);
            
            dto.StudentID = this.StudentID.HasValue ? this.StudentID : string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.OtherReason = this.OtherReason;
            dto.StudentTransferRequestIID = this.StudentTransferRequestIID;
            dto.IsTransferRequested = this.IsTransferRequested;
            dto.ExpectingRelivingDate = string.IsNullOrEmpty(this.ExpectingRelivingDateString) ? (DateTime?)null : DateTime.ParseExact(this.ExpectingRelivingDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.TransferRequestStatusID = this.TransferRequestStatusID.HasValue ? this.TransferRequestStatusID : string.IsNullOrEmpty(this.TransferRequestStatus) ? (byte?)null : byte.Parse(this.TransferRequestStatus);
            dto.TransferRequestReasonID = string.IsNullOrEmpty(this.StudentTransferRequestReasons) ? (byte?)null : byte.Parse(this.StudentTransferRequestReasons);
            dto.IsLeavingCountry = this.IsLeavingCountry;
            dto.IsSchoolChange = this.IsSchoolChange;
            dto.SchoolRemarks = this.SchoolRemarks != null ? this.SchoolRemarks : null;
            dto.Concern = this.Concern != null ? this.Concern : null;
            dto.PositiveAspect = this.PositiveAspect != null ? this.PositiveAspect : null;
            dto.IsChequeIssued = this.IsChequeIssued;
            dto.IsTCCollected = this.IsTCCollected;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentTransferRequestDTO>(jsonString);
        }

    }
}