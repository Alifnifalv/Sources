using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Comment
    {
        public Comment()
        {
            this.Comments1 = new List<Comment>();
        }

        public long CommentIID { get; set; }
        public Nullable<long> ParentCommentID { get; set; }
        public int EntityTypeID { get; set; }
        public long ReferenceID { get; set; }
        public string Comment1 { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<long> DepartmentID { get; set; }
        public virtual ICollection<Comment> Comments1 { get; set; }
        public virtual Comment Comment2 { get; set; }
        public virtual EntityType EntityType { get; set; }
    }
}
