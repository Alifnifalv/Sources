using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class vwProductListCategory_SearchMap : EntityTypeConfiguration<vwProductListCategory_Search>
    {
        public vwProductListCategory_SearchMap()
        {
            // Primary Key
            this.HasKey(t => t.RefProductCategoryProductID);

            // Properties
            this.Property(t => t.RefProductCategoryProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwProductListCategory_Search");
            this.Property(t => t.RefProductCategoryProductID).HasColumnName("RefProductCategoryProductID");
        }
    }
}
