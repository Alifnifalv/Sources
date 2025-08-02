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
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    public class FeeDueEditViewModel : BaseMasterViewModel
    {
        public FeeDueEditViewModel()
        {

            Academic = new KeyValueViewModel();
            FeeDueMaps = new List<FeeDueFeeTypeViewModel>() { new FeeDueFeeTypeViewModel() };
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            InvoiceDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
           
            FeeFines = new List<FeeDueFineViewModel>() { new FeeDueFineViewModel() };
        }


        public long StudentFeeDueIID { get; set; }
        public long FeeStructureIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }

        public System.DateTime? InvoiceDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FeeDueDate")]
        public string FeeDueGenerationDateString { get; set; }

        public System.DateTime? FeeDueGenerationDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Student")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine10 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]        
        [CustomDisplay("Class")]
        public string ClassName { get; set; }
        public int ClassId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Section")]
        public string SectionName { get; set; }
        public int SectionId { get; set; }

       


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<FeeDueFeeTypeViewModel> FeeDueMaps { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Fines")]
        public List<FeeDueFineViewModel> FeeFines { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        public bool CollectionStatus { get; set; }
       
        public bool IsAccountPostEdit { get; set; }
        public bool CollectionStatusEdit { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentFeeDueDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeDueEditViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentFeeDueDTO, FeeDueEditViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
           
            Mapper<FeeDueFeeTypeMapDTO, FeeDueFeeTypeViewModel>.CreateMap();
            Mapper<FeeDueMonthlySplitDTO, FeeDueMonthlySplitViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var feeDto = dto as StudentFeeDueDTO;
            var vm = Mapper<StudentFeeDueDTO, FeeDueEditViewModel>.Map(dto as StudentFeeDueDTO);
            vm.InvoiceNo= feeDto.InvoiceNo;
            vm.CollectionStatus = feeDto.CollectionStatus;
            vm.StudentFeeDueIID = feeDto.StudentFeeDueIID;
            vm.Academic = feeDto.AcademicYear.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.AcademicYear.Key.ToString(), Value = feeDto.AcademicYear.Value };
            vm.InvoiceDateString= (feeDto.InvoiceDate.HasValue ? feeDto.InvoiceDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            
            vm.FeeDueGenerationDateString= (feeDto.DueDate.HasValue ? feeDto.DueDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.StudentName = feeDto.StudentName;
            vm.StudentID = feeDto.StudentId;
            vm.ClassId = feeDto.ClassId.Value ;
            vm.ClassName = feeDto.Class.Value ;
            vm.SectionName = feeDto.SectionName;
            vm.IsAccountPostEdit = feeDto.IsAccountPost;
            vm.CollectionStatusEdit = feeDto.CollectionStatus;
            //vm.
            vm.FeeDueMaps = new List<FeeDueFeeTypeViewModel>();
            vm.FeeFines = new List<FeeDueFineViewModel>();

            if (feeDto.FeeFineMap != null)
            {
                foreach (var feeFines in feeDto.FeeFineMap)
                {
                    if (feeFines.Amount.HasValue && feeFines.FineName.Length > 0)
                    {
                        vm.FeeFines.Add(new FeeDueFineViewModel()
                        {
                            FineMasterID = feeFines.FineMasterID,
                            FineMasterStudentMapID = feeFines.FineMasterStudentMapID,
                            StudentFeeDueID= feeFines.StudentFeeDueID,
                            FeeDueFeeTypeMapsID= feeFines.FeeDueFeeTypeMapsID,
                            Amount = feeFines.Amount,
                            Fine = feeFines.FineName
                        });
                    }
                }
            }


            if (feeDto.FeeDueFeeTypeMap != null)
            {
                foreach (var feeType in feeDto.FeeDueFeeTypeMap)
                {
                    if (feeType.Amount.HasValue)
                    {
                        var monthlySplit = new List<FeeDueMonthlySplitViewModel>();
                        foreach (var feeMonthlySplit in feeType.FeeDueMonthlySplit)
                        {
                            if (feeMonthlySplit.Amount.HasValue)
                            {
                                var entity = new FeeDueMonthlySplitViewModel()
                                {

                                    MonthID = feeMonthlySplit.MonthID,
                                    MapIID = feeMonthlySplit.FeeDueMonthlySplitIID,
                                    FeeDueMonthlySplitID= feeMonthlySplit.FeeDueMonthlySplitIID,
                                    FeeStructureMontlySplitMapID= feeMonthlySplit.FeeStructureMontlySplitMapID,
                                    MonthName = feeMonthlySplit.MonthID == 0 ? null : new DateTime(2010, feeMonthlySplit.MonthID, 1).ToString("MMM") + " " + feeMonthlySplit.Year,
                                    Year= feeMonthlySplit.Year,
                                    Amount = feeMonthlySplit.Amount.HasValue ? feeMonthlySplit.Amount : (decimal?)null,

                                };
                                monthlySplit.Add(entity);
                            }
                        }

                        vm.FeeDueMaps.Add(new FeeDueFeeTypeViewModel()
                        {
                            FeeDueMonthly = monthlySplit,
                            InvoiceNo = feeType.InvoiceNo,
                            FeeCollectionStatus = feeType.FeeCollectionStatus.Value,
                            IsRowSelected = feeType.IsRowSelected,
                            StudentFeeDueID = feeType.StudentFeeDueID,
                            IsFeePeriodDisabled = feeType.IsFeePeriodDisabled,
                            FeeStructureFeeMapID = feeType.FeeStructureFeeMapID,
                            FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                            FeeMaster = KeyValueViewModel.ToViewModel(feeType.FeeMaster),
                            FeePeriod = KeyValueViewModel.ToViewModel(feeType.FeePeriod),
                            Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                            FeeMasterID = string.IsNullOrEmpty(feeType.FeeMaster.Key) ? (int?)null : int.Parse(feeType.FeeMaster.Key),
                            FeePeriodID = string.IsNullOrEmpty(feeType.FeePeriod.Key) ? (int?)null : int.Parse(feeType.FeePeriod.Key),
                            InvoiceDate = (feeType.InvoiceDate.HasValue ? feeType.InvoiceDate.Value : DateTime.Now).ToShortDateString()
                        });
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {

            Mapper<FeeDueEditViewModel, StudentFeeDueDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeDueFeeTypeViewModel, FeeDueFeeTypeMapDTO>.CreateMap();
            Mapper<FeeDueMonthlySplitViewModel, FeeDueMonthlySplitDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<FeeDueEditViewModel, StudentFeeDueDTO>.Map(this);

            dto.StudentFeeDueIID = this.StudentFeeDueIID;
            dto.StudentId = this.StudentID;
            dto.ClassId = this.ClassId;
            dto.InvoiceNo = this.InvoiceNo;
            dto.AcadamicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.InvoiceDate = string.IsNullOrEmpty(this.InvoiceDateString) ? (DateTime?)null : DateTime.ParseExact(this.InvoiceDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.DueDate = string.IsNullOrEmpty(this.FeeDueGenerationDateString) ? (DateTime?)null : DateTime.ParseExact(this.FeeDueGenerationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.StudentFeeDueIID = this.StudentFeeDueIID;
            dto.FeeDueFeeTypeMap = new List<FeeDueFeeTypeMapDTO>();
            
            dto.IsAccountPostEdit = this.IsAccountPostEdit;
            dto.CollectionStatusEdit = this.CollectionStatusEdit;
            foreach (var feeType in this.FeeDueMaps)
            {
                if (feeType.Amount.HasValue)
                {
                    var montlySplitDto = new List<FeeDueMonthlySplitDTO>();
                    foreach (var feeMonthlySplit in feeType.FeeDueMonthly)
                    {
                        if (feeMonthlySplit.Amount.HasValue)
                        {
                            var entity = new FeeDueMonthlySplitDTO()
                            {
                                MonthID = feeMonthlySplit.MonthID,
                                Year = feeMonthlySplit.Year,                                
                                FeeDueMonthlySplitIID = feeMonthlySplit.MapIID,
                                FeeStructureMontlySplitMapID = feeMonthlySplit.FeeStructureMontlySplitMapID.HasValue? feeMonthlySplit.FeeStructureMontlySplitMapID.Value:0,
                                FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                                FeePeriodID = string.IsNullOrEmpty(feeType.FeePeriod.Key) ? (int?)null : int.Parse(feeType.FeePeriod.Key),
                                Amount = feeMonthlySplit.Amount.HasValue ? feeMonthlySplit.Amount : (decimal?)null
                            };

                            montlySplitDto.Add(entity);
                        }
                    }
                    if (feeType.FeeMasterID.HasValue)
                    {

                        dto.FeeDueFeeTypeMap.Add(new FeeDueFeeTypeMapDTO()
                        {
                            FeeDueMonthlySplit = montlySplitDto,
                            IsRowSelected = feeType.IsRowSelected,
                            StudentFeeDueID = feeType.StudentFeeDueID,
                            FeeCollectionStatus = feeType.FeeCollectionStatus,
                            FeeStructureFeeMapID = feeType.FeeStructureFeeMapID,
                            FeeDueFeeTypeMapsIID = feeType.FeeDueFeeTypeMapsID,
                            Amount = feeType.Amount.HasValue ? feeType.Amount : (decimal?)null,
                            FeeMasterID = string.IsNullOrEmpty(feeType.FeeMaster.Key) ? (int?)null : int.Parse(feeType.FeeMaster.Key),
                            FeePeriodID = string.IsNullOrEmpty(feeType.FeePeriod.Key) ? (int?)null : int.Parse(feeType.FeePeriod.Key),
                            //TaxAmount = feeType.TaxAmount.HasValue ? feeType.TaxAmount : (decimal?)null,
                            //TaxPercentage = feeType.TaxPercentage.HasValue ? feeType.TaxPercentage : (decimal?)null,
                        });
                    }
                }
            }
            foreach (var fines in this.FeeFines)
                {
                    if (fines.FineMasterID.HasValue)
                    {

                        dto.FeeFineMap.Add(new FeeDueFeeFineMapDTO()
                        {

                            StudentFeeDueID = fines.StudentFeeDueID,
                          
                            FeeDueFeeTypeMapsID = fines.FeeDueFeeTypeMapsID,
                            Amount = fines.Amount.HasValue ? fines.Amount : (decimal?)null,
                            FineMasterStudentMapID= fines.FineMasterStudentMapID.Value,
                            FineMasterID = !fines.FineMasterID.HasValue ? (int?)null : fines.FineMasterID.Value,

                        });
                    }
                }
           
            return dto;
        }


        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentFeeDueDTO>(jsonString);
        }
    }
}
