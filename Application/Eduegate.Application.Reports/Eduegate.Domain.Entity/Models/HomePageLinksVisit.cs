using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class HomePageLinksVisit
    {
        public int HomePageLinkVisitID { get; set; }
        public Nullable<byte> RefHomePageLinkID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CustomerSessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public string VisitFrom { get; set; }
    }
}
