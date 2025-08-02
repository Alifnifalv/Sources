using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    public class PriceListDetailsViewModel : BaseMasterViewModel
    {
        [DataPicker("Supplier")]
        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [DisplayName("Price List ID")]
        public long ProductPriceListIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Description")]
        public string PriceDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Price")]
        public Nullable<decimal> Price { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Percentage")]
        public Nullable<decimal> PricePercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Valid From")]
        public string StartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Valid To")]
        public string EndDate { get; set; }
    }
}
