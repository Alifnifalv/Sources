using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.ViewModels.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "RelationDetails", "CRUDModel.ViewModel.RelationDetails")]
    [DisplayName("Setting Leave Types")]
    public class EmployeeRelationsDetailsViewModel : BaseMasterViewModel
    {
        public EmployeeRelationsDetailsViewModel()
        {
        }
        public long EmployeeRelationsDetailIID { get; set; }

        public long? EmployeeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("EmployeeRelationTypes", "Numeric", false, "")]
        [LookUp("LookUps.EmployeeRelationTypes")]
        [CustomDisplay("Relation")]
        public KeyValueViewModel EmployeeRelationTypes { get; set; }
        public int? EmployeeRelationTypesID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("First Name")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Middle Name")]
        public string MiddleName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Last Name")]
        public string LastName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NationalID")]
        public string NationalID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("PassportNumber")]
        public string PassportNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ContactNumber")]
        public string ContactNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.RelationDetails[0], CRUDModel.ViewModel.RelationDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.RelationDetails[0],CRUDModel.ViewModel.RelationDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
