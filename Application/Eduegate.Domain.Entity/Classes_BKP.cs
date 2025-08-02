namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.Classes_BKP")]
    public partial class Classes_BKP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string ClassDescription { get; set; }

        public byte? ShiftID { get; set; }

        public byte? SchoolID { get; set; }

        public int? CostCenterID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? ORDERNO { get; set; }
    }
}
