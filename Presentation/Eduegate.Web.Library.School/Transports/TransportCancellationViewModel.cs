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
    public class TransportCancellationViewModel : BaseMasterViewModel
    {
        public TransportCancellationViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
        }
        public long RequestIID { get; set; }
        public long? StudentRouteStopMapIID { get; set; }  

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Admision Number")]
        public string AdmissionNumber { get; set; }
        
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class & Section")]
        public string ClassSection { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Is One Way")]
        public string IsOneWay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Pickup Stop")]
        public string PickupStopMapName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Drop Stop")]
        public string DropStopMapName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Pickup Route Code")]
        public string PickupRouteCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Drop Route Code")]
        public string DropStopRouteCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker, "disabled")]
        [CustomDisplay("Applied on")]
        public string AppliedDateString { get; set; }
        public System.DateTime? AppliedDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Expected Cancel Date")]
        public string ExpectedCancelDateString { get; set; }
        public DateTime? ExpectedCancelDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea, "disabled")]
        [CustomDisplay("Reason")]
        public string Reason { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("School Remarks")]
        public string RemarksBySchool { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("Transport Fee Dues")]
        public string FeeDues { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.TransportCancellationStatuses")]
        public string Status { get; set; } 
        public int? StatusID { get; set; } 

        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public int? PreviousStatusID { get; set; }

        public bool? CheckBoxForSiblings { get; set; }
        public bool? CheckBoxForDeclaration { get; set; }

        
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TransportCancellationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransportCancellationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TransportCancellationDTO, TransportCancellationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var sDto = dto as TransportCancellationDTO;
            var vm = Mapper<TransportCancellationDTO, TransportCancellationViewModel>.Map(dto as TransportCancellationDTO);

            vm.RequestIID = sDto.RequestIID;
            vm.StudentRouteStopMapIID = sDto.StudentRouteStopMapIID;
            vm.Reason = sDto.Reason;
            vm.RemarksBySchool = sDto.RemarksBySchool;
            vm.Status = sDto.StatusID.HasValue ? sDto.StatusID.ToString() : null;
            vm.PreviousStatusID = sDto.StatusID;
            vm.StudentID = sDto.StudentID;
            vm.IsOneWay = sDto.IsOneWay == true ? "Yes" : "No";

            vm.AppliedDateString = sDto.AppliedDate.HasValue ? sDto.AppliedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ExpectedCancelDateString = sDto.ExpectedCancelDate.HasValue ? sDto.ExpectedCancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;

            vm.CreatedBy = sDto.CreatedBy;
            vm.CreatedDate = sDto.CreatedDate;
            vm.UpdatedBy = sDto.UpdatedBy;
            vm.UpdatedDate = sDto.UpdatedDate;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TransportCancellationViewModel, TransportCancellationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<TransportCancellationViewModel, TransportCancellationDTO>.Map(this);
          
            dto.RequestIID = this.RequestIID;
            dto.StudentRouteStopMapIID = this.StudentRouteStopMapIID;
            dto.Reason = this.Reason;
            dto.CheckBoxForSiblings = this.CheckBoxForSiblings;
            dto.RemarksBySchool = this.RemarksBySchool;
            dto.StatusID = string.IsNullOrEmpty(this.Status) ? (int?)null : int.Parse(this.Status);
            dto.PreviousStatusID = this.PreviousStatusID;
            dto.StudentID = this.StudentID;

            dto.AppliedDate = string.IsNullOrEmpty(this.AppliedDateString) ? (DateTime?)null : DateTime.ParseExact(AppliedDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ExpectedCancelDate = string.IsNullOrEmpty(this.ExpectedCancelDateString) ? (DateTime?)null : DateTime.ParseExact(ExpectedCancelDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.CreatedBy = this.CreatedBy;
            dto.CreatedDate = this.CreatedDate;
            dto.UpdatedBy = this.UpdatedBy;
            dto.UpdatedDate = this.UpdatedDate;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransportCancellationDTO>(jsonString);
        }

    }
}