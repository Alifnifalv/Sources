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
using Eduegate.Web.Library.School.Students;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FinalSettlement", "CRUDModel.ViewModel")]
    [DisplayName("Final Settlement")]
    public class FinalSettlementViewModel : BaseMasterViewModel
    {
        public FinalSettlementViewModel()
        {
            SettlementDate = DateTime.Now;
            FeeTypes = new List<FinalSettlementFeeTypeViewModel>() { new FinalSettlementFeeTypeViewModel() };
            FeePaymentMap = new List<FinalSettlementPaymentModeMapViewModel>() { new FinalSettlementPaymentModeMapViewModel() };
        }

        public long FinalSettlementIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("TransactionDate")]
        public string CollectionDateString { get; set; }
        public System.DateTime? CollectionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("SettlementDate")]
        public string SettlementDateString { get; set; }
        public System.DateTime? SettlementDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine12 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("TCRequestedStudent", "String", false, "StudentChanges(CRUDModel.ViewModel)")]
        [CustomDisplay("Student")]
        [LookUp("LookUps.TCRequestedStudent")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Academic Year")]
        public string Academic { get; set; }
        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AdmissionNo.")]
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

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeePayment")]
        public List<FinalSettlementPaymentModeMapViewModel> FeePaymentMap { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<FinalSettlementFeeTypeViewModel> FeeTypes { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Fines")]
        //public List<FeeCollectionFineViewModel> FeeFines { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FinalSettlementDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FinalSettlementViewModel>(jsonString);
        }

        public FinalSettlementViewModel FromStudentDataFromStudentVM(StudentViewModel studentVM)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = new FinalSettlementViewModel()
            {
                ClassID = studentVM.ClassID,
                StudentID = studentVM.StudentIID,
                AdmissionNumber = studentVM.AdmissionNumber,
                SectionID = studentVM.SectionID == null ? (int?)null : studentVM.SectionID,
                Section = studentVM.SectionName,
                Class = studentVM.ClassName,
                Student = new KeyValueViewModel() { Key = studentVM.StudentIID.ToString(), Value = studentVM.FirstName + " " + studentVM.MiddleName + " " + studentVM.LastName },
                SettlementDateString = (!string.IsNullOrEmpty(SettlementDateString) ? SettlementDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture)),
                //CollectionDateString = (!string.IsNullOrEmpty(CollectionDateString) ? CollectionDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture))
            };

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            Mapper<FinalSettlementDTO, FinalSettlementViewModel>.CreateMap();
            Mapper<FeeCollectionFeeTypeDTO, FinalSettlementFeeTypeViewModel>.CreateMap();
            Mapper<FeeCollectionMonthlySplitDTO, FeeDueMonthlyFinalViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as FinalSettlementDTO;
            var vm = Mapper<FinalSettlementDTO, FinalSettlementViewModel>.Map(dto as FinalSettlementDTO);

            vm.FinalSettlementIID = feeDto.FinalSettlementIID;
            vm.Student = new KeyValueViewModel() { Key = vm.StudentID.ToString(), Value = feeDto.StudentName };
            vm.Section = feeDto.SectionName;
            vm.Class = feeDto.ClassName;
            vm.Academic = feeDto.AcademicYear != null && feeDto.AcademicYear.Value != null ? feeDto.AcademicYear.Value : null;
            vm.AcademicYearID = feeDto.AcademicYear != null && feeDto.AcademicYear.Key != null ? int.Parse(feeDto.AcademicYear.Key) : (int?)null;
            vm.SettlementDateString = (vm.SettlementDate.HasValue ? vm.SettlementDate.Value : DateTime.Now).ToString(dateFormat);
            vm.CollectionDateString = (vm.CollectionDate.HasValue ? vm.CollectionDate.Value : DateTime.Now).ToString(dateFormat);
            vm.AdmissionNumber = feeDto.AdmissionNo;
            vm.CollectedAmount = feeDto.CollectedAmount;
            vm.TotalAmount = 0;
            vm.Remarks = feeDto.Remarks;

            vm.FeePaymentMap = new List<FinalSettlementPaymentModeMapViewModel>();

            foreach (var fpm in feeDto.FeeCollectionPaymentModeMapDTO)
            {
                if (fpm.PaymentMode.Key != null)
                {
                    vm.FeePaymentMap.Add(new FinalSettlementPaymentModeMapViewModel()
                    {
                        PayAmount = fpm.Amount,
                        ReferenceNo = fpm.ReferenceNo,
                        PaymentMode = new KeyValueViewModel() { Key = fpm.PaymentMode?.Key, Value = fpm.PaymentMode?.Value },
                        TDate = (fpm.CreatedDate.HasValue ? fpm.CreatedDate.Value : DateTime.Now).ToString(dateFormat),
                    });
                }
            }

            vm.FeeTypes = new List<FinalSettlementFeeTypeViewModel>();
            foreach (var feeType in feeDto.FeeTypes)
            {
                var feeDueMonthly = new List<FeeDueMonthlyFinalViewModel>();

                foreach (var mf in feeType.MontlySplitMaps)
                {
                    feeDueMonthly.Add(new FeeDueMonthlyFinalViewModel()
                    {
                        MonthID = mf.MonthID != 0 ? int.Parse(Convert.ToString(mf.MonthID)) : 0,
                        Amount = mf.Amount.HasValue ? mf.Amount : (decimal?)null,
                        MonthName = mf.MonthName,
                        //TaxAmount = mf.TaxAmount.HasValue ? mf.TaxAmount : (decimal?)null,
                        //TaxPercentage = mf.TaxPercentage.HasValue ? mf.TaxPercentage : (decimal?)null
                    });
                }

                vm.FeeTypes.Add(new FinalSettlementFeeTypeViewModel()
                {
                    FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                    FeeMasterID = feeType.FeeMasterID,
                    FeePeriodID = feeType.FeePeriodID.HasValue ? feeType.FeePeriodID.Value : (int?)null,
                    //Amount = feeType.Amount,
                    InvoiceNo = feeType.InvoiceNo,
                    FeeMaster = feeType.FeeMaster,
                    FeePeriod = feeType.FeePeriod,
                    InvoiceDateString = (feeType.InvoiceDate.HasValue ? feeType.InvoiceDate.Value : DateTime.Now).ToString(dateFormat),
                    FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                    //CreditNote = feeType.CreditNote.HasValue ? feeType.CreditNote : (decimal?)null,
                    StudentFeeDueID = feeType.StudentFeeDueID,
                    PayableAmount = feeType.ReceivableAmount,
                    Refund = feeType.RefundAmount,
                    Amount = feeType.Balance,
                    CollectedAmount = feeType.CollectedAmount,
                    FeeDueAmount = feeType.DueAmount,
                    FeeDueMonthlyFinal = feeDueMonthly
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FinalSettlementViewModel, FinalSettlementDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FinalSettlementFeeTypeViewModel, FeeCollectionFeeTypeDTO>.CreateMap();
            Mapper<FeeDueMonthlyFinalViewModel, FeeCollectionMonthlySplitDTO>.CreateMap();
            var dto = Mapper<FinalSettlementViewModel, FinalSettlementDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            //dto.FeeReceiptNo = this.FeeReceiptNo;
            dto.FinalSettlementIID = this.FinalSettlementIID;
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.SectionID = this.SectionID;
            dto.ClassID = this.ClassID;
            dto.SchoolID = this.SchoolID;
            dto.AcadamicYearID = this.AcademicYearID;
            dto.Remarks = this.Remarks;
            //dto.ClassFeeMasterId = this.FeeClass == null || string.IsNullOrEmpty(this.FeeClass.Key) ? (long?)null : long.Parse(this.FeeClass.Key);
            dto.SettlementDate = string.IsNullOrEmpty(this.SettlementDateString) ? (DateTime?)null : DateTime.ParseExact(this.SettlementDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.CollectionDate = string.IsNullOrEmpty(this.CollectionDateString) ? (DateTime?)null : DateTime.ParseExact(this.CollectionDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
            foreach (var feePayment in this.FeePaymentMap)
            {
                if (feePayment.PayAmount.HasValue)
                {
                    dto.FeeCollectionPaymentModeMapDTO.Add(new FeeCollectionPaymentModeMapDTO()
                    {
                        Amount = feePayment.PayAmount,
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
                    foreach (var feeMonthlySplit in feeType.FeeDueMonthlyFinal)
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
                                PrvCollect = feeMonthlySplit.PrvCollect,
                                ReceivableAmount = feeMonthlySplit.PayableAmount,

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
                            ReceivableAmount = feeType.PayableAmount,
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
            return jsonString == null ? null : JsonConvert.DeserializeObject<FinalSettlementDTO>(jsonString);
        }

    }
}