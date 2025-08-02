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
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeStructure", "CRUDModel.ViewModel")]
    [DisplayName("Fee Details")]
    public class FeeStructureViewModel : BaseMasterViewModel
    {
        public FeeStructureViewModel()
        {
            IsActive = true;
            //Academic = new KeyValueViewModel();           
            FeeMasterMaps = new List<FeeStructureFeeTypeViewModel>() { new FeeStructureFeeTypeViewModel() };
        }

        public long FeeStructureIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "AcademicYearChanges($event, $element, CRUDModel.ViewModel)", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Name")]
        public string Name { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        public byte? FeeCycleID { get; set; }

        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool IsActive { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("FeeTypes")]
        public List<FeeStructureFeeTypeViewModel> FeeMasterMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeeStructureDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {

            return JsonConvert.DeserializeObject<FeeStructureViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            List<KeyValueViewModel> ClassList = new List<KeyValueViewModel>();
            Mapper<FeeStructureDTO, FeeStructureViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeStructureMontlySplitMapDTO , FeeStructureMonthlySplitViewModel>.CreateMap();
            Mapper<FeeStructureFeeMapDTO, FeeStructureFeeTypeViewModel>.CreateMap();
            var feeDto = dto as FeeStructureDTO;
            var vm = Mapper<FeeStructureDTO, FeeStructureViewModel>.Map(feeDto);
            vm.Name = feeDto.Name;
            vm.IsActive = feeDto.IsActive;
            vm.Description= feeDto.Description;
            vm.FeeMasterMaps = new List<FeeStructureFeeTypeViewModel>();
            vm.Academic = feeDto.AcademicYear.Key == null ? new KeyValueViewModel() { Key = null, Value = null } : new KeyValueViewModel() { Key = feeDto.AcademicYear.Key.ToString(), Value = feeDto.AcademicYear.Value };
            foreach (var feeMasterMapDto in feeDto.FeeStructureFeeMaps)
            {
                if (feeMasterMapDto.Amount.HasValue)
                {

                    var feeMasterMonthlySplit = new List<FeeStructureMonthlySplitViewModel>();
                    foreach (var monthlySplit in feeMasterMapDto.FeeStructureMontlySplitMaps)
                    {
                        if (monthlySplit.Amount.HasValue)
                        {
                            var splitVM = new FeeStructureMonthlySplitViewModel()
                            {
                                MapIID = monthlySplit.FeeStructureMontlySplitMapIID,
                                MonthID = monthlySplit.MonthID,
                                Year = monthlySplit.Year,
                                MonthName = monthlySplit.MonthID == 0 ? null : new DateTime(2010, monthlySplit.MonthID, 1).ToString("MMM") +" " + monthlySplit.Year,
                                Amount = monthlySplit.Amount.HasValue ? monthlySplit.Amount : (decimal?)null,
                                //TaxAmount = monthlySplit.Tax.HasValue ? monthlySplit.Tax : (decimal?)null,
                                //TaxPercentage = monthlySplit.TaxPercentage.HasValue ? monthlySplit.TaxPercentage : (decimal?)null
                                FeePeriodID = string.IsNullOrEmpty(feeMasterMapDto.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeePeriod.Key),
                            };

                            feeMasterMonthlySplit.Add(splitVM);
                        }
                    }

                    vm.FeeMasterMaps.Add(new FeeStructureFeeTypeViewModel()
                    {
                        FeeStructureFeeMapIID = feeMasterMapDto.FeeStructureFeeMapIID,
                        FeeStructureID = feeMasterMapDto.FeeStructureID,
                        FeeMasterID = string.IsNullOrEmpty(feeMasterMapDto.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeeMaster.Key),
                        FeeMaster = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeeMaster),
                        FeePeriodID = string.IsNullOrEmpty(feeMasterMapDto.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeePeriod.Key),
                        FeePeriod = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeePeriod),
                        Amount = feeMasterMapDto.Amount,                        
                        FeeMonthly = feeMasterMonthlySplit,
                        IsFeePeriodDisabled = feeMasterMapDto.IsFeePeriodDisabled
                    });

                }
            }
            return vm;

        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeStructureViewModel, FeeStructureDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeMonthlySplitViewModel, FeeMasterClassMonthlySplitDTO>.CreateMap();
            Mapper<FeeStructureFeeTypeViewModel, FeeMasterClassFeePeriodDTO>.CreateMap();
            var dto = Mapper<FeeStructureViewModel, FeeStructureDTO>.Map(this);

            //dto.PackageConfigID = string.IsNullOrEmpty(this.Package.Key) ? (int?)null : int.Parse(this.Package.Key);
            dto.AcadamicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.Name = this.Name;
            dto.IsActive = this.IsActive;
            dto.Description = this.Description;
            dto.FeeStructureFeeMaps = new List<FeeStructureFeeMapDTO>();
            List<KeyValueDTO> ClassList = new List<KeyValueDTO>();
           
            foreach (var feeMasterClass in this.FeeMasterMaps)
            {
                if (feeMasterClass.Amount.HasValue)
                {
                    List<FeeStructureMontlySplitMapDTO> FeeMasterMonthlyDTO = new List<FeeStructureMontlySplitMapDTO>();
                    foreach (var feeMasterSplit in feeMasterClass.FeeMonthly)
                    {
                        if (feeMasterClass.Amount.HasValue)
                        {
                            var monthlySplitDto = new FeeStructureMontlySplitMapDTO()
                            {
                                FeeStructureMontlySplitMapIID = feeMasterSplit.MapIID,
                                FeeStructureFeeMapID = feeMasterSplit.FeeStructureFeeMapID,
                                MonthID = feeMasterSplit.MonthID,
                                Year = feeMasterSplit.Year,                                
                                Amount = feeMasterSplit.Amount.HasValue ? feeMasterSplit.Amount : (decimal?)null,
                            };

                            FeeMasterMonthlyDTO.Add(monthlySplitDto);
                        }
                    }

                    if (!string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key))
                    {
                        dto.FeeStructureFeeMaps.Add(new FeeStructureFeeMapDTO()
                        {
                            FeeStructureFeeMapIID = feeMasterClass.FeeStructureFeeMapIID,
                            FeeStructureID = feeMasterClass.FeeStructureID,
                            FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
                            FeePeriodID = string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),
                            Amount = feeMasterClass.Amount,                            
                            FeeStructureMontlySplitMaps = FeeMasterMonthlyDTO
                        });
                    }
                }
            }

            return dto;

        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeeStructureDTO>(jsonString);
        }
    }
}

