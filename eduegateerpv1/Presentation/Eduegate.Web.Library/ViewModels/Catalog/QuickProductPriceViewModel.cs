using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Prices", "CRUDModel.ViewModel.Prices")]
    [DisplayName("Price Settings")]
    public class QuickProductPriceViewModel : BaseMasterViewModel
    {
        public long ProductPriceListProductMapIID { get; set; }
        public long? ProductPriceListID { get; set; }
        public decimal? PricePercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("PriceType")]
        public string PriceDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("Price")]
        public decimal? Price { get; set; }

        [HasClaim(HasClaims.HASITEMDISCOUNT)]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("Discount")]
        public decimal? Discount { get; set; }

        [HasClaim(HasClaims.HASITEMDISCOUNT)]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [CustomDisplay("Discount%")]
        public decimal? DiscountPercentage { get; set; }
    }
}
