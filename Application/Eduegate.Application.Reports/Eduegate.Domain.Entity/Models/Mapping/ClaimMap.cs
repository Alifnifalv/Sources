using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimMap : EntityTypeConfiguration<Claim>
    {
        public ClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimIID);

            // Properties
            this.Property(t => t.ClaimName)
                .HasMaxLength(50);

            this.Property(t => t.ResourceName)
                .HasMaxLength(50);

            this.Property(t => t.Rights)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Claims", "admin");
            this.Property(t => t.ClaimIID).HasColumnName("ClaimIID");
            this.Property(t => t.ClaimName).HasColumnName("ClaimName");
            this.Property(t => t.ResourceName).HasColumnName("ResourceName");
            this.Property(t => t.ClaimTypeID).HasColumnName("ClaimTypeID");
            this.Property(t => t.Rights).HasColumnName("Rights");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.ClaimType)
                .WithMany(t => t.Claims)
                .HasForeignKey(d => d.ClaimTypeID);

        }
    }
}
