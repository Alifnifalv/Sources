using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Allocations", "CRUDModel.Model.MasterViewModel.Allocations.Allocations")]
    public class QuantityAllocationViewModel : BaseMasterViewModel
    {
        public long ProductID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label,"")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Available Quantity")]
        public decimal Quantity { get; set; }
        public List<long> BranchIDs { get; set; }
        public List<string> BranchName { get; set; }
        [PivotColumn()]
        [ControlType(Framework.Enums.ControlTypes.PivotColumn)]
        [DisplayName("Quantity")]     
        public List<decimal> AllocatedQuantity { get; set; }
    }
}
