using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SyncFieldMapTypeMap : EntityTypeConfiguration<SyncFieldMapType>
    {
        public SyncFieldMapTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SynchFieldMapTypeID);

            // Properties
            this.Property(t => t.SynchFieldMapTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MapName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SyncFieldMapTypes", "sync");
            this.Property(t => t.SynchFieldMapTypeID).HasColumnName("SynchFieldMapTypeID");
            this.Property(t => t.MapName).HasColumnName("MapName");
        }
    }
}
