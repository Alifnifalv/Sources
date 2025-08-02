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
using Eduegate.Domain;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentLeaveApplicationViewModel : BaseMasterViewModel
    {
        public long StudentLeaveApplicationIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("LeaveApplicationNo.")]
        public string LeaveAppNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Admission Number")]
        public string AdmissionNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine8 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class")]
        public string ClassName { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine9 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("FromDate")]
        public string FromDateString { get; set; }
        public DateTime? FromDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ToDate")]
        public string ToDateString { get; set; }
        public DateTime? ToDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Reason")]
        [StringLength(500)]
        public string Reason { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DataType(DataType.Duration)]
        //[DisplayName("From Session")]
        public byte? FromSessionID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DataType(DataType.Duration)]
        //[DisplayName("To Session")]
        public byte? ToSessionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.StudentLeaveStatus")]
        [CustomDisplay("ApplicationStatus")]
        public string StudentLeaveStatus { get; set; }
        public byte? LeaveStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public string CreatedDateString { get; set; }
        public new DateTime? CreatedDate { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentLeaveApplicationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentLeaveApplicationViewModel>(jsonString);
        }

        public StudentLeaveApplicationViewModel ToVM(StudentLeaveApplicationDTO dto)
        {
            Mapper<StudentLeaveApplicationDTO, StudentLeaveApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var applicationDto = dto as StudentLeaveApplicationDTO;
            var vm = Mapper<StudentLeaveApplicationDTO, StudentLeaveApplicationViewModel>.Map(applicationDto);

            vm.StudentLeaveApplicationIID = Convert.ToInt64(applicationDto.StudentLeaveApplicationIID);
            vm.StudentID = applicationDto.StudentID;
            vm.StudentName = applicationDto.StudentID == null ? null : applicationDto.StudentID.ToString();
            vm.Reason = applicationDto.Reason;
            vm.LeaveAppNumber = applicationDto.LeaveAppNumber;
            vm.ClassID = applicationDto.ClassID;
            vm.FromSessionID = applicationDto.FromSessionID;
            vm.ToSessionID = applicationDto.ToSessionID;
            vm.FromDateString = applicationDto.FromDate.HasValue ? applicationDto.FromDate.Value.ToString("dd/MM/yyyy") : null;
            vm.ToDateString = applicationDto.ToDate.HasValue ? applicationDto.ToDate.Value.ToString("dd/MM/yyyy") : null;
            vm.StudentLeaveStatus = applicationDto.LeaveStatusID.HasValue ? applicationDto.LeaveStatusID.ToString() : null;
            vm.ClassName = applicationDto.ClassID == null ? null : applicationDto.ClassID.ToString();
            vm.CreatedDateString = applicationDto.CreatedDate.HasValue ? applicationDto.CreatedDate.Value.ToLongDateString() : null;
            vm.CreatedDate = applicationDto.CreatedDate;
            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentLeaveApplicationDTO, StudentLeaveApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as StudentLeaveApplicationDTO;
            var vm = Mapper<StudentLeaveApplicationDTO, StudentLeaveApplicationViewModel>.Map(stDtO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.StudentName = stDtO.StudentID.HasValue ? stDtO.StudentName.ToString() : null;
            vm.FromDateString = stDtO.FromDate.HasValue ? stDtO.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = stDtO.ToDate.HasValue ? stDtO.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.StudentLeaveStatus = stDtO.LeaveStatusID.HasValue ? stDtO.LeaveStatusID.ToString() : null;
            vm.ClassName = stDtO.ClassID.HasValue ? stDtO.ClassName.ToString() : null;
            vm.ClassID = stDtO.ClassID;
            vm.Remarks = stDtO.Remarks;
            vm.FromSessionID = stDtO.FromSessionID;
            vm.ToSessionID = stDtO.ToSessionID;
            vm.FromDate = stDtO.FromDate;
            vm.ToDate = stDtO.ToDate;
            vm.CreatedDateString = stDtO.CreatedDate.HasValue ? stDtO.CreatedDate.Value.ToLongDateString() : null;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentLeaveApplicationViewModel, StudentLeaveApplicationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<StudentLeaveApplicationViewModel, StudentLeaveApplicationDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.StudentID = StudentID;
            dto.ClassID = this.ClassID == null ? null : this.ClassID;
            dto.Reason = this.Reason;
            dto.Remarks = this.Remarks;
            dto.FromDate = this.FromDate.HasValue ? this.FromDate : 
                string.IsNullOrEmpty(this.FromDateString) ? (DateTime?)null : DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ToDate = this.ToDate.HasValue ? this.ToDate :
                string.IsNullOrEmpty(this.ToDateString) ? (DateTime?)null : DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.LeaveStatusID = this.StudentLeaveStatus == null ? this.LeaveStatusID : string.IsNullOrEmpty(this.StudentLeaveStatus) ? (byte?)null : byte.Parse(this.StudentLeaveStatus);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentLeaveApplicationDTO>(jsonString);
        }

    }
}