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
using System;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentConcession", "CRUDModel.ViewModel")]
    [DisplayName("StudentConcession")]

    public class StudentConcessionViewModel : BaseMasterViewModel
    {
        public StudentConcessionViewModel()
        {
            
            Staff = new KeyValueViewModel();
            Parent = new KeyValueViewModel();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            ConcessionDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            StudentConcessionDetail = new List<StudentConcessionDetailViewModel> { new StudentConcessionDetailViewModel() };
           
        }
        public long StudentFeeConcessionIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public string AcademicYear { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Concession Date")]
        public string ConcessionDateString { get; set; }
        public System.DateTime? ConcessionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }
      

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("EmployeeWB", "Numeric", false, "StaffChanges($event, $element, CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=EmployeeWB", "LookUps.EmployeeWB")]
        [CustomDisplay("Staff")]
        public KeyValueViewModel Staff { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Parent")]
        [Select2("Parent", "Numeric", false, "ParentChanges($event, $element, CRUDModel.ViewModel)")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Parent", "LookUps.Parent")]
        [LookUp("LookUps.Parent")]
        public KeyValueViewModel Parent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [LookUp("LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Concession Approval Type")]
        [LookUp("LookUps.ConcessionApprovalType")]
        public string ConcessionApprovalType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GetFeeDueDetails(CRUDModel.ViewModel)")]
        [CustomDisplay("Fill Fee Details")]
        public string PostButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "StudentConcessionDetail")]
        [CustomDisplay("Student Concession Details")]
        public List<StudentConcessionDetailViewModel> StudentConcessionDetail { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentConcessionDetail", "StudentConcessionDetail")]
        //[CustomDisplay("Student Fee Concession Details")]
        //public StudentConcessionFeeDetailViewModel StudentConcessionFeeDetail { get; set; }

        public long StudentGroupFeeMasters { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentFeeConcessionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentConcessionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            Mapper<StudentFeeConcessionDTO, StudentConcessionViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<StudentFeeConcessionDetailDTO, StudentConcessionDetailViewModel>.CreateMap();
            var vm = Mapper<StudentFeeConcessionDTO, StudentConcessionViewModel>.Map(dto as StudentFeeConcessionDTO);
            var DTO = dto as StudentFeeConcessionDTO;
            vm.StudentFeeConcessionIID = DTO.StudentFeeConcessionIID;
            vm.AcademicYear = DTO.AcademicYearID.HasValue ? DTO.AcademicYearID.Value.ToString() : null;
            vm.ConcessionDateString = (!string.IsNullOrEmpty(ConcessionDateString) ? ConcessionDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture));
            vm.ConcessionApprovalType = DTO.ConcessionApprovalTypeID.HasValue ? DTO.ConcessionApprovalTypeID.Value.ToString() : null;
            vm.Student = DTO.StudentID.HasValue ? new KeyValueViewModel()
            {
                Key = DTO.StudentID.ToString(),
                Value = DTO.StudentName
            } : new KeyValueViewModel();


            vm.StudentConcessionDetail = new List<StudentConcessionDetailViewModel>();
            foreach (var concessionDetail in DTO.StudentFeeConcessionDetail)
            {
                vm.StudentConcessionDetail.Add(new StudentConcessionDetailViewModel()
                {
                    StudentFeeConcessionID = concessionDetail.StudentFeeConcessionID,
                    FeeDueFeeTypeMapID = concessionDetail.FeeDueFeeTypeMapsID,
                    CreditNoteFeeTypeMapID = concessionDetail.CreditNoteFeeTypeMapID,
                    CreditNoteID = concessionDetail.CreditNoteID,
                    FeeInvoiceID = concessionDetail.StudentFeeDueID,
                    FeeMaster = KeyValueViewModel.ToViewModel(concessionDetail.FeeMaster),
                    FeePeriod = KeyValueViewModel.ToViewModel(concessionDetail.FeePeriod),
                    InvoiceNo = KeyValueViewModel.ToViewModel(concessionDetail.FeeInvoice),                  
                    ConcessionPercentage = concessionDetail.PercentageAmount,
                    ConcessionAmount = concessionDetail.ConcessionAmount,
                    Amount = concessionDetail.DueAmount,
                    NetToPay = concessionDetail.NetAmount
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentConcessionViewModel, StudentFeeConcessionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<StudentConcessionDetailViewModel, StudentFeeConcessionDetailDTO>.CreateMap();
            var dto = Mapper<StudentConcessionViewModel, StudentFeeConcessionDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.ConcessionDate = string.IsNullOrEmpty(this.ConcessionDateString) ? (DateTime?)null : DateTime.ParseExact(this.ConcessionDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.StudentFeeConcessionIID = this.StudentFeeConcessionIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
         
            dto.StaffID = this.Staff == null || string.IsNullOrEmpty(this.Staff.Key) ? (int?)null : int.Parse(this.Staff.Key);
            dto.ParentID = this.Parent == null || string.IsNullOrEmpty(this.Parent.Key) ? (int?)null : int.Parse(this.Parent.Key);
            dto.ConcessionApprovalTypeID = string.IsNullOrEmpty(this.ConcessionApprovalType) ? (short?)null : short.Parse(this.ConcessionApprovalType);
            dto.StudentID = this.Student == null || string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);

            dto.StudentFeeConcessionDetail = new List<StudentFeeConcessionDetailDTO>();
            foreach (var concessionDetail in this.StudentConcessionDetail)
            {
                if (concessionDetail.ConcessionAmount.HasValue)
                {
                    dto.StudentFeeConcessionDetail.Add(new StudentFeeConcessionDetailDTO()
                    {
                        StudentFeeConcessionID = concessionDetail.StudentFeeConcessionID,
                        FeePeriodID = concessionDetail.FeePeriod == null || string.IsNullOrEmpty(concessionDetail.FeePeriod.Key) ? (int?)null : int.Parse(concessionDetail.FeePeriod.Key),
                        FeeMasterID = concessionDetail.FeeMaster == null || string.IsNullOrEmpty(concessionDetail.FeeMaster.Key) ? (int?)null : int.Parse(concessionDetail.FeeMaster.Key),
                        StudentFeeDueID = concessionDetail.InvoiceNo == null || string.IsNullOrEmpty(concessionDetail.InvoiceNo.Key) ? (long?)null : long.Parse(concessionDetail.InvoiceNo.Key),
                        CreditNoteID = concessionDetail.CreditNoteID,
                        CreditNoteFeeTypeMapID = concessionDetail.CreditNoteFeeTypeMapID,
                        FeeDueFeeTypeMapsID = concessionDetail.FeeDueFeeTypeMapID,
                        PercentageAmount = concessionDetail.ConcessionPercentage,
                        ConcessionAmount = concessionDetail.ConcessionAmount,
                        DueAmount = concessionDetail.Amount,
                        NetAmount = concessionDetail.NetToPay
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentFeeConcessionDTO>(jsonString);
        }
    }
}

