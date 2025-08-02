namespace Eduegate.Domain.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DeliveryOrderTypes", Schema = "inventory")]
    public partial class DeliveryOrderType
    {
        public byte DeliveryOrderTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
