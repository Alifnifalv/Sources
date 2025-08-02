using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Mutual
{
    [Table("VisitorAttachmentMaps", Schema = "mutual")]
    public partial class VisitorAttachmentMap
    {
        [Key]
        public long VisitorAttachmentMapIID { get; set; }

        public long? VisitorID { get; set; }

        public long? QIDFrontAttachmentID { get; set; }

        public long? QIDBackAttachmentID { get; set; }

        public long? PassportFrontAttachmentID { get; set; }

        public long? PassportBackAttachmentID { get; set; }

        public long? VisitorProfileID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Visitor Visitor { get; set; }
    }
}