using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AreaMasterMap : EntityTypeConfiguration<AreaMaster>
    {
        public AreaMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.AreaID);

            // Properties
            this.Property(t => t.AreaNameEn)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.AreaNameAr)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("AreaMaster");
            this.Property(t => t.AreaID).HasColumnName("AreaID");
            this.Property(t => t.RefCountryID).HasColumnName("RefCountryID");
            this.Property(t => t.AreaNameEn).HasColumnName("AreaNameEn");
            this.Property(t => t.AreaNameAr).HasColumnName("AreaNameAr");
            this.Property(t => t.RouteID).HasColumnName("RouteID");
            this.Property(t => t.Active).HasColumnName("Active");
        }
    }
}
