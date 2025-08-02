using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AdditionlInfo", "CRUDModel.ViewModel.AdditionlInfo")]
    [DisplayName("Additional Info")]
    public class StudentAdditionalDetailsViewModel : BaseMasterViewModel
    {
        public StudentAdditionalDetailsViewModel()
        {
            StudentGroup = new List<KeyValueViewModel>();
        }
        public long StudentMiscDetailsIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Employee")]
        //[Select2("Employee", "Numeric", false)]
        //[LookUp("LookUps.Employee")]
        //public KeyValueViewModel Staff { get; set; }
        //public long? StaffID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Discound Group")]
        //[LookUp("LookUps.StudentGroup")]
        //public string StudentGroup { get; set; }

        //public int? StudentGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Discount Group")]
        [Select2("StudentGroup", "Numeric", true)]
        [LookUp("LookUps.StudentGroup")]

        //public string StudentGroup { get; set; }
        public List<KeyValueViewModel> StudentGroup { get; set; }

        public int? StudentGroupID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Relation")]
        //[LookUp("LookUps.GaurdianType")]
        //public string GuardianType { get; set; }

        //public byte? GuardianTypeID { get; set; }
    }
}
