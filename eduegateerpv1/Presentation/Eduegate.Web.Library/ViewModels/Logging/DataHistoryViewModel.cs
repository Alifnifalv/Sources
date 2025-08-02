using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Logging
{
    public class DataHistoryViewModel
    {
        public long LogIID { get; set; }
        public long RecordIID { get; set; }
        public string UpdatedDate { get; set; }
        public int UserIID { get; set; }
        public string UpdatedUserName { get; set; }
        public object LastValue { get; set; }
        public object NewValue { get; set; }
    }
}
