using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSetLoginMapMap : EntityTypeConfiguration<ClaimSetLoginMap>
    {
        public ClaimSetLoginMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimSetLoginMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClaimSetLoginMaps", "admin");
            this.Property(t => t.ClaimSetLoginMapIID).HasColumnName("ClaimSetLoginMapIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.ClaimSetID).HasColumnName("ClaimSetID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.ClaimSet)
                .WithMany(t => t.ClaimSetLoginMaps)
                .HasForeignKey(d => d.ClaimSetID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.ClaimSetLoginMaps)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Login)
                .WithMany(t => t.ClaimSetLoginMaps)
                .HasForeignKey(d => d.LoginID);

        }
    }
}
