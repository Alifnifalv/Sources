using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SchoolWorkingDayMaps", "CRUDModel.ViewModel.SchoolWorkingDayMaps")]
    [DisplayName("School Working Day Map")]
    public class AcademicSchoolMapWorkingDaysViewModel : BaseMasterViewModel
    {

        public long SchoolDateSettingMapsIID { get; set; }

        public long? SchoolDateSettingID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Month")]
        public string MonthName { get; set; }
        public byte? MonthID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Year")]
        public int? YearID { get; set; }

        //[Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("WorkingDays")]
        public int? TotalWorkingDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("PayrollCutOffDate")]
        public string PayrollCutOffDateString { get; set; }
        public DateTime? PayrollCutOffDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SchoolWorkingDayMaps[0], CRUDModel.ViewModel.SchoolWorkingDayMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SchoolWorkingDayMaps[0],CRUDModel.ViewModel.SchoolWorkingDayMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}