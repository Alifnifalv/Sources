namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("marketing.CampaignStatTypes")]
    public partial class CampaignStatType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CampaignStatType()
        {
            CampaignStatSummaryMaps = new HashSet<CampaignStatSummaryMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CampaignStatTypeID { get; set; }

        [StringLength(50)]
        public string StatName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CampaignStatSummaryMap> CampaignStatSummaryMaps { get; set; }
    }
}
