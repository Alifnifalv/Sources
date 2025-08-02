using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class LookupMap : EntityTypeConfiguration<Lookup>
    {
        public LookupMap()
        {
            // Primary Key
            this.HasKey(t => t.LookupID);

            // Properties
            this.Property(t => t.LookupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LookupType)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LookupName)
                .HasMaxLength(50);

            this.Property(t => t.Query)
                .HasMaxLength(500);

            this.Property(t => t.Value1)
                .HasMaxLength(500);

            this.Property(t => t.Value2)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Lookups", "setting");
            this.Property(t => t.LookupID).HasColumnName("LookupID");
            this.Property(t => t.LookupType).HasColumnName("LookupType");
            this.Property(t => t.LookupName).HasColumnName("LookupName");
            this.Property(t => t.Query).HasColumnName("Query");
            this.Property(t => t.Value1).HasColumnName("Value1");
            this.Property(t => t.Value2).HasColumnName("Value2");
        }
    }
}
