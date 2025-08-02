using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("FilterColumnCultureDatas", Schema = "setting")]
    public partial class FilterColumnCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long FilterColumnID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        [StringLength(50)]
        public string ColumnCaption { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual FilterColumn FilterColumn { get; set; }
    }
}