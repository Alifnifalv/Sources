namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeConcessionTypes")]
    public partial class FeeConcessionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ConcessionTypeID { get; set; }

        [StringLength(50)]
        public string TypeName { get; set; }

        public DateTime? ApplicableFrom { get; set; }

        public DateTime? ApplicableTo { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
