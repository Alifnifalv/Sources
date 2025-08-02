using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Common
{
    public class ScreenFieldSettingViewModel
    {
        public ScreenFieldSettingViewModel()
        {
            IsSet = false;
        }

        public long ScreenFieldSettingID { get; set; }
        public long ScreenFieldID { get; set; }
        public string FieldName { get; set; }
        public string ModelName { get; set; }
        public string LookupName { get; set; }
        public string DateType { get; set; }
        public string DefaultValue { get; set; }
        public string DefaultFormat { get; set; }
        public bool IsSet { get; set; }
    }
}
