namespace Eduegate.Domain.Entity.Setting.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ScreenLookupMaps", Schema = "setting")]
    public partial class ScreenLookupMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ScreenLookupMapID { get; set; }

        public long? ScreenID { get; set; }

        public bool? IsOnInit { get; set; }

        [StringLength(50)]
        public string LookUpName { get; set; }

        [StringLength(1000)]
        public string Url { get; set; }

        [StringLength(50)]
        public string CallBack { get; set; }

        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
