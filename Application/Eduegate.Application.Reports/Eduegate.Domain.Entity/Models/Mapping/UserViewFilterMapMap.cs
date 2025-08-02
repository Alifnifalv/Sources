using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserViewFilterMapMap : EntityTypeConfiguration<UserViewFilterMap>
    {
        public UserViewFilterMapMap()
        {
            // Primary Key
            this.HasKey(t => t.UserViewFilterMapIID);

            // Properties
            this.Property(t => t.Value1)
                .HasMaxLength(50);

            this.Property(t => t.Value2)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserViewFilterMaps", "setting");
            this.Property(t => t.UserViewFilterMapIID).HasColumnName("UserViewFilterMapIID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ViewFilterID).HasColumnName("ViewFilterID");
            this.Property(t => t.Value1).HasColumnName("Value1");
            this.Property(t => t.Value2).HasColumnName("Value2");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.UserViewFilterMaps)
                .HasForeignKey(d => d.UserID);
            this.HasOptional(t => t.ViewFilter)
                .WithMany(t => t.UserViewFilterMaps)
                .HasForeignKey(d => d.ViewFilterID);

        }
    }
}
