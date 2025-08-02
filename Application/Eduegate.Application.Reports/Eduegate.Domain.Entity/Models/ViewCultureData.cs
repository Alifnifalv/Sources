using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("setting.ViewCultureDatas")]
    public partial class ViewCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ViewID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        public string ViewDescription { get; set; }

        public string ViewTitle { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual View View { get; set; }
    }
}