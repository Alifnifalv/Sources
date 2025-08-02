using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PageView
    {
        public long PageID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string SiteName { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string TemplateName { get; set; }
        public Nullable<byte> PageTypeID { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> NoOfBoilerPlates { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
