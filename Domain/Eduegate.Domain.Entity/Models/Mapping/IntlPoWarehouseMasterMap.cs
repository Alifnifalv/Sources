using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoWarehouseMasterMap : EntityTypeConfiguration<IntlPoWarehouseMaster>
    {
        public IntlPoWarehouseMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoWarehouseMasterID);

            // Properties
            this.Property(t => t.IntlPoWarehouseCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.IntlPoWarehouseName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("IntlPoWarehouseMaster");
            this.Property(t => t.IntlPoWarehouseMasterID).HasColumnName("IntlPoWarehouseMasterID");
            this.Property(t => t.IntlPoWarehouseCode).HasColumnName("IntlPoWarehouseCode");
            this.Property(t => t.IntlPoWarehouseName).HasColumnName("IntlPoWarehouseName");
            this.Property(t => t.IntlPoWarehouseActive).HasColumnName("IntlPoWarehouseActive");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
        }
    }
}
