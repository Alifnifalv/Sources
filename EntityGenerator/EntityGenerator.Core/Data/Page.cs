using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Pages", Schema = "cms")]
    public partial class Page
    {
        public Page()
        {
            InverseParentPage = new HashSet<Page>();
            PageBoilerplateMaps = new HashSet<PageBoilerplateMap>();
            PageBoilerplateReports = new HashSet<PageBoilerplateReport>();
            SiteHomePages = new HashSet<Site>();
            SiteMasterPages = new HashSet<Site>();
        }

        [Key]
        public long PageID { get; set; }
        public int? SiteID { get; set; }
        [StringLength(50)]
        public string PageName { get; set; }
        public byte? PageTypeID { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TemplateName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PlaceHolder { get; set; }
        public long? ParentPageID { get; set; }
        public long? MasterPageID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? IsCache { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("PageTypeID")]
        [InverseProperty("Pages")]
        public virtual PageType PageType { get; set; }
        [ForeignKey("ParentPageID")]
        [InverseProperty("InverseParentPage")]
        public virtual Page ParentPage { get; set; }
        [ForeignKey("SiteID")]
        [InverseProperty("Pages")]
        public virtual Site Site { get; set; }
        [InverseProperty("ParentPage")]
        public virtual ICollection<Page> InverseParentPage { get; set; }
        [InverseProperty("Page")]
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        [InverseProperty("Page")]
        public virtual ICollection<PageBoilerplateReport> PageBoilerplateReports { get; set; }
        [InverseProperty("HomePage")]
        public virtual ICollection<Site> SiteHomePages { get; set; }
        [InverseProperty("MasterPage")]
        public virtual ICollection<Site> SiteMasterPages { get; set; }
    }
}
