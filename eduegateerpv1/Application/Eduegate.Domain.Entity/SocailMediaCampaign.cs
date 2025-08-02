namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.SocailMediaCampaigns")]
    public partial class SocailMediaCampaign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SocailMediaCampaign()
        {
            CampaignEmployeeMaps = new HashSet<CampaignEmployeeMap>();
            CampaignStatSummaryMaps = new HashSet<CampaignStatSummaryMap>();
            SocailMediaCampaignTags = new HashSet<SocailMediaCampaignTag>();
        }

        [Key]
        public long SocailMediaCampaignIID { get; set; }

        public long? CampaignID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CampaignEmployeeMap> CampaignEmployeeMaps { get; set; }

        public virtual Campaign Campaign { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CampaignStatSummaryMap> CampaignStatSummaryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SocailMediaCampaignTag> SocailMediaCampaignTags { get; set; }
    }
}
