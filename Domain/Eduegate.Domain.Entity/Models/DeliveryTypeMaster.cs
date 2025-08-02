using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryTypeMaster", Schema = "cms")]
    public partial class DeliveryTypeMaster
    {
        [Key]
        public int DeliveryTypeID { get; set; }
        public string Description { get; set; }
    }
}