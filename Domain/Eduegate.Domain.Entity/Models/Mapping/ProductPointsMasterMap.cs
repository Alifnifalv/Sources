using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductPointsMasterMap : EntityTypeConfiguration<ProductPointsMaster>
    {
        public ProductPointsMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductPointsMasterID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ProductPointsMaster");
            this.Property(t => t.ProductPointsMasterID).HasColumnName("ProductPointsMasterID");
            this.Property(t => t.RefProductID).HasColumnName("RefProductID");
            this.Property(t => t.Points).HasColumnName("Points");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            //this.HasRequired(t => t.ProductMaster)
            //    .WithMany(t => t.ProductPointsMasters)
            //    .HasForeignKey(d => d.RefProductID);

        }
    }
}
