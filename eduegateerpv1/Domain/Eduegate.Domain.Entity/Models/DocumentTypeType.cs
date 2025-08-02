using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DocumentTypeTypeMaps", Schema = "mutual")]
    public partial class DocumentTypeType
    {
        public DocumentTypeType() 
        {

        }

        [Key]
        public long DocumentTypeTypeMapIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; } 
        public Nullable<int> DocumentTypeMapID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual DocumentType DocumentTypeType2 { get; set; }

    }
}
