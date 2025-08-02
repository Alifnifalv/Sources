using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;


namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalaryComponentRelationsGrid", "CRUDModel.ViewModel.SalaryComponentRelationsGrid")]
    [DisplayName(" ")]
    public class SalaryComponentRelationsGridViewModel : BaseMasterViewModel
    {
        public SalaryComponentRelationsGridViewModel()
        {

        }

        public long SalaryComponentRelationMapIID { get; set; }

        public int? SalaryComponentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Components")]
        [LookUp("LookUps.SalaryComponent")]
        public string RelationComponent { get; set; }
        public int? RelatedComponentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("RelationType")]
        [LookUp("LookUps.SalaryComponentRelationTypes")]
        public string SalaryComponentRelationType { get; set; }
        public short? RelationTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("NoOfDaysApplicable")]
        public string NoOfDaysApplicable { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.SalaryComponentRelationsGrid[0],CRUDModel.ViewModel.SalaryComponentRelationsGrid)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.SalaryComponentRelationsGrid[0],CRUDModel.ViewModel.SalaryComponentRelationsGrid)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }

}


