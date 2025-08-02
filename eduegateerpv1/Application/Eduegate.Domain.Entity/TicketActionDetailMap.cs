namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cs.TicketActionDetailMaps")]
    public partial class TicketActionDetailMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TicketActionDetailMap()
        {
            TicketActionDetailDetailMaps = new HashSet<TicketActionDetailDetailMap>();
        }

        [Key]
        public long TicketActionDetailIID { get; set; }

        public long TicketID { get; set; }

        public int? RefundTypeID { get; set; }

        public decimal? RefundAmount { get; set; }

        [StringLength(200)]
        public string Reason { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        [StringLength(50)]
        public string ReturnNumber { get; set; }

        public int? GiveItemTo { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? IssueType { get; set; }

        public long? AssignedEmployee { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timestamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketActionDetailDetailMap> TicketActionDetailDetailMaps { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
