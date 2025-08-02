using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Applications", Schema = "analytics")]
    public partial class Application
    {
        public Application()
        {
            UserReferences = new HashSet<UserReference>();
        }

        [Key]
        public byte AppID { get; set; }
        [StringLength(50)]
        public string AppName { get; set; }

        [InverseProperty("App")]
        public virtual ICollection<UserReference> UserReferences { get; set; }
    }
}
