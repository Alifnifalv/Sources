namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.StatusesCultureDatas")]
    public partial class StatusesCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte StatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual Status Status { get; set; }
    }
}
