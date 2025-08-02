using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ViewActions", Schema = "setting")]
    public partial class ViewAction
    {
        [Key]
        public long ViewActionID { get; set; }
        public long? ViewID { get; set; }
        [StringLength(100)]
        public string ActionCaption { get; set; }
        [StringLength(1000)]
        public string ActionAttribute { get; set; }
        [StringLength(1000)]
        public string ActionAttribute2 { get; set; }

        [ForeignKey("ViewID")]
        [InverseProperty("ViewActions")]
        public virtual View View { get; set; }
    }
}
