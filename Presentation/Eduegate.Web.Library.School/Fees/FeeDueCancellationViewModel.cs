using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Students;
using System.Collections.Generic;
using System.ComponentModel;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeDueCancellation", "CRUDModel.ViewModel")]
    [DisplayName("FeeDueCancellation")]

    public class FeeDueCancellationViewModel : BaseMasterViewModel
    {
        public FeeDueCancellationViewModel()
        {
            Student = new KeyValueViewModel();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            CancelationDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            FeeDueDetails = new List<FeeDueCancellationDetailViewModel> { new FeeDueCancellationDetailViewModel() };
            
        }
        public long FeeDueCancellationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public string AcademicYear { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Cancelation Date")]
        public string CancelationDateString { get; set; }
        public System.DateTime? CancelationDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
       

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GetFeeDueDetails(CRUDModel.ViewModel)")]
        [CustomDisplay("Fill Fee Details")]
        public string PostButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "FeeDueCancellationDetail")]
        [CustomDisplay("Fee Due Details")]
        public List<FeeDueCancellationDetailViewModel> FeeDueDetails { get; set; }

        public long StudentGroupFeeMasters { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeDueCancellationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDueCancellationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeDueCancellationDTO, FeeDueCancellationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeDueCancellationDetailDTO, FeeDueCancellationDetailViewModel>.CreateMap();
            var vm = Mapper<FeeDueCancellationDTO, FeeDueCancellationViewModel>.Map(dto as FeeDueCancellationDTO);
            var DTO = dto as FeeDueCancellationDTO;
            vm.FeeDueCancellationIID = DTO.FeeDueCancellationIID;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.CancelationDateString = (!string.IsNullOrEmpty(CancelationDateString) ? CancelationDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture));
            vm.AcademicYear = DTO.AcademicYearID.HasValue ? DTO.AcademicYearID.Value.ToString() : null;
            vm.Student = DTO.StudentID.HasValue ? new KeyValueViewModel() { Key = DTO.StudentID.ToString(), Value = DTO.StudentName } : new KeyValueViewModel();
            vm.FeeDueDetails = new List<FeeDueCancellationDetailViewModel>();
            foreach (var dueDetails in DTO.FeeDueCancellation)
            {
                vm.FeeDueDetails.Add(new FeeDueCancellationDetailViewModel()
                {
                    FeeDueCancellationID = dueDetails.FeeDueCancellationID,
                    FeeDueFeeTypeMapID = dueDetails.FeeDueFeeTypeMapsID,   
                    CreditNoteFeeTypeMapID=dueDetails.CreditNoteFeeTypeMapID,
                    CreditNoteID=dueDetails.CreditNoteID,
                    FeeInvoiceID = dueDetails.StudentFeeDueID,
                    FeeMaster = KeyValueViewModel.ToViewModel(dueDetails.FeeMaster),
                    FeePeriod = KeyValueViewModel.ToViewModel(dueDetails.FeePeriod),
                    InvoiceNo = KeyValueViewModel.ToViewModel(dueDetails.FeeInvoice),
                    IsCancel = dueDetails.IsCancel,
                    Amount = dueDetails.DueAmount,
                    Remarks = dueDetails.Remarks,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeDueCancellationViewModel, FeeDueCancellationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeDueCancellationDetailViewModel, FeeDueCancellationDetailDTO>.CreateMap();
            var dto = Mapper<FeeDueCancellationViewModel, FeeDueCancellationDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.CancelationDate = string.IsNullOrEmpty(this.CancelationDateString) ? (DateTime?)null : DateTime.ParseExact(this.CancelationDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.FeeDueCancellationIID = this.FeeDueCancellationIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.StudentID = this.Student == null || string.IsNullOrEmpty(this.Student.Key) ? (int?)null : int.Parse(this.Student.Key);
            dto.FeeDueCancellation = new List<FeeDueCancellationDetailDTO>();
            foreach (var feeDue in this.FeeDueDetails)
            {
                dto.FeeDueCancellation.Add(new FeeDueCancellationDetailDTO()
                {
                    FeeDueCancellationID = feeDue.FeeDueCancellationID,
                    StudentID = this.Student == null || string.IsNullOrEmpty(this.Student.Key) ? (int?)null : int.Parse(this.Student.Key),
                    FeePeriodId = feeDue.FeePeriod == null || string.IsNullOrEmpty(feeDue.FeePeriod.Key) ? (int?)null : int.Parse(feeDue.FeePeriod.Key),
                    AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear),
                    FeeMasterID = feeDue.FeeMaster == null || string.IsNullOrEmpty(feeDue.FeeMaster.Key) ? (int?)null : int.Parse(feeDue.FeeMaster.Key),
                    InvoiceNo = feeDue.InvoiceNo == null || string.IsNullOrEmpty(feeDue.InvoiceNo.Value) ? null : (feeDue.InvoiceNo.Value),
                    IsCancel = feeDue.IsCancel,
                    Remarks = feeDue.Remarks,
                    FeeDueFeeTypeMapsID = feeDue.FeeDueFeeTypeMapID,
                });

            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDueCancellationDTO>(jsonString);
        }
    }
}

