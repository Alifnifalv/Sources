using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SyncFieldMapMap : EntityTypeConfiguration<SyncFieldMap>
    {
        public SyncFieldMapMap()
        {
            // Primary Key
            this.HasKey(t => t.SyncFieldMapID);

            // Properties
            this.Property(t => t.SyncFieldMapID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SourceField)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DestinationField)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SyncFieldMaps", "sync");
            this.Property(t => t.SyncFieldMapID).HasColumnName("SyncFieldMapID");
            this.Property(t => t.SynchFieldMapTypeID).HasColumnName("SynchFieldMapTypeID");
            this.Property(t => t.SourceField).HasColumnName("SourceField");
            this.Property(t => t.DestinationField).HasColumnName("DestinationField");

            // Relationships
            this.HasOptional(t => t.SyncFieldMapType)
                .WithMany(t => t.SyncFieldMaps)
                .HasForeignKey(d => d.SynchFieldMapTypeID);

        }
    }
}
