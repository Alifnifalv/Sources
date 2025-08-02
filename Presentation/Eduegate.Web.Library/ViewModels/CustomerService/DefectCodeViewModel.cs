using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.CustomerService
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DefectDetails", "CRUDModel.Model.MasterViewModel.Defect.DefectDetails", "","","","",true)]
    [DisplayName("Defects")]
    public class DefectCodeViewModel : BaseMasterViewModel
    {
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        //[Select2("DefectSide", "String", false)]
        //[DisplayName("Symptoms")]
        //[LazyLoad("", "CustomerService/RepairOrder/OperationCodes", "LookUps.DefectSide")]
        //[QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        //public string DefectSide { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("DefectSide", "String", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=DefectSide", "LookUps.DefectSide")]
        [DisplayName("Defect Side")]
        [QuickSmartView("DefectSide")]
        public KeyValueViewModel DefectSide { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Description")]
        public string SideDescription { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        //[Select2("DefectCode", "String", false)]
        //[DisplayName("Symptoms")]
        //[LazyLoad("", "CustomerService/RepairOrder/OperationCodes", "LookUps.DefectCode")]
        //[QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        //public string DefectCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("DefectCode", "String", false)]
        [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=DefectCode", "LookUps.DefectCode")]
        [DisplayName("Defect Code")]
        [QuickSmartView("DefectCode")]
        public KeyValueViewModel DefectCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Description")]
        public string CodeDescription { get; set; }
    }
}
