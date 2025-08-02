using Eduegate.Domain.Entity.Models.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping.Inventory
{
    public class TaxTemplateMap : EntityTypeConfiguration<TaxTemplate>
    {
        public TaxTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.TaxTemplateID);

            // Properties
            this.Property(t => t.TaxTemplateID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TaxTemplates", "inventory");
            this.Property(t => t.TaxTemplateID).HasColumnName("TaxTemplateID");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.HasTaxInclusive).HasColumnName("HasTaxInclusive");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
