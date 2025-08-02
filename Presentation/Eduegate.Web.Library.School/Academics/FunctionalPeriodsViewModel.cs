using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "FunctionalPeriods", "CRUDModel.ViewModel")]
    [DisplayName("Functional Period")]
    public class FunctionalPeriodsViewModel : BaseMasterViewModel
    {
        public FunctionalPeriodsViewModel()
        {
        }

        public int FunctionalPeriodID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("School")]
        public string SchoolString { get; set; }
        public byte? SchoolID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Academic Year")]
        public string AcademicYearString { get; set; }
        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FromDate")]
        public string FromDateString { get; set; }
        public DateTime FromDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("ToDate")]
        public string ToDateString { get; set; }
        public DateTime ToDate { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FunctionalPeriodsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FunctionalPeriodsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FunctionalPeriodsDTO, FunctionalPeriodsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var mpdto = dto as FunctionalPeriodsDTO;
            var vm = Mapper<FunctionalPeriodsDTO, FunctionalPeriodsViewModel>.Map(dto as FunctionalPeriodsDTO);
            vm.FromDateString = mpdto.FromDate != null ? mpdto.FromDate.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.ToDateString = mpdto.ToDate != null ? mpdto.ToDate.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            vm.SchoolID = mpdto.SchoolID;
            vm.AcademicYearID = mpdto.AcademicYearID;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FunctionalPeriodsViewModel, FunctionalPeriodsDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<FunctionalPeriodsViewModel, FunctionalPeriodsDTO>.Map(this);
            dto.FromDate = DateTime.ParseExact(this.FromDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.ToDate = DateTime.ParseExact(this.ToDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.AcademicYearID = this.AcademicYearID;
            dto.SchoolID = this.SchoolID;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FunctionalPeriodsDTO>(jsonString);
        }
    }
}

