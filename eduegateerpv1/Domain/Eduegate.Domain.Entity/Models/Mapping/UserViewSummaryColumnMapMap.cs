using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserViewSummaryColumnMapMap : EntityTypeConfiguration<UserViewSummaryColumnMap>
    {
        public UserViewSummaryColumnMapMap()
        {
            // Primary Key
            this.HasKey(t => t.UserViewSummaryColumnMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserViewSummaryColumnMaps", "setting");
            this.Property(t => t.UserViewSummaryColumnMapIID).HasColumnName("UserViewSummaryColumnMapIID");
            this.Property(t => t.UserViewID).HasColumnName("UserViewID");
            this.Property(t => t.ViewSummaryColumnID).HasColumnName("ViewSummaryColumnID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.UserView)
                .WithMany(t => t.UserViewSummaryColumnMaps)
                .HasForeignKey(d => d.UserViewID);
        }
    }
}
