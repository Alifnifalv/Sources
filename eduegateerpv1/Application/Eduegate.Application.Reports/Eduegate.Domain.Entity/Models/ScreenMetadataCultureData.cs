using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("setting.ScreenMetadataCultureDatas")]
    public partial class ScreenMetadataCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        public string ListButtonDisplayName { get; set; }

        public string DisplayName { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}