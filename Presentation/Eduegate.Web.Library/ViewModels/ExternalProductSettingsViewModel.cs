using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExternalProductSettings", "CRUDModel.ViewModel.ExternalSettings.ExternalProductSettings")]
    [DisplayName("")]
    public class ExternalProductSettingsViewModel : BaseMasterViewModel
    {
        public long CustomerID { get; set; }
        public long ProductSKUMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("PartNo")]
        public string PartNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("External BarCode")]
        public string ExternalBarcode { get; set; }


        public static ExternalProductSettingsViewModel POSProductDTOtoExternalProductSettingsViewModel(POSProductDTO dto)
        {
            if (dto != null)
            {
                return new ExternalProductSettingsViewModel()
                {
                    ProductSKUMapID = dto.ProductSKUMapIID,
                    ProductName = dto.ProductName,
                    PartNo = dto.PartNo,
                    ExternalBarcode = dto.Barcode,
                };
            }
            else
            {
                return new ExternalProductSettingsViewModel();
            }
        }

    }
}
