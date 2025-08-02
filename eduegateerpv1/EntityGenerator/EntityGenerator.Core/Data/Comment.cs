using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Comments", Schema = "mutual")]
    public partial class Comment
    {
        public Comment()
        {
            InverseParentComment = new HashSet<Comment>();
        }

        [Key]
        public long CommentIID { get; set; }
        public long? ParentCommentID { get; set; }
        public int EntityTypeID { get; set; }
        public long ReferenceID { get; set; }
        [Required]
        [Column("Comment")]
        [Unicode(false)]
        public string Comment1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? DepartmentID { get; set; }

        [ForeignKey("EntityTypeID")]
        [InverseProperty("Comments")]
        public virtual EntityType EntityType { get; set; }
        [ForeignKey("ParentCommentID")]
        [InverseProperty("InverseParentComment")]
        public virtual Comment ParentComment { get; set; }
        [InverseProperty("ParentComment")]
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
