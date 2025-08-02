using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ClaimTypeMap : EntityTypeConfiguration<ClaimType>
    {
        public ClaimTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimTypeID);

            // Properties
            this.Property(t => t.ClaimTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClaimTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ClaimTypes", "admin");
            this.Property(t => t.ClaimTypeID).HasColumnName("ClaimTypeID");
            this.Property(t => t.ClaimTypeName).HasColumnName("ClaimTypeName");
        }
    }
}
