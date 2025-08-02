using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
  public  class UnitsViewModel
    {
        public long UnitID { get; set; }
      
        public string UnitCode { get; set; }

        public string UnitName { get; set; }
        
        public double? Fraction { get; set; }
        
        public long? UnitGroupID { get; set; }
    }
}
