using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.CRM.Models
{
    [Table("MarketSegments", Schema = "crm")]
    public partial class MarketSegment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MarketSegment()
        {
            Leads = new HashSet<Lead>();
        }
        [Key]
        public byte MarketSegmentID { get; set; }

        [StringLength(50)]
        public string MarketSegmentName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lead> Leads { get; set; }
    }
}