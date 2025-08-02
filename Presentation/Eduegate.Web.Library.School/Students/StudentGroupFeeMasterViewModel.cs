using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FeeMasterClass", "CRUDModel.ViewModel")]
    [DisplayName("Fee Details")]
    public class StudentGroupFeeMasterViewModel : BaseMasterViewModel
    {
        public StudentGroupFeeMasterViewModel()
        {
            StudentGroup = new KeyValueViewModel();
            AcadamicYear
                = new KeyValueViewModel();
            FeeMasterMaps = new List<StudentGroupFeeTypeMapViewmodel>() { new StudentGroupFeeTypeMapViewmodel() };

        }
        public long StudentGroupFeeMasterIID { get; set; }

        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false, "")]
        [LookUp("LookUps.AcademicYear")]
        [DisplayName("Acadamic Year")]
        public KeyValueViewModel AcadamicYear { get; set; }
        public int? AcadamicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StudentGroup", "Numeric", false, "")]
        [LookUp("LookUps.StudentGroup")]
        [DisplayName("Student Group")]
        public KeyValueViewModel StudentGroup { get; set; }
        public int? StudentGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(50)]
        [DisplayName("Description")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Eduegate.Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Amount")]
        //public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Fee Periods")]
        public List<StudentGroupFeeTypeMapViewmodel> FeeMasterMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentGroupFeeMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupFeeMasterViewModel>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentGroupFeeMasterDTO, StudentGroupFeeMasterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<FeeMasterClassMonthlySplitDTO, FeeMonthlySplitViewModel>.CreateMap();
            Mapper<StudentGroupFeeTypeMapDTO, StudentGroupFeeTypeMapViewmodel>.CreateMap();
            var mapDTO = dto as StudentGroupFeeMasterDTO;
            var vm = Mapper<StudentGroupFeeMasterDTO, StudentGroupFeeMasterViewModel>.Map(mapDTO);
            
            vm.FeeMasterMaps = new List<StudentGroupFeeTypeMapViewmodel>();
            foreach (var feeMasterMapDto in mapDTO.FeeMasterClassMaps)
            {

                if (feeMasterMapDto.PercentageAmount.HasValue)
                {

                    var feeMasterMonthlySplit = new List<FeeMonthlySplitViewModel>();
                    //foreach (var monthlySplit in feeMasterMapDto.FeeMasterClassMontlySplitMaps)
                    //{
                    //    //if (monthlySplit.Amount.HasValue)
                    //    //{
                    //    //    var splitVM = new FeeMonthlySplitViewModel()
                    //    //    {
                    //    //        MapIID = monthlySplit.FeeMasterClassMontlySplitMapIID,
                    //    //        MonthID = monthlySplit.MonthID,
                    //    //        MonthName = monthlySplit.MonthID == 0 ? null : new DateTime(2010, monthlySplit.MonthID, 1).ToString("MMM"),
                    //    //        Amount = monthlySplit.Amount.HasValue ? monthlySplit.Amount : (decimal?)null,
                    //    //        //TaxAmount = monthlySplit.Tax.HasValue ? monthlySplit.Tax : (decimal?)null,
                    //    //        //TaxPercentage = monthlySplit.TaxPercentage.HasValue ? monthlySplit.TaxPercentage : (decimal?)null
                    //    //    };

                    //    //    feeMasterMonthlySplit.Add(splitVM);
                    //    //}
                    //}
                    vm.FeeMasterMaps.Add(new StudentGroupFeeTypeMapViewmodel()
                    {
                        StudentGroupFeeTypeMapIID = feeMasterMapDto.StudentGroupFeeTypeMapIID,
                        StudentGroupFeeMasterID = feeMasterMapDto.StudentGroupFeeMasterID,
                        FeeMasterID = string.IsNullOrEmpty(feeMasterMapDto.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeeMaster.Key),
                        FeeMaster = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeeMaster),
                        FeePeriodID = string.IsNullOrEmpty(feeMasterMapDto.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterMapDto.FeePeriod.Key),
                        FeePeriod = KeyValueViewModel.ToViewModel(feeMasterMapDto.FeePeriod),
                        PercentageAmount = feeMasterMapDto.PercentageAmount,
                        //TaxPercentage = feeMasterMapDto.TaxPercentage,
                        IsPercentage = feeMasterMapDto.IsPercentage,
                        //TaxAmount = feeMasterMapDto.TaxAmount,
                        //IsDiscount = feeMasterMapDto.IsDiscount,
                        //FeeMonthly = feeMasterMonthlySplit,
                        IsFeePeriodDisabled = feeMasterMapDto.IsFeePeriodDisabled
                    });

                }
            }
            return vm;
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentGroupFeeMasterViewModel, StudentGroupFeeMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<FeeMonthlySplitViewModel, FeeMasterClassMonthlySplitDTO>.CreateMap();
            Mapper<StudentGroupFeeTypeMapViewmodel, StudentGroupFeeTypeMapDTO>.CreateMap();
            var dto = Mapper<StudentGroupFeeMasterViewModel, StudentGroupFeeMasterDTO>.Map(this);
            dto.StudentGroupID = string.IsNullOrEmpty(this.StudentGroup.Key) ? (int?)null : int.Parse(this.StudentGroup.Key);
            dto.AcadamicYearID = string.IsNullOrEmpty(this.AcadamicYear.Key) ? (int?)null : int.Parse(this.AcadamicYear.Key);
            dto.Description = this.Description;
            dto.FeeMasterClassMaps = new List<StudentGroupFeeTypeMapDTO>();

            List<FeeMasterClassMonthlySplitDTO> FeeMasterMonthlyDTO = new List<FeeMasterClassMonthlySplitDTO>();
            foreach (var feeMasterClass in this.FeeMasterMaps)
            {
                if (feeMasterClass.PercentageAmount.HasValue)
                {
                    
                    //foreach (var feeMasterSplit in feeMasterClass.FeeMonthly)
                    //{
                    //    if (feeMasterClass.PercentageAmount.HasValue)
                    //    {
                    //        var monthlySplitDto = new FeeMasterClassMonthlySplitDTO();
                    //        //{
                    //        //    FeeMasterClassMontlySplitMapIID = feeMasterSplit.MapIID,
                    //        //    FeeMasterClassMapID = feeMasterSplit.FeeMasterClassMapID,
                    //        //    MonthID = feeMasterSplit.MonthID,
                    //        //    Amount = feeMasterSplit.Amount.HasValue ? feeMasterSplit.Amount : (decimal?)null,
                    //        //    //Tax = feeMasterSplit.TaxAmount.HasValue ? feeMasterSplit.TaxAmount : (decimal?)null,
                    //        //    //TaxPercentage = feeMasterSplit.TaxPercentage.HasValue ? feeMasterSplit.TaxPercentage : (decimal?)null
                    //        //};

                    //        FeeMasterMonthlyDTO.Add(monthlySplitDto);
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key))
                    {
                        dto.FeeMasterClassMaps.Add(new StudentGroupFeeTypeMapDTO()
                        {
                            StudentGroupFeeTypeMapIID = feeMasterClass.StudentGroupFeeTypeMapIID,
                            StudentGroupFeeMasterID = feeMasterClass.StudentGroupFeeMasterID,
                            FeeMasterID = string.IsNullOrEmpty(feeMasterClass.FeeMaster.Key) ? (int?)null : int.Parse(feeMasterClass.FeeMaster.Key),
                            FeePeriodID = string.IsNullOrEmpty(feeMasterClass.FeePeriod.Key) ? (int?)null : int.Parse(feeMasterClass.FeePeriod.Key),
                            PercentageAmount = feeMasterClass.PercentageAmount,
                            //TaxPercentage = feeMasterClass.TaxPercentage,
                            //TaxAmount = feeMasterClass.TaxAmount,
                            //IsDiscount = feeMasterClass.IsDiscount,
                            IsPercentage = feeMasterClass.IsPercentage,
                            FeeMasterClassMontlySplitMaps = FeeMasterMonthlyDTO
                        });
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupFeeMasterDTO>(jsonString);
        }
    }
}
