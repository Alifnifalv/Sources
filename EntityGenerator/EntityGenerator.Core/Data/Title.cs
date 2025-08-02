using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Titles", Schema = "mutual")]
    public partial class Title
    {
        public Title()
        {
            OrderContactMaps = new HashSet<OrderContactMap>();
        }

        [Key]
        public short TitleID { get; set; }
        [StringLength(50)]
        public string TitleName { get; set; }

        [InverseProperty("Title")]
        public virtual ICollection<OrderContactMap> OrderContactMaps { get; set; }
    }
}
