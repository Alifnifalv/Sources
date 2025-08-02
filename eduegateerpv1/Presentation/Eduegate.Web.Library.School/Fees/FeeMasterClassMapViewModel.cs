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
namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeMasterClass", "CRUDModel.ViewModel")]
    [DisplayName("Fee Details")]
    public class FeeMasterClassMapViewModel : BaseMasterViewModel
    {
        public FeeMasterClassMapViewModel()
        {
            Class = new List<KeyValueViewModel>();
            Academic = new KeyValueViewModel();
            Package = new KeyValueViewModel();
            FeeMasterMaps = new List<FeeMasterClassFeePeriodViewModel>() { new FeeMasterClassFeePeriodViewModel() };
        }

        public long ClassFeeMasterIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", true)]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public List<KeyValueViewModel> Class { get; set; }

        public int? ClassID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Package", "Numeric", false, "")]
        [LookUp("LookUps.Package")]
        [DisplayName("Package")]
        public KeyValueViewModel Package { get; set; }

        public long? PackageConfigID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [DisplayName("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }

        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public byte? FeeCycleID { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee Periods")]
        public List<FeeMasterClassFeePeriodViewModel> FeeMasterMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassFeeMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            
            return JsonConvert.DeserializeObject<FeeMasterClassMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            List<KeyValueViewModel> ClassList = new List<KeyValueViewModel>();
            Mapper<ClassFeeMasterDTO, FeeMasterClassMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeMasterClassMonthlySplitDTO, FeeMonthlySplitViewModel>.CreateMap();
            Mapper<FeeMasterClassFeePeriodDTO, FeeMasterClassFeePeriodViewModel>.CreateMap();
            var feeDto = dto as ClassFeeMasterDTO;
            var vm = Mapper<ClassFeeMasterDTO, FeeMasterClassMapViewModel>.Map(feeDto);
            vm.FeeMasterMaps = new List<FeeMasterClassFeePeriodViewModel>();
            if (feeDto.Class != null && feeDto.Class.Count > 0)
            {
                foreach (KeyValueDTO dtoClass in feeDto.Class)
                {
                    if (dtoClass.Key != null)
                        ClassList.Add(new KeyValueViewModel { Key = dtoClass.Key, Value = dtoClass.Value }
                            );
                }
            }
            vm.Class = ClassList;
            foreach (var feeMasterMapDto in feeDto.FeeMasterClassMaps)
            {               
                if (feeMasterMapDto.Amount.HasValue)
                {
                    
                    var feeMasterMonthlySplit = new List<FeeMonthlySplitViewModel>();
                    foreach (var monthlySplit in feeMasterMapDto.FeeMasterClassMontlySplitMaps)
                    {
                        if (monthlySplit.Amount.HasValue)
                        {
                            var splitVM = new FeeMonthlySplitViewModel()
                            {
                                MapIID = monthlySplit.FeeMasterClassMontlySplitMapIID,
                                MonthID = monthlySplit.MonthID,
                                Year = monthlySplit.MonthID,
                                MonthName = monthlySplit.MonthID == 0 ? null : new DateTime(2010, monthlySplit.MonthID, 1).ToString("MMM"),
                                Amount = monthlySplit.Amount.HasValue ? monthlySplit.Amount : (decimal?)null,
                                //TaxAmount = monthlySplit.Tax.HasValue ? monthlySplit.Tax : (decimal?)null,
                                //TaxPercentage = monthlySplit.TaxPercentage.HasValue ? monthlySplit.TaxPercentage : (decimal?)null
                                FeePeriodID = string.IsNullOrEmpty(feeMasterMapDto.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeePeriod.Key),
                            };

                            feeMasterMonthlySplit.Add(splitVM);
                        }
                    }

                 
                    vm.FeeMasterMaps.Add(new FeeMasterClassFeePeriodViewModel()
                    {
                        FeeMasterClassMapIID = feeMasterMapDto.FeeMasterClassMapIID,
                        ClassFeeMasterID = feeMasterMapDto.ClassFeeMasterID,
                        FeeMasterID = string.IsNullOrEmpty(feeMasterMapDto.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeeMaster.Key),
                        FeeMaster = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeeMaster),
                        FeePeriodID = string.IsNullOrEmpty(feeMasterMapDto.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeePeriod.Key),
                        FeePeriod = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeePeriod),
                        Amount = feeMasterMapDto.Amount,
                        //TaxPercentage = feeMasterMapDto.TaxPercentage,
                        //TaxAmount = feeMasterMapDto.TaxAmount,
                        FeeMonthly = feeMasterMonthlySplit,
                        IsFeePeriodDisabled = feeMasterMapDto.IsFeePeriodDisabled
                    });

                }
            }
            return vm;

        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeeMasterClassMapViewModel, ClassFeeMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeMonthlySplitViewModel, FeeMasterClassMonthlySplitDTO>.CreateMap();
            Mapper<FeeMasterClassFeePeriodViewModel, FeeMasterClassFeePeriodDTO>.CreateMap();
            var dto = Mapper<FeeMasterClassMapViewModel, ClassFeeMasterDTO>.Map(this);
           
            //dto.PackageConfigID = string.IsNullOrEmpty(this.Package.Key) ? (int?)null : int.Parse(this.Package.Key);
            dto.AcadamicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.Description = this.Description;
            dto.FeeMasterClassMaps = new List<FeeMasterClassFeePeriodDTO>();
            List<KeyValueDTO> ClassList = new List<KeyValueDTO>();
            if (this.Class != null || this.Class.Count > 0)
            {

                foreach (KeyValueViewModel vmc in this.Class)
                {
                    if (vmc.Key != null)
                        ClassList.Add(new KeyValueDTO { Key = vmc.Key, Value = vmc.Value }
                    );
                }
            }

            dto.Class = ClassList;

            foreach (var feeMasterClass in this.FeeMasterMaps)
            {
                if (feeMasterClass.Amount.HasValue)
                {
                    List<FeeMasterClassMonthlySplitDTO> FeeMasterMonthlyDTO = new List<FeeMasterClassMonthlySplitDTO>();
                    foreach (var feeMasterSplit in feeMasterClass.FeeMonthly)
                    {
                        if (feeMasterClass.Amount.HasValue)
                        {
                            var monthlySplitDto = new FeeMasterClassMonthlySplitDTO()
                            {
                                FeeMasterClassMontlySplitMapIID = feeMasterSplit.MapIID,
                                FeeMasterClassMapID=feeMasterSplit.FeeMasterClassMapID,
                                MonthID = feeMasterSplit.MonthID,
                                Year = feeMasterSplit.Year,
                                FeePeriodID = string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),
                                Amount = feeMasterSplit.Amount.HasValue ? feeMasterSplit.Amount : (decimal?)null,
                                //Tax = feeMasterSplit.TaxAmount.HasValue ? feeMasterSplit.TaxAmount : (decimal?)null,
                                //TaxPercentage = feeMasterSplit.TaxPercentage.HasValue ? feeMasterSplit.TaxPercentage : (decimal?)null
                            };

                            FeeMasterMonthlyDTO.Add(monthlySplitDto);
                        }
                    }

                    if (!string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key))
                    {
                        dto.FeeMasterClassMaps.Add(new FeeMasterClassFeePeriodDTO()
                        {
                            FeeMasterClassMapIID = feeMasterClass.FeeMasterClassMapIID,
                            ClassFeeMasterID = feeMasterClass.ClassFeeMasterID,
                            FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
                            FeePeriodID = string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),
                            Amount = feeMasterClass.Amount,
                            //TaxPercentage = feeMasterClass.TaxPercentage,
                            //TaxAmount = feeMasterClass.TaxAmount,
                            FeeMasterClassMontlySplitMaps = FeeMasterMonthlyDTO
                        });
                    }
                }
            }

            return dto;

        }
        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassFeeMasterDTO>(jsonString);
        }
    }
}
