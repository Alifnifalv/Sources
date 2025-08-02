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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "OtherClassTeachers", "CRUDModel.ViewModel.OtherClassTeachers")]
    [DisplayName("Class Teachers Subject Map")]
    public class ClassOtherTeacherMapViewModel : BaseMasterViewModel
    {
        public ClassOtherTeacherMapViewModel()
        {
            Subject = new KeyValueViewModel();
            OtherTeacher = new KeyValueViewModel();
        }

        public long? ClassClassTeacherMapID { get; set; }

        public long ClassTeacherMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "String", false, "")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DepartmentsTeacher", "String", false, "")]
        [LookUp("LookUps.DepartmentsTeacher")]
        [CustomDisplay("Teacher")]
        public KeyValueViewModel OtherTeacher { get; set; }

        public long? OtherTeacherID { get; set; }


        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.OtherClassTeachers[0],CRUDModel.ViewModel.OtherClassTeachers)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.OtherClassTeachers[0],CRUDModel.ViewModel.OtherClassTeachers)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}