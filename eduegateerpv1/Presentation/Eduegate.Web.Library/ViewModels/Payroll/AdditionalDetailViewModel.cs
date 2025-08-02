using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AdditionalInfo", "CRUDModel.ViewModel.AdditionalInfo")]
    [DisplayName("Additional Info")]
    public class AdditionalDetailViewModel : BaseMasterViewModel
    {
        public long EmployeeAdditionalInfoIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("HighestAcademicQualification")]
        public string HighestAcademicQualitication { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("HighestProfessionalQualification")]
        public string HighestPrefessionalQualitication { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("Total Years of Experience")]
        public string TotalYearsofExperience { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("ClassesTaught")]
        public string ClassessTaught { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("AppointedSubjects")]
        public string AppointedSubject { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("MainSubjectsTaught")]
        public string MainSubjectTought { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500)]
        [CustomDisplay("AdditionalSubjectsTaught")]
        public string AdditioanalSubjectTought { get; set; }
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[MaxLength(500)]
        [CustomDisplay("IsComputerTrained")]
        public bool IsComputerTrained { get; set; }
    }
}
