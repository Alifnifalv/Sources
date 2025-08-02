using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubjectMaps", "CRUDModel.ViewModel.SubjectMaps")]
    [DisplayName("Class Teachers Subject Map")]
    public class ClassSubjectPeriodMapMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectPeriodMapMapViewModel()
        {
            Subject = new KeyValueViewModel();
        }

        public long? PeriodMapDetailIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "String", false, "")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("WeekPeriods")]
        public int? WeekPeriods { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("TotalPeriods")]
        public int? TotalPeriods { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("MinimumPeriods")]
        public int? MinimumPeriods { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [CustomDisplay("MaximumPeriods")]
        public int? MaximumPeriods { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.SubjectMaps[0],CRUDModel.ViewModel.SubjectMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.SubjectMaps[0],CRUDModel.ViewModel.SubjectMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}