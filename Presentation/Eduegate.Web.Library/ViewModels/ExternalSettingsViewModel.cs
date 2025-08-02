using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerExternalSettings", "CRUDModel.ViewModel.ExternalSettings")]
    [DisplayName("ExternalSettings")]
    public class ExternalSettingsViewModel : BaseMasterViewModel
    {
        public ExternalSettingsViewModel()
        {
            //ExternalProductSettings = new List<ExternalProductSettingsViewModel>() { new ExternalProductSettingsViewModel(){ }  };
            ProductCategoriesAttach = new List<SupplierProductCategoriesAttachmentViewModel>() { new SupplierProductCategoriesAttachmentViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Productor/Service Description")]
        public string ProductorServiceDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Pricing Information")]
        public string PricingInformation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Lead Time Days")]
        public int? LeadTimeDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Minimum Order Quantities (MOQs)")]
        public int? MinOrderQty { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Warranty/Guarantee Information")]
        public string Warranty_GuaranteeInfo { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.Products")]
        //[Select2("ProductSKUMapID", "Numeric", false)]
        //[DisplayName("Product Settings")]
        public KeyValueViewModel Product { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Grid)]
        //[DisplayName("ExternalSettings")]
        //public List<ExternalProductSettingsViewModel> ExternalProductSettings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Product Categories")]
        public List<SupplierProductCategoriesAttachmentViewModel> ProductCategoriesAttach { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ProductCategoriesAttach", "CRUDModel.ViewModel.ExternalSettings.ProductCategoriesAttach")]
    [DisplayName("")]
    public class SupplierProductCategoriesAttachmentViewModel : BaseMasterViewModel
    {
        public SupplierProductCategoriesAttachmentViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Product Categories")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "PrdctCategories", "")]
        public long? PrdctCategories { get; set; }
        public string PrdctCategoriesID { get; set; }

    }
}
