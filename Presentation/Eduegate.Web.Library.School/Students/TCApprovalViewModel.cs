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
    public class TCApprovalViewModel : BaseMasterViewModel
    {
        public TCApprovalViewModel()
        {
            //Student = new KeyValueViewModel();
            //IsTransferRequested = true;
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
        [ControlType(Framework.Enums.ControlTypes.Label)]
        //[Select2("Students", "Numeric", false, optionalAttribute1: "ng-disabled")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public string Student { get; set; }
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

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=ViewTransferCertificate(CRUDModel.ViewModel)")]
        [CustomDisplay("View TC")]
        public string ViewButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=UploadTC(CRUDModel.ViewModel)")]
        [CustomDisplay("Approved and Upload TC")]
        public string UploadButton { get; set; }

        public string TCContentFileID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentTransferRequestDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TCApprovalViewModel>(jsonString);
        }

        public TCApprovalViewModel ToVM(StudentTransferRequestDTO dto)
        {
            Mapper<StudentTransferRequestDTO, TCApprovalViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var applicationDto = dto as StudentTransferRequestDTO;
            var vm = Mapper<StudentTransferRequestDTO, TCApprovalViewModel>.Map(applicationDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.StudentTransferRequestIID = Convert.ToInt64(applicationDto.StudentTransferRequestIID);
            vm.StudentID = applicationDto.StudentID;
            //vm.Student = applicationDto.StudentID.HasValue ? new KeyValueViewModel() { Key = applicationDto.StudentID.ToString(), Value = applicationDto.StudentName } : null;
            vm.Student = applicationDto.StudentName;
            vm.Section = applicationDto.Section;
            vm.Class = applicationDto.Class;
            vm.TCContentFileID = applicationDto.ContentFileIID;
            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentTransferRequestDTO, TCApprovalViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var stDtO = dto as StudentTransferRequestDTO;
            var vm = Mapper<StudentTransferRequestDTO, TCApprovalViewModel>.Map(stDtO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.StudentTransferRequestIID = stDtO.StudentTransferRequestIID;
            //vm.Student = stDtO.StudentID.HasValue ? new KeyValueViewModel() { Key = stDtO.StudentID.ToString(), Value = stDtO.StudentName } : null;
            vm.Student = stDtO.StudentName;
            vm.Class = stDtO.Class;
            vm.Section = stDtO.Section;
            vm.TCContentFileID = stDtO.ContentFileIID;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TCApprovalViewModel, StudentTransferRequestDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<TCApprovalViewModel, StudentTransferRequestDTO>.Map(this);
            //dto.StudentID = this.StudentID.HasValue ? this.StudentID : string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.StudentTransferRequestIID = this.StudentTransferRequestIID;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentTransferRequestDTO>(jsonString);
        }
    }
}