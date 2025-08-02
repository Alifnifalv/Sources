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
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeCollection", "CRUDModel.ViewModel")]
    [DisplayName("Fee Details")]
    public class FeeCollectionViewModel : BaseMasterViewModel
    {
        public FeeCollectionViewModel()
        {
            FeeCollectionStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED");
            Student = new KeyValueViewModel();
            FeeFines = new List<FeeCollectionFineViewModel>() { new FeeCollectionFineViewModel() };
            FeeTypes = new List<FeeCollectionFeeTypeViewModel>() { new FeeCollectionFeeTypeViewModel() };
            FeeInvoice = new List<FeeCollectionPendingInvoiceViewModel>() { new FeeCollectionPendingInvoiceViewModel() };
            FeePaymentMap = new List<FeeCollectionPaymentModeMapViewModel>() { new FeeCollectionPaymentModeMapViewModel() };
           // Summary=new List<FeeCollectionFeeSummaryViewModel>() { new FeeCollectionFeeSummaryViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            CollectionDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
            Academic = new KeyValueViewModel();
            IsAutoFill = false; ;
        }

        public long FeeCollectionIID { get; set; }

        public long FeeStructureIID { get; set; }

        public bool? IsAutoFill { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CollectionDate")]
        public string CollectionDateString { get; set; }
        public System.DateTime? CollectionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }
        public string AcademicYearString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr", Attributes = "ng-bind=GetTotalFeeAmount(CRUDModel.ViewModel.FeeTypes,CRUDModel.ViewModel.PreviousFees,CRUDModel.ViewModel.FeeFines)")]
        [CustomDisplay("TotalDueAmount")]
        public decimal? TotalAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine20 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Student", "String", false, "StudentChanges(CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllStudents", "LookUps.AllStudents")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }

        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DataPicker, "textleft")]
        [CustomDisplay("SelectStudent")]
        [DataPicker("StudenAdvancedSearch")]
        public string ReferenceStudent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "currSign htmllabelinfo-curr", Attributes = "ng-bind=GetTotalCollectedAmount(CRUDModel.ViewModel.FeePaymentMap) | number")]
        [CustomDisplay("TotalCollectedAmount")]
        public decimal? CollectedAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignright")]
        [DisplayName("")]
        public string NewLine { get; set; }

        public byte? FeeReceiptID { get; set; }
        public string FeeReceiptNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft2")]
        [CustomDisplay("AdmissionNo.")]
        public string AdmissionNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine25 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "texlright")]
        [CustomDisplay("Class")]
        public string Class { get; set; }

        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "texlright")]
        [CustomDisplay("Section")]
        public string Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine21 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("SiblingOutstandingInfo")]
        public string SiblingFeeInfo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignright")]
        [DisplayName("")]
        public string NewLine11 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=StudentChanges(CRUDModel.ViewModel)")]
        //[DisplayName("Get Invoice Data")]
        //public string GenerateButton { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("PendingFeeInvoiceDetails")]
        public List<FeeCollectionPendingInvoiceViewModel> FeeInvoice { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Pending Fee Invoice Details")]
        //public List<FeeCollectionPreviousFeesViewModel> PreviousFees { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<FeeCollectionFeeTypeViewModel> FeeTypes { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("Fee Summary")]
        //public List<FeeCollectionFeeSummaryViewModel> Summary { get; set; }
      
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Fines")]
        public List<FeeCollectionFineViewModel> FeeFines { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeePayment")]
        public List<FeeCollectionPaymentModeMapViewModel> FeePaymentMap { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Cashier", "String", false)]
        [CustomDisplay("Cashier")]
        [LookUp("LookUps.Cashier")]
        public KeyValueViewModel CashierEmployee { get; set; }
        public long? CashierID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }


        public string EmailID { get; set; }

        public byte? SchoolID { get; set; }

        public string FeeCollectionStatus { get; set; }
     
        //Online fee history related changes
        public bool IsExpand { get; set; }

        public string SchoolName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeCollectionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCollectionViewModel>(jsonString);
        }

        public FeeCollectionViewModel FromStudentDataFromStudentVM(StudentViewModel studentVM)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var vm = new FeeCollectionViewModel()
            {
                ClassID = studentVM.ClassID,
                StudentID = studentVM.StudentIID,
                AdmissionNumber = studentVM.AdmissionNumber,
                SectionID = studentVM.SectionID == null ? (int?)null : studentVM.SectionID,
                Section = studentVM.SectionName,
                Class = studentVM.ClassName,
                Student = new KeyValueViewModel() { Key = studentVM.StudentIID.ToString(), Value = studentVM.FirstName + " " + studentVM.MiddleName + " " + studentVM.LastName },
                CollectionDateString = (!string.IsNullOrEmpty(CollectionDateString) ? CollectionDateString : DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture)),
                AcademicYearID =studentVM.AcademicYearID,
                EmailID = studentVM.EmailID,
                SchoolID= studentVM.SchoolID,
                Academic = new KeyValueViewModel() { Key = studentVM.AcademicYearID.ToString(), Value = studentVM.Academicyear },
                AcademicYearString = studentVM.Academicyear                 
            };

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeeCollectionDTO, FeeCollectionViewModel>.CreateMap();
            Mapper<FeeCollectionFeeTypeDTO, FeeCollectionFeeTypeViewModel>.CreateMap();
            Mapper<FeeCollectionFeeFinesDTO, FeeCollectionFineViewModel>.CreateMap();
            Mapper<FeeCollectionPendingInvoiceDTO, FeeCollectionPendingInvoiceViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var feeDto = dto as FeeCollectionDTO;
            var vm = Mapper<FeeCollectionDTO, FeeCollectionViewModel>.Map(dto as FeeCollectionDTO);

            List<long> invoiceNoList = new List<long>();
            decimal totalAmount = 0;
            vm.IsAutoFill = feeDto.IsAutoFill;
            vm.FeeCollectionIID = feeDto.FeeCollectionIID;
            vm.SiblingFeeInfo = feeDto.SiblingFeeInfo;
            vm.Section = feeDto.SectionName;
            vm.Class = feeDto.ClassName;
            vm.SectionID = vm.SectionID;
            vm.ClassID = vm.ClassID;
            vm.Academic = feeDto.AcademicYear == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.AcademicYear.Key.ToString(), Value = feeDto.AcademicYear.Value };
            vm.Student = new KeyValueViewModel() { Key = vm.StudentID.ToString(), Value = feeDto.StudentName };
            vm.CollectionDateString = (vm.CollectionDate.HasValue ? vm.CollectionDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);           
            vm.FeePaymentMap = new List<FeeCollectionPaymentModeMapViewModel>();
            vm.FeeTypes = new List<FeeCollectionFeeTypeViewModel>();
            vm.FeeInvoice = new List<FeeCollectionPendingInvoiceViewModel>();
            vm.FeeFines = new List<FeeCollectionFineViewModel>();
            vm.AdmissionNumber = feeDto.AdmissionNo;
            vm.EmailID = feeDto.EmailID;
            vm.CashierEmployee = feeDto.CashierEmployee == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.CashierEmployee.ToString(), Value = feeDto.CashierEmployee.Value };
            vm.Remarks = feeDto.Remarks;
            vm.FeeCollectionStatus = feeDto.FeeCollectionStatusID.HasValue ? feeDto.FeeCollectionStatusID.ToString() : null;
            vm.FeeInvoice = new List<FeeCollectionPendingInvoiceViewModel>();

            if (feeDto.FeeCollectionPendingInvoiceDTO != null && feeDto.FeeCollectionPendingInvoiceDTO.Count() > 0)
            {
                foreach (var dat in feeDto.FeeCollectionPendingInvoiceDTO)
                {
                    if ((dat.Amount - (dat.CollAmount ?? 0) - (dat.CrDrAmount ?? 0)) != 0)
                    {
                        vm.FeeInvoice.Add(new FeeCollectionPendingInvoiceViewModel()
                        {
                            InvoiceNo = dat.InvoiceNo,
                            Amount = dat.Amount,
                            StudentFeeDueID = dat.StudentFeeDueID,
                            FeePeriodID = dat.FeePeriodID,
                            InvoiceDate = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString(dateFormat, CultureInfo.InvariantCulture),
                            Remarks = dat.Remarks,
                            IsExternal = dat.IsExternal,
                            FeeMasterID = dat.FeeMasterID,
                            CrDrAmount = dat.CrDrAmount,
                            CollAmount = dat.CollAmount,
                            Balance = dat.Amount - (dat.CrDrAmount ?? 0) - (dat.CollAmount ?? 0)
                        });
                    }
                }
            }
            if (feeDto.FeeFines != null)
            {
                foreach (var feeFines in feeDto.FeeFines)
                {
                    if (feeFines.Amount.HasValue && feeFines.FineName.Length > 0)
                    {
                        vm.FeeFines.Add(new FeeCollectionFineViewModel()
                        {
                            InvoiceNo = feeFines.InvoiceNo,
                            InvoiceDateString = (feeFines.InvoiceDate.HasValue ? feeFines.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                            FineMasterID = feeFines.FineMasterID,
                            FineMasterStudentMapID = feeFines.FineMasterStudentMapID,
                            FeeDueFeeTypeMapsID = feeFines.FeeDueFeeTypeMapsID,
                            StudentFeeDueID = feeFines.StudentFeeDueID,
                            Amount = feeFines.Amount,
                            Fine = feeFines.FineName,                            
                            CreditNote = feeFines.CreditNoteAmount,
                            PrvCollect = feeFines.PrvCollect,
                            NowPaying = feeFines.Amount - (feeFines.CreditNoteAmount ?? 0),
                            Balance = feeFines.Amount - ((feeFines.CreditNoteAmount ?? 0) - feeFines.NowPaying ?? 0),
                        });
                    }
                }
            }

            if (feeDto.FeeTypes != null)
            {

                foreach (var feeType in feeDto.FeeTypes)
                {
                    if (feeType.Amount.HasValue)
                    {

                        var montlySplitDto = new List<FeeAssignMonthlySplitViewModel>();
                        foreach (var feeMonthlySplit in feeType.MontlySplitMaps)
                        {
                            if (feeMonthlySplit.Amount.HasValue)
                            {
                                var entity = new FeeAssignMonthlySplitViewModel()
                                {

                                    MonthID = feeMonthlySplit.MonthID,
                                    Amount = feeMonthlySplit.Amount.HasValue ? feeMonthlySplit.Amount : (decimal?)null,
                                    Year = feeMonthlySplit.Year,
                                    FeeDueMonthlySplitID = feeMonthlySplit.FeeDueMonthlySplitID,
                                    FeeCollectionFeeTypeMapId = feeType.FeeCollectionFeeTypeMapsIID,
                                    CreditNote = feeMonthlySplit.CreditNoteAmount.HasValue ? feeMonthlySplit.CreditNoteAmount : (decimal?)null,
                                   
                                    IsRowSelected = true,
                                    MonthName = feeMonthlySplit.MonthID == 0 ? null : new DateTime(2010, feeMonthlySplit.MonthID, 1).ToString("MMM") + " " + feeMonthlySplit.Year,
                                    CreditNoteFeeTypeMapID = feeMonthlySplit.CreditNoteFeeTypeMapID.HasValue ? feeMonthlySplit.CreditNoteFeeTypeMapID : (long?)null,
                                    //TaxAmount = feeMonthlySplit.TaxAmount.HasValue ? feeMonthlySplit.TaxAmount : (decimal?)null,
                                    //TaxPercentage = feeMonthlySplit.TaxPercentage.HasValue ? feeMonthlySplit.TaxPercentage : (decimal?)null,                                   
                                    PrvCollect = feeMonthlySplit.PrvCollect,
                                    NowPaying = feeMonthlySplit.Amount - (feeMonthlySplit.CreditNoteAmount ?? 0),
                                    Balance = feeMonthlySplit.Amount - ((feeMonthlySplit.CreditNoteAmount ?? 0) - feeMonthlySplit.NowPaying ?? 0),
                                };

                                montlySplitDto.Add(entity);
                            }
                        }

                        vm.FeeTypes.Add(new FeeCollectionFeeTypeViewModel()
                        {

                            InvoiceNo = feeType.InvoiceNo,
                            IsRowSelected = true,
                            FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                            StudentFeeDueID = feeType.StudentFeeDueID,
                            FeeMasterID = feeType.FeeMasterID,
                            FeeStructureFeeMapID = feeType.FeeStructureFeeMapID,
                            FeePeriodID = !feeType.FeePeriodID.HasValue ? (int?)null : feeType.FeePeriodID.Value,
                            FeePeriod = feeType.FeePeriod != null ? feeType.FeePeriod : null,
                            Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                            FeeMaster = feeType.FeeMaster,
                            CreditNoteFeeTypeMapID = feeType.CreditNoteFeeTypeMapID.HasValue ? feeType.CreditNoteFeeTypeMapID : (long?)null,
                            //FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod : null,
                            FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                            IsFeePeriodDisabled = feeType.IsFeePeriodDisabled,
                            InvoiceDateString = (feeType.InvoiceDate.HasValue ? feeType.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture),
                            CreditNote = feeType.CreditNoteAmount.HasValue ? feeType.CreditNoteAmount : (decimal?)null,
                            NowPaying = feeType.Amount - (feeType.CreditNoteAmount ?? 0),
                            Balance = feeType.Amount - ((feeType.CreditNoteAmount ?? 0) - feeType.NowPaying ?? 0),                            
                            FeeMonthly = montlySplitDto,
                            FineMasterID = (int?)null,
                            FineMasterStudentMapID = (int?)null,
                        });;

                        var feeCollectionFeeType = new FeeCollectionFeeTypeViewModel();

                        if (invoiceNoList.Contains(feeType.StudentFeeDueID.Value) == false)
                        {

                            totalAmount = feeDto.FeeTypes.Where(y => y.StudentFeeDueID == feeType.StudentFeeDueID.Value).Sum(x => x.Amount.Value);

                            invoiceNoList.Add(feeType.StudentFeeDueID.Value);
                            vm.FeeInvoice.Add(new FeeCollectionPendingInvoiceViewModel()
                            {
                                InvoiceNo = feeType.InvoiceNo,
                                StudentFeeDueID = feeType.StudentFeeDueID,
                                Amount = totalAmount,
                                IsRowSelected = true,
                                InvoiceDate = (feeType.InvoiceDate.HasValue ? feeType.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture)
                            });
                        }

                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeCollectionViewModel, FeeCollectionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeCollectionFeeTypeViewModel, FeeCollectionFeeTypeDTO>.CreateMap();
            Mapper<FeeCollectionFineViewModel, FeeCollectionFeeFinesDTO>.CreateMap();
            Mapper<FeeAssignMonthlySplitViewModel, FeeCollectionMonthlySplitDTO>.CreateMap();
            var dto = Mapper<FeeCollectionViewModel, FeeCollectionDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            dto.CollectionDate = string.IsNullOrEmpty(this.CollectionDateString) ? (DateTime?)null : DateTime.ParseExact(this.CollectionDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key);
            dto.SectionID = this.SectionID;
            dto.ClassID = this.ClassID;
            dto.AcadamicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.FeeCollectionPaymentModeMapDTO = new List<FeeCollectionPaymentModeMapDTO>();
            dto.CashierID = string.IsNullOrEmpty(this.CashierEmployee.Key) ? (long?)null : long.Parse(this.CashierEmployee.Key);
            dto.Remarks = this.Remarks;
            dto.SchoolID = this.SchoolID;
            dto.FeeCollectionStatusID = string.IsNullOrEmpty(this.FeeCollectionStatus) ? (int?)null : int.Parse(this.FeeCollectionStatus);
            dto.Amount = this.FeePaymentMap.Count > 0 ? this.FeePaymentMap.Sum(s => s.Amount) : 0;
            dto.PaidAmount = this.FeePaymentMap.Count > 0 ? this.FeePaymentMap.Sum(s => s.Amount) : 0;

            foreach (var feePayment in this.FeePaymentMap)
            {
                if (feePayment.Amount > 0)
                {
                    dto.FeeCollectionPaymentModeMapDTO.Add(new FeeCollectionPaymentModeMapDTO()
                    {
                        Amount = feePayment.Amount,
                        ReferenceNo = feePayment.ReferenceNo,
                        PaymentModeID = string.IsNullOrEmpty(feePayment.PaymentMode.Key) ? (int?)null : int.Parse(feePayment.PaymentMode.Key),
                        BankID = feePayment.BankID.HasValue ? feePayment.BankID : null,
                        TDate = (feePayment.TDate == null || feePayment.TDate == string.Empty) ? (DateTime?)null : DateTime.ParseExact(feePayment.TDate, dateFormat, CultureInfo.InvariantCulture),

                    });
                }
            }

            dto.FeeFines = new List<FeeCollectionFeeFinesDTO>();

            foreach (var feeFine in this.FeeFines)
            {
                if (feeFine.Amount.HasValue)
                {
                    dto.FeeFines.Add(new FeeCollectionFeeFinesDTO()
                    {
                        FineMasterID = feeFine.FineMasterID,
                        FineMasterStudentMapID = feeFine.FineMasterStudentMapID,
                        FineName = feeFine.Fine,
                        Amount = feeFine.Amount.HasValue ? feeFine.Amount : (decimal?)null,
                        FeeDueFeeTypeMapsID = feeFine.FeeDueFeeTypeMapsID,
                        StudentFeeDueID = feeFine.StudentFeeDueID,
                    });
                }
            }
            dto.FeeTypes = new List<FeeCollectionFeeTypeDTO>();
            foreach (var feeType in this.FeeTypes)
            {
                if (feeType.Amount.HasValue)
                {
                    var montlySplitDto = new List<FeeCollectionMonthlySplitDTO>();
                    foreach (var feeMonthlySplit in feeType.FeeMonthly)
                    {
                        if (feeMonthlySplit.Amount.HasValue && (feeMonthlySplit.IsRowSelected.HasValue ? feeMonthlySplit.IsRowSelected.Value : false) == true)
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
                                CreditNoteFeeTypeMapID = feeMonthlySplit.CreditNoteFeeTypeMapID.HasValue ? feeMonthlySplit.CreditNoteFeeTypeMapID : (long?)null,
                                NowPaying = feeMonthlySplit.NowPaying ,
                                //TaxAmount = feeMonthlySplit.TaxAmount.HasValue ? feeMonthlySplit.TaxAmount : (decimal?)null,
                                //TaxPercentage = feeMonthlySplit.TaxPercentage.HasValue ? feeMonthlySplit.TaxPercentage : (decimal?)null
                            };

                            montlySplitDto.Add(entity);
                        }
                    }
                    if (feeType.FeeMasterID.HasValue)//!string.IsNullOrEmpty(feeType.FeeMaster.Key)
                    {
                        if (feeType.Amount.HasValue && (feeType.IsRowSelected.HasValue ? feeType.IsRowSelected.Value : false) == true)
                        {
                            dto.FeeTypes.Add(new FeeCollectionFeeTypeDTO()
                            {
                                FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                                CreditNoteFeeTypeMapID = feeType.CreditNoteFeeTypeMapID.HasValue ? feeType.CreditNoteFeeTypeMapID.Value : (long?)null,
                                FeeMasterID = feeType.FeeMasterID,
                                FeePeriodID = feeType.FeePeriodID.HasValue ? feeType.FeePeriodID.Value : (int?)null,
                                Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                                FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                                CreditNoteAmount = feeType.CreditNote.HasValue ? feeType.CreditNote : (decimal?)null,
                                Balance = feeType.Balance.HasValue ? feeType.Balance : (decimal?)null,
                                StudentFeeDueID = feeType.StudentFeeDueID,
                                NowPaying = feeType.NowPaying,
                                MontlySplitMaps = montlySplitDto
                            });
                        }

                    }
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeCollectionDTO>(jsonString);
        }
    }
}

