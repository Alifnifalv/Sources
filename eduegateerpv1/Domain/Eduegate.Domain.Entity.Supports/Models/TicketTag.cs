using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models
{
    [Table("TicketTags", Schema = "cs")]
    public partial class TicketTag
    {
        [Key]
        public int TicketTagsID { get; set; }

        [StringLength(50)]
        public string TagName { get; set; }
    }
}