using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimLoginMapMap : EntityTypeConfiguration<ClaimLoginMap>
    {
        public ClaimLoginMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimLoginMapIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClaimLoginMaps", "admin");
            this.Property(t => t.ClaimLoginMapIID).HasColumnName("ClaimLoginMapIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.ClaimID).HasColumnName("ClaimID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Claim)
                .WithMany(t => t.ClaimLoginMaps)
                .HasForeignKey(d => d.ClaimID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.ClaimLoginMaps)
                .HasForeignKey(d => d.CompanyID);
            this.HasOptional(t => t.Login)
                .WithMany(t => t.ClaimLoginMaps)
                .HasForeignKey(d => d.LoginID);

        }
    }
}
