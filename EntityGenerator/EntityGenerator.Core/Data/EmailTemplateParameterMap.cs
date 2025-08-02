using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailTemplateParameterMaps", Schema = "notification")]
    public partial class EmailTemplateParameterMap
    {
        [Key]
        public int EmailTemplateParameterMapID { get; set; }
        public int? EmailTemplateID { get; set; }
        [MaxLength(50)]
        public byte[] TemplateVariable { get; set; }
        [StringLength(20)]
        public string VariableType { get; set; }

        [ForeignKey("EmailTemplateID")]
        [InverseProperty("EmailTemplateParameterMaps")]
        public virtual EmailTemplate2 EmailTemplate { get; set; }
    }
}
