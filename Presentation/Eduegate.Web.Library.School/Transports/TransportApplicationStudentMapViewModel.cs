using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "TransportApplicationStudentMaps", "CRUDModel.ViewModel.TransportApplicationStudentMaps")]
    [DisplayName("Student Details")]
    public class TransportApplicationStudentMapViewModel : BaseMasterViewModel
    {
        public TransportApplicationStudentMapViewModel()
        {
            //Class = new KeyValueViewModel();
            IsActive = true;
            IsNewRider = false;
            IsMedicalCondition = false;
            CheckBoxStudent = true;
        }

        public long TransportApplctnStudentMapIID { get; set; }

        public long? TransportApplicationID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Class")]
        //public string ClassName { get; set; }
        //public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }
        public string ClassName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Student")]
        [Select2("Student", "Numeric", false)]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        public string StudentName { get; set; }
        public string FirstName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Last Name")]
        public string LastName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("School Name")]
        public string SchoolName { get; set; }
        public byte? SchoolID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Gender")]
        //[LookUp("LookUps.Gender")]
        public string Gender { get; set; }
        public byte? GenderID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FromDate")]
        public string StartDateString { get; set; }
        public DateTime? StartDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width")]
        //[DisplayName("Is Active")]
        public bool? IsActive { get; set; }
        public bool? CheckBoxStudent { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox,"small-col-width")]
        [CustomDisplay("IsNewRider")]
        public bool? IsNewRider { get; set; }
        public bool? LocationChange { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width")]
        [CustomDisplay("IsMedicalCondition")]
        public bool? IsMedicalCondition { get; set; }

        [StringLength(500)]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown/*,"disabled"*/)]
        [CustomDisplay("ApplicationStatus")]
        [LookUp("LookUps.TransportApplicationStatus")]
        public string ApplicationStatus { get; set; }
        public byte? TransportApplcnStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Status Remarks")]
        public string Remarks1 { get; set; }

        public int? CreatedByID { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedDateString { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.TransportApplicationStudentMaps[0], CRUDModel.ViewModel.TransportApplicationStudentMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.TransportApplicationStudentMaps[0],CRUDModel.ViewModel.TransportApplicationStudentMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}