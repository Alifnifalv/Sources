namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.ServiceProviderLogs")]
    public partial class ServiceProviderLog
    {
        [Key]
        public long ServiceProviderLogIID { get; set; }

        public int? ServiceProviderID { get; set; }

        public long? ReferenceDocumentID { get; set; }

        public DateTime? LogDateTime { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
