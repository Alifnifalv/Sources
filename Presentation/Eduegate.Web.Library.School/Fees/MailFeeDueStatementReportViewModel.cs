using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeDueStatementReportMail", "CRUDModel.ViewModel")]
    [DisplayName("FeeDueStatementReportMail")]

    public class MailFeeDueStatementReportViewModel : BaseMasterViewModel
    {
        public MailFeeDueStatementReportViewModel()
        {
            Select_Deselect = true;
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            FeeDueDetails = new List<MailFeeDueStatementReportDetailViewModel> { new MailFeeDueStatementReportDetailViewModel() };
        }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("As on date")]
        public string AsOnDate { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false)]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        public long? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "ClassSectionChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox,attribs: "ng-change='CheckBoxClicks($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Select/Deselect all")]
        public bool? Select_Deselect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='SendReportToALL($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Mail all")]
        public string SendToAll { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='ClearALL($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Clear all")]
        public string ClearAll { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "FeeDueDetails")]
        [CustomDisplay("Fee Due Details")]
        public List<MailFeeDueStatementReportDetailViewModel> FeeDueDetails { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MailFeeDueStatementReportDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MailFeeDueStatementReportViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MailFeeDueStatementReportDTO, MailFeeDueStatementReportViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeDueCancellationDetailDTO, MailFeeDueStatementReportDetailViewModel>.CreateMap();
            var vm = Mapper<MailFeeDueStatementReportDTO, MailFeeDueStatementReportViewModel>.Map(dto as MailFeeDueStatementReportDTO);
            var DTO = dto as MailFeeDueStatementReportDTO;
            vm.StudentClass = DTO.ClassID.HasValue ? new KeyValueViewModel() { Key = DTO.ClassID.ToString(), Value = DTO.Class } : new KeyValueViewModel();
            vm.FeeDueDetails = new List<MailFeeDueStatementReportDetailViewModel>();
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MailFeeDueStatementReportViewModel, MailFeeDueStatementReportDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<MailFeeDueStatementReportDetailViewModel, FeeDueCancellationDetailDTO>.CreateMap();
            var dto = Mapper<MailFeeDueStatementReportViewModel, MailFeeDueStatementReportDTO>.Map(this);

            dto.ClassID = this.StudentClass == null || string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MailFeeDueStatementReportDTO>(jsonString);
        }
    }
}

