using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimSetSearchViewMap : EntityTypeConfiguration<ClaimSetSearchView>
    {
        public ClaimSetSearchViewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ClaimSetIID, t.NoOfClaimSets, t.NoOfClaims });

            // Properties
            this.Property(t => t.ClaimSetIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClaimSetName)
                .HasMaxLength(50);

            this.Property(t => t.NoOfClaimSets)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NoOfClaims)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ClaimSetSearchView", "admin");
            this.Property(t => t.ClaimSetIID).HasColumnName("ClaimSetIID");
            this.Property(t => t.ClaimSetName).HasColumnName("ClaimSetName");
            this.Property(t => t.NoOfClaimSets).HasColumnName("NoOfClaimSets");
            this.Property(t => t.NoOfClaims).HasColumnName("NoOfClaims");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
        }
    }
}
