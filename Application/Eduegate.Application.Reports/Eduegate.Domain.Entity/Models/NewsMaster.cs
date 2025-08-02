using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NewsMaster
    {
        public long NewsID { get; set; }
        public string ReferenceKey { get; set; }
        public string NewsTitleEn { get; set; }
        public string NewsTitleAr { get; set; }
        public string DetailsEn { get; set; }
        public string DetailsAr { get; set; }
        public string ImageName { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
