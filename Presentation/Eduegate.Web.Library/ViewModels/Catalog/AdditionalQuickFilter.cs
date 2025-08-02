using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Catalog
{
    public class AdditionalQuickFilter
    {
        public AdditionalQuickFilter()
        {
            IsDirty = false;
            IsMandatory = false;
        }

        public string Field { get; set; }
        public string FilterName { get; set; }
        public string FilterLabel { get; set; }
        public object Value { get; set; }
        public bool IsDirty { get; set; }
        public bool IsMandatory { get; set; }
        public string FilterValue { get; set; }
        public string FilterType { get; set; }
    }
}
