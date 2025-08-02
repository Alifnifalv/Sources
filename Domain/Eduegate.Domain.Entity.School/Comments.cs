using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Eduegate.Domain.Entity.School.Models.School

{
    [Table("Comments", Schema = "mutual")]
    public partial class Comment
    {
        public Comment()
        {
            Comments1 = new HashSet<Comment>();
            Comments = new HashSet<Comment>();
            InverseParentComment = new HashSet<Comment>();
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

        public byte[] TimeStamps { get; set; }  // Uncommented

        public long? DepartmentID { get; set; }

        //public virtual EntityType EntityType { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> Comments1 { get; set; }

        public long? FromLoginID { get; set; }

        public long? ToLoginID { get; set; }

        [ForeignKey("FromLoginID")]
        public virtual Employee FromEmployee { get; set; }  // Navigation property for the teacher who sent the message

        public bool IsRead { get; set; }


        public long? BroadcastID { get; set; }

        public long? PhotoContentID { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Comment> InverseParentComment { get; set; }  // Added this
    }

}
