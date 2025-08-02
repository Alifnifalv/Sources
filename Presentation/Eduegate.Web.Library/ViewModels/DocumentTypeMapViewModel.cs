using System.ComponentModel;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DocumentTypes", "CRUDModel.ViewModel.DocumentTypeMap.DocumentTypes")]
    [DisplayName("Document Type Maps")]
    public class DocumentTypeMapViewModel : BaseMasterViewModel
    {
        public long BranchDocumentTypeMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, "large-col-width")]
        [LookUp("LookUps.DocumentType")]
        [CustomDisplay("DocumentTypes")]
        public string DocumentTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DocumentTypeMap.DocumentTypes[0], CRUDModel.ViewModel.DocumentTypeMap.DocumentTypes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.DocumentTypeMap.DocumentTypes[0], CRUDModel.ViewModel.DocumentTypeMap.DocumentTypes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}