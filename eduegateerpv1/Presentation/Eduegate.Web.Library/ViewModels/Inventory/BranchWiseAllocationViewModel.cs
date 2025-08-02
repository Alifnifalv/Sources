using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    public class BranchWiseAllocationViewModel : BaseMasterViewModel
    {
        public BranchWiseAllocationViewModel()
        {
            Allocations = new List<QuantityAllocationViewModel>() {  new QuantityAllocationViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.PivotGrid)]
        [DisplayName("")]
        public List<QuantityAllocationViewModel> Allocations { get; set; }
    }
}
