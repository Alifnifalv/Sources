namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ScreenShortCuts", Schema = "setting")]
    public partial class ScreenShortCut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenShortCutID { get; set; }

        public long? ScreenID { get; set; }

        [StringLength(25)]
        public string KeyCode { get; set; }

        [StringLength(50)]
        public string ShortCutKey { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
