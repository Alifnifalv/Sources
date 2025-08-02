using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Eduegate.Web.Library.School.Students;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Domain.Entity.School.Models;
using System.Net;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Refund", "CRUDModel.ViewModel")]
    [DisplayName("Refund")]
    public class RefundViewModel : BaseMasterViewModel
    {
        public RefundViewModel()
        {
            RefundDate = DateTime.Now;
            AcademicYear = new KeyValueViewModel();
            FeeTypes = new List<RefundFeeTypeViewModel>() { new RefundFeeTypeViewModel() };            
            FeePaymentMap = new List<RefundPaymentModeMapViewModel>() { new RefundPaymentModeMapViewModel() };           
        }
        public long FinalSettlementIID { get; set; }

        public long RefundIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("TransactionDate")]
        public string CollectionDateString { get; set; }
        public System.DateTime? CollectionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("RefundDate")]
        public string RefundDateString { get; set; }
        public System.DateTime? RefundDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DataPicker)]
        //[DisplayName("Select Student")]
        //[DataPicker("StudenAdvancedSearch")]
        //public string ReferenceStudent { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false, "StudentChanges(CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [CustomDisplay("Student")]
        //[LookUp("LookUps.Student")]
        public KeyValueViewModel Student { get; set; }

        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Academic Year")]
        [Select2("AcademicYear", "Numeric", false, "AcademicYearChanges(CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AcademicYearWithLastYear")]
        public KeyValueViewModel AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

     
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AdmissionNumber")]
        public string AdmissionNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Class")]
        public string Class { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; }

        public byte? SchoolID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine16 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalFeeAmount(CRUDModel.ViewModel.FeeTypes,CRUDModel.ViewModel.FeeFines) | number")]
        [CustomDisplay("TotalAmounttobePaid")]
        public decimal? TotalAmount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalCollectedAmount(CRUDModel.ViewModel.FeePaymentMap) | number")]
        [CustomDisplay("TotalCollectedAmount")]
        public decimal? CollectedAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeePayment")]
        public List<RefundPaymentModeMapViewModel> FeePaymentMap { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<RefundFeeTypeViewModel> FeeTypes { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Fines")]
        //public List<FeeCollectionFineViewModel> FeeFines { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RefundDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RefundViewModel>(jsonString);
        }

        public RefundViewModel FromStudentDataFromStudentVM(StudentViewModel studentVM)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = new RefundViewModel()
            {
                ClassID = studentVM.ClassID,
                StudentID = studentVM.StudentIID,
                AdmissionNumber = studentVM.AdmissionNumber,
                SectionID = studentVM.SectionID == null ? (int?)null : studentVM.SectionID,
                Section = studentVM.SectionName,
                Class = studentVM.ClassName,
                Student = new KeyValueViewModel() { Key = studentVM.StudentIID.ToString(), Value = studentVM.FirstName + " " + studentVM.MiddleName + " " + studentVM.LastName },
                RefundDateString = (!string.IsNullOrEmpty(RefundDateString) ? RefundDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture)),
                //CollectionDateString = (!string.IsNullOrEmpty(CollectionDateString) ? CollectionDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture))
            };

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<RefundDTO, RefundViewModel>.CreateMap();
            Mapper<FeeCollectionFeeTypeDTO, RefundFeeTypeViewModel >.CreateMap();
            Mapper<FeeCollectionMonthlySplitDTO, RefundSplitViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as RefundDTO;
            var vm = Mapper<RefundDTO, RefundViewModel>.Map(dto as RefundDTO);

            //FeePaymentMap.

            vm.RefundIID = feeDto.RefundIID;
            vm.Student = new KeyValueViewModel() { Key = feeDto.Student.Key.ToString(), Value = feeDto.Student.Value };
            vm.Section = feeDto.SectionName;
            vm.Class = feeDto.ClassName;
            vm.AcademicYear = new KeyValueViewModel() { Key = feeDto.AcademicYear.Key, Value = feeDto.AcademicYear.Value };
            vm.AcademicYearID = feeDto.AcademicYear != null && feeDto.AcademicYear.Key != null ? int.Parse(feeDto.AcademicYear.Key) : (int?)null;
            vm.RefundDateString = (vm.RefundDate.HasValue ? vm.RefundDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.CollectionDateString = (vm.CollectionDate.HasValue ? vm.CollectionDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.AdmissionNumber = feeDto.AdmissionNo;
            vm.CollectedAmount = feeDto.CollectedAmount;
            vm.TotalAmount = 0;
            vm.FeePaymentMap = new List<RefundPaymentModeMapViewModel>();
            vm.FeeTypes = new List<RefundFeeTypeViewModel>();

            foreach (var payment in feeDto.FeeCollectionPaymentModeMapDTO)
            {
                vm.FeePaymentMap.Add(new RefundPaymentModeMapViewModel()
                {
                    PaymentMode = new KeyValueViewModel() { Key = Convert.ToString(payment.PaymentMode.Key), Value = payment.PaymentMode.Value },
                    TDate = (payment.CreatedDate.HasValue ? payment.CreatedDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                    ReferenceNo = payment.ReferenceNo,
                    Amount = payment.Amount,
                });
            }

            foreach(var ft in feeDto.FeeTypes)
            {
                var monthlySplit = new List<RefundSplitViewModel>();

                foreach (var monthly in ft.MontlySplitMaps)
                {
                    monthlySplit.Add(new RefundSplitViewModel()
                    {
                        CreditNote = monthly.CreditNoteAmount,
                        Amount = monthly.DueAmount,
                        Refund = monthly.RefundAmount,
                        MonthName = monthly.MonthName
                    });
                }

                vm.FeeTypes.Add(new RefundFeeTypeViewModel()
                {
                    Amount = ft.Amount,
                    FeeDueAmount = ft.DueAmount,
                    Refund = ft.RefundAmount,
                    CreditNote = ft.CreditNoteAmount,
                    InvoiceDateString = (ft.InvoiceDate.HasValue ? ft.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                    InvoiceNo = ft.InvoiceNo,
                    FeePeriodID = ft.FeePeriodID,
                    InvoiceDate = ft.InvoiceDate,
                    StudentFeeDueID = ft.StudentFeeDueID,
                    FeeCycleID = ft.FeeCycleID,
                    FeeDueFeeTypeMapsID = ft.FeeDueFeeTypeMapsID,
                    FeeMaster = ft.FeeMaster == null ? null : ft.FeeMaster,
                    FeePeriod = ft.FeePeriodID.HasValue ? ft.FeePeriod : null,
                    MonthlySplits = monthlySplit,
                });
            }
            
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RefundViewModel, RefundDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<RefundFeeTypeViewModel, FeeCollectionFeeTypeDTO>.CreateMap();
            Mapper<RefundSplitViewModel, FeeCollectionMonthlySplitDTO>.CreateMap();
            var dto = Mapper<RefundViewModel, RefundDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //dto.FeeReceiptNo = this.FeeReceiptNo;
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.RefundIID = this.RefundIID;
            dto.SectionID = this.SectionID;
            dto.ClassID = this.ClassID;
            dto.SchoolID = this.SchoolID;
            dto.AcadamicYearID = this.AcademicYearID;
            dto.RefundDate = string.IsNullOrEmpty(this.RefundDateString) ? (DateTime?)null : DateTime.ParseExact(this.RefundDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.CollectionDate = string.IsNullOrEmpty(this.CollectionDateString) ? (DateTime?)null : DateTime.ParseExact(this.CollectionDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
            foreach (var feePayment in this.FeePaymentMap)
            {
                if (feePayment.Amount.HasValue)
                {
                    dto.FeeCollectionPaymentModeMapDTO.Add(new FeeCollectionPaymentModeMapDTO()
                    {
                        Amount = feePayment.Amount,
                        ReferenceNo = feePayment.ReferenceNo,
                        PaymentModeID = string.IsNullOrEmpty(feePayment.PaymentMode.Key) ? (int?)null : int.Parse(feePayment.PaymentMode.Key),
                        TDate = (feePayment.TDate == null || feePayment.TDate == string.Empty) ? (DateTime?)null : DateTime.ParseExact(feePayment.TDate, dateFormat, CultureInfo.InvariantCulture),
                    });
                }
            }

            dto.FeeTypes = new List<FeeCollectionFeeTypeDTO>();
            foreach (var feeType in this.FeeTypes)
            {
                if (feeType.IsRowSelected == true)
                {
                    var montlySplitDto = new List<FeeCollectionMonthlySplitDTO>();
                    foreach (var feeMonthlySplit in feeType.MonthlySplits)
                    {
                        if (feeMonthlySplit.IsRowSelected == true)
                        {
                            var entity = new FeeCollectionMonthlySplitDTO()
                            {
                                FeeCollectionMonthlySplitIID = feeMonthlySplit.MapIID,
                                MonthID = feeMonthlySplit.MonthID,
                                Amount = feeMonthlySplit.Amount.HasValue ? feeMonthlySplit.Amount : (decimal?)null,
                                Year = feeMonthlySplit.Year,
                                FeeDueMonthlySplitID = feeMonthlySplit.FeeDueMonthlySplitID,
                               
                                FeeCollectionFeeTypeMapId = feeType.FeeCollectionFeeTypeMapsIID,
                                CreditNoteAmount = feeMonthlySplit.CreditNote.HasValue ? feeMonthlySplit.CreditNote : (decimal?)null,
                                Balance = feeMonthlySplit.Balance.HasValue ? feeMonthlySplit.Balance : (decimal?)null,
                                NowPaying = feeMonthlySplit.Amount,
                                RefundAmount = feeMonthlySplit.Refund,
                                PrvCollect = feeMonthlySplit.PrvCollect                              

                            };

                            montlySplitDto.Add(entity);
                        }
                    }
                    if (feeType.FeeMasterID.HasValue)
                    {

                        dto.FeeTypes.Add(new FeeCollectionFeeTypeDTO()
                        {
                            FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                            FeeMasterID = feeType.FeeMasterID,
                            FeePeriodID = feeType.FeePeriodID.HasValue ? feeType.FeePeriodID.Value : (int?)null,
                            Amount = feeType.FeeDueAmount,
                            FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                            CreditNoteAmount = feeType.CreditNote.HasValue ? feeType.CreditNote : (decimal?)null,
                            StudentFeeDueID = feeType.StudentFeeDueID,                           
                            RefundAmount = feeType.Refund,
                            Balance = feeType.Amount,
                            CollectedAmount = feeType.CollectedAmount,
                            DueAmount = feeType.FeeDueAmount,
                            MontlySplitMaps = montlySplitDto

                        });

                    }
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RefundDTO>(jsonString);
        }

    }
}
