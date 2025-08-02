namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.PackingTypes")]
    public partial class PackingType
    {
        [Key]
        public short PackingTypeIID { get; set; }

        [Column("PackingType")]
        [StringLength(50)]
        public string PackingType1 { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public decimal? PackingCost { get; set; }

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
