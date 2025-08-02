using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ViewColumnCultureDatas", Schema = "setting")]
    public partial class ViewColumnCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewColumnID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(50)]
        public string ColumnName { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual ViewColumn ViewColumn { get; set; }
    }
}