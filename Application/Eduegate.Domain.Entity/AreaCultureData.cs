namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.AreaCultureDatas")]
    public partial class AreaCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AreaID { get; set; }

        [StringLength(50)]
        public string AreaName { get; set; }

        public virtual Area Area { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
