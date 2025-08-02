using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ScreenLookupMapMap : EntityTypeConfiguration<ScreenLookupMap>
    {
        public ScreenLookupMapMap()
        {
            // Primary Key
            this.HasKey(t => t.ScreenLookupMapID);

            // Properties
            this.Property(t => t.ScreenLookupMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LookUpName)
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .HasMaxLength(1000);

            this.Property(t => t.CallBack)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ScreenLookupMaps", "setting");
            this.Property(t => t.ScreenLookupMapID).HasColumnName("ScreenLookupMapID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.IsOnInit).HasColumnName("IsOnInit");
            this.Property(t => t.LookUpName).HasColumnName("LookUpName");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.CallBack).HasColumnName("CallBack");

            // Relationships
            this.HasOptional(t => t.ScreenMetadata)
                .WithMany(t => t.ScreenLookupMaps)
                .HasForeignKey(d => d.ScreenID);

        }
    }
}
