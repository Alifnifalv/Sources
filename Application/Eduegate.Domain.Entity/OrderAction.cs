namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.OrderActions")]
    public partial class OrderAction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ActionID { get; set; }

        [StringLength(50)]
        public string Action { get; set; }
    }
}
