using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentTypeSettings", Schema = "mutual")]
    public partial class DocumentTypeSetting
    {
        [Key]
        public long DocumentTypeSettingID { get; set; }
        public int? DocumentTypeID { get; set; }
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("DocumentTypeID")]
        [InverseProperty("DocumentTypeSettings")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
