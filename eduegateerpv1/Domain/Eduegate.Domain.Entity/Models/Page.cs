using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Pages", Schema = "cms")]
    public partial class Page
    {
        public Page()
        {
            this.PageBoilerplateMaps = new List<PageBoilerplateMap>();
            this.PageBoilerplateReports = new List<PageBoilerplateReport>();
            this.Pages1 = new List<Page>();
            this.Sites = new List<Site>();
            this.Sites1 = new List<Site>();
        }

        [Key]
        public long PageID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string PageName { get; set; }
        public Nullable<byte> PageTypeID { get; set; }
        public string Title { get; set; }
        public string TemplateName { get; set; }
        public string PlaceHolder { get; set; }
        public Nullable<long> ParentPageID { get; set; }
        public Nullable<long> MasterPageID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        ////public byte[] TimeStamps { get; set; }
        public Nullable<int> IsCache { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }
        public virtual ICollection<PageBoilerplateReport> PageBoilerplateReports { get; set; }
        public virtual ICollection<Page> Pages1 { get; set; }
        public virtual Page Page1 { get; set; }
        public virtual PageType PageType { get; set; }
        public virtual Site Site { get; set; }
        public virtual ICollection<Site> Sites { get; set; }
        public virtual ICollection<Site> Sites1 { get; set; }
    }
}
