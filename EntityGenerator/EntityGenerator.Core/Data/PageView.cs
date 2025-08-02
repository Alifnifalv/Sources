using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PageView
    {
        public long PageID { get; set; }
        public int? SiteID { get; set; }
        [StringLength(50)]
        public string SiteName { get; set; }
        [StringLength(50)]
        public string PageName { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TemplateName { get; set; }
        public byte? PageTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }
        public int? NoOfBoilerPlates { get; set; }
        public int? CompanyID { get; set; }
    }
}
