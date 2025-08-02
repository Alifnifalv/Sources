using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("Settings", Schema = "schools")]
    public partial class Setting
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string SettingCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyID { get; set; }

        public int? SiteID { get; set; }

        [StringLength(1000)]
        public string SettingValue { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool? ShowProductImageForPOS { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(50)]
        public string ValueType { get; set; }

        public int? LookupTypeID { get; set; }
    }
}