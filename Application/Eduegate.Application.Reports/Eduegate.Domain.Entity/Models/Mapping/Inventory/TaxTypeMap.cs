using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class TaxTypeMap : EntityTypeConfiguration<TaxType>
    {
        public TaxTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxTypeID);

            // Properties
            this.Property(t => t.TaxTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TaxTypes", "inventory");
            this.Property(t => t.TaxTypeID).HasColumnName("TaxTypeID");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
