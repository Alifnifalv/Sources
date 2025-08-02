namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Operators")]
    public partial class Operator
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OperatorID { get; set; }

        [Column("Operator")]
        [StringLength(250)]
        public string Operator1 { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }
    }
}
