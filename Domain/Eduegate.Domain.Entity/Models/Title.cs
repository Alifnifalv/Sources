using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Titles", Schema = "mutual")]
    public partial class Title
    {
        public Title()
        {
            this.OrderContactMaps = new List<OrderContactMap>();
        }

        [Key]
        public short TitleID { get; set; }
        public string TitleName { get; set; }
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
