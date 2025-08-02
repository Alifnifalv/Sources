using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BlinkLocationMasterMap : EntityTypeConfiguration<BlinkLocationMaster>
    {
        public BlinkLocationMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.BlinkLocationMasterID);

            // Properties
            this.Property(t => t.BlinkLocationMasterCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.BlinkLocationMasterName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BlinkLocationMaster");
            this.Property(t => t.BlinkLocationMasterID).HasColumnName("BlinkLocationMasterID");
            this.Property(t => t.BlinkLocationMasterCode).HasColumnName("BlinkLocationMasterCode");
            this.Property(t => t.BlinkLocationMasterName).HasColumnName("BlinkLocationMasterName");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.BlinkLocationMasterActive).HasColumnName("BlinkLocationMasterActive");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.CountryMaster)
                .WithMany(t => t.BlinkLocationMasters)
                .HasForeignKey(d => d.RefCountryID);

        }
    }
}
