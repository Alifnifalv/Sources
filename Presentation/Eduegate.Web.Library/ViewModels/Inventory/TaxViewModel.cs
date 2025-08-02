using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class TaxViewModel : BaseMasterViewModel
    {
        public TaxViewModel()
        {
            IsFixedPercentage = false;
        }

        public int? TaxTemplateItemID { get; set; }
        public int? TaxTemplateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("")]
        public string TaxName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("")]
        public decimal? TaxAmount { get; set; }

        public int? TaxPercentage { get; set; }
        public long TaxID { get; set; }
        public int? TaxTypeID { get; set; }
        public bool IsFixedPercentage { get; set; }
        public decimal? InclusiveTaxAmount { get; set; }
        public decimal? ExclusiveTaxAmount { get; set; }
        public bool? HasTaxInclusive { get; set; }
    }
}
