using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoGrnMasterMap : EntityTypeConfiguration<IntlPoGrnMaster>
    {
        public IntlPoGrnMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoGrnMasterID);

            // Properties
            this.Property(t => t.GrnStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("IntlPoGrnMaster");
            this.Property(t => t.IntlPoGrnMasterID).HasColumnName("IntlPoGrnMasterID");
            this.Property(t => t.RefIntlPoWarehouseMasterID).HasColumnName("RefIntlPoWarehouseMasterID");
            this.Property(t => t.GrnDate).HasColumnName("GrnDate");
            this.Property(t => t.GrnStatus).HasColumnName("GrnStatus");
            this.Property(t => t.Remarks).HasColumnName("Remarks");

            // Relationships
            this.HasRequired(t => t.IntlPoWarehouseMaster)
                .WithMany(t => t.IntlPoGrnMasters)
                .HasForeignKey(d => d.RefIntlPoWarehouseMasterID);

        }
    }
}
