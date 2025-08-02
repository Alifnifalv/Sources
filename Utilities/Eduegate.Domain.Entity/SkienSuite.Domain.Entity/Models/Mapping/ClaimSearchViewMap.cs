using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSearchViewMap : EntityTypeConfiguration<ClaimSearchView>
    {
        public ClaimSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimIID);

            // Properties
            this.Property(t => t.ClaimIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClaimName)
                .HasMaxLength(50);

            this.Property(t => t.ResourceName)
                .HasMaxLength(50);

            this.Property(t => t.ClaimTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ClaimSearchView", "admin");
            this.Property(t => t.ClaimIID).HasColumnName("ClaimIID");
            this.Property(t => t.ClaimName).HasColumnName("ClaimName");
            this.Property(t => t.ResourceName).HasColumnName("ResourceName");
            this.Property(t => t.ClaimTypeName).HasColumnName("ClaimTypeName");
            this.Property(t => t.companyid).HasColumnName("companyid");
        }
    }
}
