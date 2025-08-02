using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Logging
{
    public class DataHistoryResultViewModel
    {
        public int DataHistoryEntityID { get; set; }
        public string Title { get; set; }
        public List<DataHistoryViewModel> HistoryData { get; set; }
    }
}
