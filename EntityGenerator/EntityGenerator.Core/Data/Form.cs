using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Forms", Schema = "form")]
    public partial class Form
    {
        public Form()
        {
            FormFields = new HashSet<FormField>();
            FormValues = new HashSet<FormValue>();
        }

        [Key]
        public int FormID { get; set; }
        public string FormName { get; set; }
        [StringLength(100)]
        public string ReportName { get; set; }

        [InverseProperty("Form")]
        public virtual ICollection<FormField> FormFields { get; set; }
        [InverseProperty("Form")]
        public virtual ICollection<FormValue> FormValues { get; set; }
    }
}
