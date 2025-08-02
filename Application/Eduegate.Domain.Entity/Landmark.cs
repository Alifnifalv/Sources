namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Landmarks")]
    public partial class Landmark
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LandmarkID { get; set; }

        [StringLength(500)]
        public string LandmarkDescription { get; set; }

        public int? LocationID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Locations1 Locations1 { get; set; }
    }
}
