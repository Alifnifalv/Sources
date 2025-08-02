using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Eduegate.Domain.Entity.Models
{
    [Table("account.PeriodClosingTranStatuses")]
    public partial class PeriodClosingTranStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PeriodClosingTranStatus()
        {
            PeriodClosingTranHeads = new HashSet<PeriodClosingTranHead>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PeriodClosingTranStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PeriodClosingTranHead> PeriodClosingTranHeads { get; set; }
    }
}
