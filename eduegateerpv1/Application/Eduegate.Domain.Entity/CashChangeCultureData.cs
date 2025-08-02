namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.CashChangeCultureDatas")]
    public partial class CashChangeCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CashChangeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual CashChanx CashChanx { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
