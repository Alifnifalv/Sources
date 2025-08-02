using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Notification
{
    [Table("EmailTemplateParameterMaps", Schema = "notification")]
    public partial class EmailTemplateParameterMap
    {
        [Key]
        public int EmailTemplateParameterMapID { get; set; }

        public int? EmailTemplateID { get; set; }

        [MaxLength(50)]
        public byte[] TemplateVariable { get; set; }

        [StringLength(20)]
        public string VariableType { get; set; }

        public virtual EmailTemplate2 EmailTemplate { get; set; }
    }
}