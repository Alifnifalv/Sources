using System.Data.Entity.ModelConfiguration;


namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentIID);

            // Properties
            this.Property(t => t.Comment1)
                .IsRequired();

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Comments", "mutual");
            this.Property(t => t.CommentIID).HasColumnName("CommentIID");
            this.Property(t => t.ParentCommentID).HasColumnName("ParentCommentID");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Comment1).HasColumnName("Comment");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");

            // Relationships
            this.HasOptional(t => t.Comment2)
                .WithMany(t => t.Comments1)
                .HasForeignKey(d => d.ParentCommentID);
            this.HasRequired(t => t.EntityType)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.EntityTypeID);

        }
    }
}
