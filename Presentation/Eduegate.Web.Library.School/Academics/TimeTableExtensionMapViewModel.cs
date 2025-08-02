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

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "MapDetails", "CRUDModel.ViewModel.MapDetails")]
    [DisplayName("Time Table Extension Map")]
    public class TimeTableExtensionMapViewModel : BaseMasterViewModel
    {
        public TimeTableExtensionMapViewModel()
        {
            Class = new List<KeyValueViewModel>();
            Section = new List<KeyValueViewModel>();
            Subject = new List<KeyValueViewModel>();
            Teacher = new List<KeyValueViewModel>();
            ClassTiming = new List<KeyValueViewModel>();
            ClassTimingOperator = new KeyValueViewModel();
            WeekDay = new List<KeyValueViewModel>();
            WeekDayOperator = new KeyValueViewModel();
        }

        //public long? MapDetailIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        //[DisplayName("Sr.No")]
        //public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Classes", "String", true, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public List<KeyValueViewModel> Class { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Section", "String", true, "")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("Subject", "String", true, "SubjectChanges($event, $element, CRUDModel.ViewModel.MapDetails)")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public List<KeyValueViewModel> Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "ex-large-col-width")]
        [Select2("Teacher", "String", true, "")]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("Teacher")]
        public List<KeyValueViewModel> Teacher { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("WeekDay", "String", true, "")]
        [LookUp("LookUps.WeekDay")]
        [CustomDisplay("WeekDay")]
        public List<KeyValueViewModel> WeekDay { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("LogicalOperators", "String", false, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.MapDetails.WeekDay[0].Key")]
        [LookUp("LookUps.LogicalOperators")]
        [CustomDisplay("WeekDay-Conditions")]
        public KeyValueViewModel WeekDayOperator { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("ClassTime", "String", true, "")]
        [LookUp("LookUps.ClassTime")]
        [CustomDisplay("ClassTiming")]
        public List<KeyValueViewModel> ClassTiming { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("LogicalOperators", "String", false, "", optionalAttribute1: "ng-disabled=!CRUDModel.ViewModel.MapDetails.ClassTiming[0].Key")]
        [LookUp("LookUps.LogicalOperators")]
        [CustomDisplay("ClassTiming-Conditions")]
        public KeyValueViewModel ClassTimingOperator { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.MapDetails[0],CRUDModel.ViewModel.MapDetails)'")]
        //[DisplayName("+")]
        //public string Add { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.MapDetails[0],CRUDModel.ViewModel.MapDetails)'")]
        //[DisplayName("-")]
        //public string Remove { get; set; }

    }
}