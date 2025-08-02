using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;


namespace Eduegate.Web.Library.School.Fees
{
    public class FeePeriodsViewModel : BaseMasterViewModel
    {
        public FeePeriodsViewModel()
        {
            //Academic = new KeyValueViewModel();
        }

        public int FeePeriodID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, Attributes = "ng-change=NumberofMonthChanges(CRUDModel.ViewModel)")]
        [CustomDisplay("PeriodFrom")]
        public string PeriodDateString { get; set; }
        public DateTime? PeriodFrom { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes = "ng-change=NumberofMonthChanges(CRUDModel.ViewModel)")]
        [MaxLength(4, ErrorMessage = "Maximum Length should be within 4!")]
        [CustomDisplay("NumberOfMonths")]
        public int? NumberOfPeriods { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("PeriodTo")]
        public string PeriodToDateString { get; set; }
        public DateTime? PeriodTo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Academic", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }
        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is mandatory to pay")]
        public bool? IsMandatoryToPay { get; set; }  
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("Is Transport")]
        public bool? IsTransport { get; set; } 

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FeePeriodsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePeriodsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FeePeriodsDTO, FeePeriodsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var feeDto = dto as FeePeriodsDTO;
            var vm = Mapper<FeePeriodsDTO, FeePeriodsViewModel>.Map(feeDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.AcademicYearID = feeDto.AcademicYearID;
            vm.IsMandatoryToPay = feeDto.IsMandatoryToPay;
            vm.IsTransport = feeDto.IsTransport;
            vm.Academic = feeDto.AcademicYearID.HasValue ? new KeyValueViewModel()
            {
                Key = feeDto.AcademicYearID.ToString(),
                Value = feeDto.AcademicYear.Value
            } : null;
            vm.Academic = KeyValueViewModel.ToViewModel(feeDto.AcademicYear);
            vm.PeriodDateString = Convert.ToDateTime(feeDto.PeriodFrom).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.PeriodToDateString = Convert.ToDateTime(feeDto.PeriodTo).ToString(dateFormat, CultureInfo.InvariantCulture);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FeePeriodsViewModel, FeePeriodsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<FeePeriodsViewModel, FeePeriodsDTO>.Map(this);
            dto.IsMandatoryToPay = this.IsMandatoryToPay;
            dto.IsTransport = this.IsTransport == true ? true : false;
            dto.PeriodFrom = DateTime.ParseExact(this.PeriodDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.PeriodTo = DateTime.ParseExact(this.PeriodToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FeePeriodsDTO>(jsonString);
        }

    }
}
