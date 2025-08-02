using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.School.Common;
using Eduegate.Services.Contracts.School.Common;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "StudentList", "CRUDModel.ViewModel.StudentList")]
    [DisplayName("Student List")]
    public class CampusTransferStudentListViewModel : BaseMasterViewModel
    {
        public CampusTransferStudentListViewModel()
        {
            Student = new KeyValueViewModel();
            IsActive = false;        
        }

        public long CampusTransferIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "ex-large-col-width")]
        [Select2("Student", "String", false, "")]
        [LookUp("LookUps.Student")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }


        public byte? FromSchoolID { get; set; }

        public int FromAcademicYearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.StudentList[0], CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.StudentList[0],CRUDModel.ViewModel.StudentList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}