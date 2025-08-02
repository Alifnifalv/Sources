using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Mutual
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}