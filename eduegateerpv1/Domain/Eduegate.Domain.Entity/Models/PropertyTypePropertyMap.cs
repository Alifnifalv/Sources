using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PropertyTypePropertyMap
    {
        [Key]
        public byte PropertyTypeID { get; set; }
        public long PropertyID { get; set; }
        public virtual PropertyType PropertyType { get; set; }
    }
}
