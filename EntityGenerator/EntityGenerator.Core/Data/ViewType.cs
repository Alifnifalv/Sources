using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ViewTypes", Schema = "setting")]
    public partial class ViewType
    {
        public ViewType()
        {
            Views = new HashSet<View>();
        }

        [Key]
        public byte ViewTypeID { get; set; }
        [StringLength(50)]
        public string ViewTypeName { get; set; }

        [InverseProperty("ViewType")]
        public virtual ICollection<View> Views { get; set; }
    }
}
