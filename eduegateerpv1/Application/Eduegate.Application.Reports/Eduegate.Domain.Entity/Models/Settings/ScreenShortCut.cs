using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Settings
{
    public partial class ScreenShortCut
    {
        public ScreenShortCut()
        {
        }

        public long ScreenShortCutID { get; set; }
        public long ScreenID { get; set; }
        public string KeyCode { get; set; }
        public string ShortCutKey { get; set; }
        public string Action { get; set; }
        public virtual ScreenMetadata View { get; set; }
    }
}
