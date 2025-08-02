namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin.ClaimSetLoginMaps")]
    public partial class ClaimSetLoginMap
    {
        [Key]
        public long ClaimSetLoginMapIID { get; set; }

        public int? CompanyID { get; set; }

        public long? LoginID { get; set; }

        public long? ClaimSetID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ClaimSet ClaimSet { get; set; }

        public virtual Company Company { get; set; }

        public virtual Login Login { get; set; }
    }
}
