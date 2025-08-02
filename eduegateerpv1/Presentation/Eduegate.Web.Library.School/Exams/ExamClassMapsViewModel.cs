using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExamClass", "CRUDModel.ViewModel.ExamClass.ExamClasses")]
    [DisplayName("Exam Class")]
    public class ExamClassMapsViewModel:BaseMasterViewModel
    {
        public ExamClassMapsViewModel()
        {
            StudentClass = new KeyValueViewModel();
            Section = new KeyValueViewModel();
        }
     
        public long ExamClassMapIID { get; set; }

        public long? ExamID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.ExamSchedule")]
        //[DisplayName("Exam Schedule")]
        public long? ExamScheduleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false)]
        //[LookUp("LookUps.NonFilterClasses")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        public int? ClassID { get; set; }

        
        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false)]
        //[LookUp("LookUps.NonFilterSections")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }

        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.ExamClass.ExamClasses[0], CRUDModel.ViewModel.ExamClass.ExamClasses)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ExamClass.ExamClasses[0],CRUDModel.ViewModel.ExamClass.ExamClasses)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
