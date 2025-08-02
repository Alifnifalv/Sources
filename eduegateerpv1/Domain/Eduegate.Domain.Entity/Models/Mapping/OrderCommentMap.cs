using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderCommentMap : EntityTypeConfiguration<OrderComment>
    {
        public OrderCommentMap()
        {
            // Primary Key
            this.HasKey(t => t.OrderCommentID);

            // Properties
            this.Property(t => t.OrderComment1)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("OrderComment");
            this.Property(t => t.OrderCommentID).HasColumnName("OrderCommentID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.OrderComment1).HasColumnName("OrderComment");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
