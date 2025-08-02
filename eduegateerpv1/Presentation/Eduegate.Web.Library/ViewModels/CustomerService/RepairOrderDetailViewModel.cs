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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "JobDetailDetails", "CRUDModel.Model.MasterViewModel.JobDetail.Details","","","","",true)]
    [DisplayName("Job Details")]
    public class RepairOrderDetailViewModel : BaseMasterViewModel
    {
         public RepairOrderDetailViewModel()
         {
             ShowDynamicPopup = false;
         }

         [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
         [DisplayName("IsRowSelected")]
         public bool IsRowSelected { get; set; }
         public bool SelectAll { get; set; }

         [Required]
         [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
         [DisplayName("Sr.No")]
         public long SerialNo { get; set; }

         [Required]
         [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
         [Select2("OperationGroup", "String", false)]
         [DisplayName("Operation Groups")]
         [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=OPERATIONGROUP", "LookUps.OperationGroup")]
         [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
         public KeyValueViewModel OperationGroup { get; set; }

         [Required]
         [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
         [Select2("OperationCode", "String", false)]
         [DisplayName("Operations")]
         [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=OPERATION", "LookUps.Operation")]
         [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
         public KeyValueViewModel Operation { get; set; }

         [Required]
         [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
         [Select2("Symptom", "String", false)]
         [LazyLoad("", "Mutual/GetLazyLookUpData?lookType=SYMPTOMCODE", "LookUps.Symptoms")]
         [DisplayName("Symptoms")]
         [QuickSmartView("SYMPTOMCODE")]
         public KeyValueViewModel Symptom { get; set; }

         //[Required]
         //[ControlType(Framework.Enums.ControlTypes.TextBoxWithDynamicPopup, "medium-col-width textright serialnum-wrap", "ng-focus='BringDynamicPopup(detail)' ng-readonly='true'")]
         //[DisplayName("Defects")]
         //public List<DefectCodeViewModel> DefectDetails { get; set; }

         public bool ShowDynamicPopup { get; set; }
    }
}
