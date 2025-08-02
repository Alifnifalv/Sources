namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("crm.OpportunityStatuses")]
    public partial class OpportunityStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpportunityStatus()
        {
            Opportunities = new HashSet<Opportunity>();
        }

        public byte OpportunityStatusID { get; set; }

        [StringLength(50)]
        public string OpportunityStatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
    }
}
