using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FormValues", Schema = "form")]
    public partial class FormValue
    {
        [Key]
        public long FormValueIID { get; set; }
        public long? FormFieldID { get; set; }
        public int? FormID { get; set; }
        public long? ReferenceID { get; set; }
        public string FormFieldName { get; set; }
        public string FormFieldValue { get; set; }

        [ForeignKey("FormID")]
        [InverseProperty("FormValues")]
        public virtual Form Form { get; set; }
        [ForeignKey("FormFieldID")]
        [InverseProperty("FormValues")]
        public virtual FormField FormField { get; set; }
    }
}
