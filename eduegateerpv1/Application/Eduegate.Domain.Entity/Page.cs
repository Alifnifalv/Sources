namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.Pages")]
    public partial class Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Page()
        {
            PageBoilerplateMaps = new HashSet<PageBoilerplateMap>();
            Pages1 = new HashSet<Page>();
            Sites = new HashSet<Site>();
            Sites1 = new HashSet<Site>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PageID { get; set; }

        public int? SiteID { get; set; }

        [StringLength(50)]
        public string PageName { get; set; }

        public byte? PageTypeID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        [StringLength(50)]
        public string PlaceHolder { get; set; }

        public long? ParentPageID { get; set; }

        public long? MasterPageID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? IsCache { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageBoilerplateMap> PageBoilerplateMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Page> Pages1 { get; set; }

        public virtual Page Page1 { get; set; }

        public virtual PageType PageType { get; set; }

        public virtual Site Site { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Sites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Site> Sites1 { get; set; }
    }
}
