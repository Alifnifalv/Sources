using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VariableSettings", Schema = "mutual")]
    public partial class VariableSetting
    {
        [Key]
        public int VariableSettingID { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        public string RelatedName { get; set; }
        public string Formula { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
