using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels
{
    public class TagViewModel
    {
        public List<long> ProductSKUIDs { get; set; }
        public List<KeyValueViewModel> AvailableTags { get; set; }
        public List<KeyValueViewModel> SelectedTags { get; set; }
    }
}
