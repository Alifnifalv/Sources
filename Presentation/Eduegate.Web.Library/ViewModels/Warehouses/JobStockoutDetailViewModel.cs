using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    public class JobStockoutDetailViewModel : JobOperationDetailViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        [Order(5)]
        [DisplayName("Quantity")]
        public Nullable<decimal> VerifyQuantity { get; set; }
        
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright serialnum-wrap")]
        //[Order(6)]
        //[DisplayName("Order Quantity")]
        //public Nullable<decimal> ReferenceQuantity { get; set; }
    }
}
