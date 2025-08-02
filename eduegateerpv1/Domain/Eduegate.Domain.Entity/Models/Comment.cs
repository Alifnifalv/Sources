using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Comments", Schema = "mutual")]
    public partial class Comment
    {
        public Comment()
        {
            Comments1 = new HashSet<Comment>();
        }

        [Key]
        public long CommentIID { get; set; }

        public long? ParentCommentID { get; set; }

        public int EntityTypeID { get; set; }

        public long ReferenceID { get; set; }

        [Required]
        [Column("Comment")]
        public string Comment1 { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public long? DepartmentID { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Comments1 { get; set; }

        public long? FromLoginID { get; set; }

        public long? ToLoginID { get; set; }

        [ForeignKey("FromLoginID")]
        public virtual Employee FromEmployee { get; set; }  // Navigation property for the teacher who sent the message

        public bool IsRead { get; set; }



    }
}
