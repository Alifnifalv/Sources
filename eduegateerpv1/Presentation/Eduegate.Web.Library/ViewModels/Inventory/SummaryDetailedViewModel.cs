using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class SummaryDetailedViewModel : SummaryViewModel
    {
        [Order(3)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.DetailViewModel|sumByKey:'TotalDiscount' | number:3}}")]
        [DisplayName("Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [Order(4)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "{{CRUDModel.Model.DetailViewModel|sumByKey:'Amount' | number:3}}")]
        [DisplayName("Total Amount")]
        public decimal TotalAmmount { get; set; }

        [Order(5)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "")]
        [DisplayName("Created on")]
        public DateTime Created { get; set; }

        [Order(6)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "")]
        [DisplayName("Updated on")]
        public DateTime Updated { get; set; }

        [Order(7)]
        [ControlType(Framework.Enums.ControlTypes.Label, "", "")]
        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
