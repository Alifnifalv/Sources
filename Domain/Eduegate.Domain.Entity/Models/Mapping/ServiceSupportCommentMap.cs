using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ServiceSupportCommentMap : EntityTypeConfiguration<ServiceSupportComment>
    {
        public ServiceSupportCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.CommentID);

            // Properties
            this.Property(t => t.Comment)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ServiceSupportComment");
            this.Property(t => t.CommentID).HasColumnName("CommentID");
            this.Property(t => t.RefTicketID).HasColumnName("RefTicketID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
