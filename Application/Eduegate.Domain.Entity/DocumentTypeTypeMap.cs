namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.DocumentTypeTypeMaps")]
    public partial class DocumentTypeTypeMap
    {
        [Key]
        public long DocumentTypeTypeMapIID { get; set; }

        public int? DocumentTypeID { get; set; }

        public int? DocumentTypeMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual DocumentType DocumentType1 { get; set; }
    }
}
