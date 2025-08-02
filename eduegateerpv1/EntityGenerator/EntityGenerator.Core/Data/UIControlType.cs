using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UIControlTypes", Schema = "setting")]
    public partial class UIControlType
    {
        public UIControlType()
        {
            CategorySettings = new HashSet<CategorySetting>();
            FilterColumns = new HashSet<FilterColumn>();
            Properties = new HashSet<Property>();
        }

        [Key]
        public byte UIControlTypeID { get; set; }
        [StringLength(50)]
        public string ControlName { get; set; }

        [InverseProperty("UIControlType")]
        public virtual ICollection<CategorySetting> CategorySettings { get; set; }
        [InverseProperty("UIControlType")]
        public virtual ICollection<FilterColumn> FilterColumns { get; set; }
        [InverseProperty("UIControlType")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
