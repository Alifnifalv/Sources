using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FormFields", Schema = "form")]
    public partial class FormField
    {
        public FormField()
        {
            FormValues = new HashSet<FormValue>();
        }

        [Key]
        public long FormFieldID { get; set; }
        public int? FormID { get; set; }
        public string FieldName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTitle { get; set; }
        public bool? IsSubTitle { get; set; }

        [ForeignKey("FormID")]
        [InverseProperty("FormFields")]
        public virtual Form Form { get; set; }
        [InverseProperty("FormField")]
        public virtual ICollection<FormValue> FormValues { get; set; }
    }
}
