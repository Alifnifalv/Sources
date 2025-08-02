using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CountryGroupMap : EntityTypeConfiguration<CountryGroup>
    {
        public CountryGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupID);

            // Properties
            this.Property(t => t.GroupType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.GroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CountryGroup");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupType).HasColumnName("GroupType");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.MinWeight).HasColumnName("MinWeight");
            this.Property(t => t.MinAmount).HasColumnName("MinAmount");
            this.Property(t => t.AfterMinWeight).HasColumnName("AfterMinWeight");
            this.Property(t => t.AfterMinAmount).HasColumnName("AfterMinAmount");
        }
    }
}
