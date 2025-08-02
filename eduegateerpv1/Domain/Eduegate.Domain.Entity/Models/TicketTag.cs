using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TicketTags", Schema = "cs")]
    public partial class TicketTag
    {
        [Key]
        public int TicketTagsID { get; set; }
        public string TagName { get; set; }
    }
}
