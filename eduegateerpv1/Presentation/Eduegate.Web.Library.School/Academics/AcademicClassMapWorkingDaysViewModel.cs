using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "WorkingDayMaps", "CRUDModel.ViewModel.WorkingDayMaps")]
    [DisplayName("Working Day Map")]
    public class AcademicClassMapWorkingDaysViewModel : BaseMasterViewModel
    {
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

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.WorkingDayMaps[0], CRUDModel.ViewModel.WorkingDayMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.WorkingDayMaps[0],CRUDModel.ViewModel.WorkingDayMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}