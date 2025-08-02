using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ViewTypes", Schema = "setting")]
    public partial class ViewType
    {
        public ViewType()
        {
            this.Views = new List<View>();
        }

        [Key]
        public byte ViewTypeID { get; set; }
        public string ViewTypeName { get; set; }
        public virtual ICollection<View> Views { get; set; }
    }
}
