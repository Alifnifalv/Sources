using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ViewAction
    {
        public ViewAction()
        {
            
        }

        public long ViewActionID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public string ActionCaption { get; set; }
        public string ActionAttribute { get; set; }
        public string ActionAttribute2 { get; set; }

        public virtual View View { get; set; }
    }
}
