using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MarkRegisterDetails", "CRUDModel.ViewModel.MarkRegisterDetails")]
    [DisplayName("Mark Register Details Assign")]
    public class MarkRegisterDetailsViewmodel : BaseMasterViewModel
    {
        public MarkRegisterDetailsViewmodel()
        {
            Student = new KeyValueViewModel();
            MarkRegisterDetailsSplit = new List<MarkRegisterDetailsSplitViewModel>() { new MarkRegisterDetailsSplitViewModel() };
        }

        public long MarkRegisterStudentMapIID { get; set; }

        public long?  MarkRegisterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Student")]
        [Select2("Students", "Numeric", false, "OnStudentChange(gridModel, $select,$index)", false, "ng-click=LoadStudent($index)")]
        [LookUp("LookUps.Students")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("StudentDetails")]
        public string Studentdetails { get; set; }

    

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.MarkRegisterDetails[0], CRUDModel.ViewModel.MarkRegisterDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.MarkRegisterDetails[0],CRUDModel.ViewModel.MarkRegisterDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MarkRegisterDetailsSplit", Attributes4 = "colspan=6")]
        [DisplayName("")]
        public List<MarkRegisterDetailsSplitViewModel> MarkRegisterDetailsSplit { get; set; }


    }
}
