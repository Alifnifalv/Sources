using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UIControlValidations", Schema = "setting")]
    public partial class UIControlValidation
    {
        public UIControlValidation()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        public byte UIControlValidationID { get; set; }
        [StringLength(50)]
        public string ValidationName { get; set; }

        [InverseProperty("UIControlValidation")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
