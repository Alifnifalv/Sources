using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchViewMap : EntityTypeConfiguration<BranchView>
    {
        public BranchViewMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchIID);

            // Properties
            this.Property(t => t.BranchIID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BranchName)
                .HasMaxLength(255);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            this.Property(t => t.Supplier)
                .HasMaxLength(767);

            this.Property(t => t.RowCategory)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("BranchView", "mutual");
            this.Property(t => t.BranchIID).HasColumnName("BranchIID");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.IsMarketPlace).HasColumnName("IsMarketPlace");
            this.Property(t => t.StatusName).HasColumnName("StatusName");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.supplieriid).HasColumnName("supplieriid");
            this.Property(t => t.Supplier).HasColumnName("Supplier");
            this.Property(t => t.RowCategory).HasColumnName("RowCategory");
        }
    }
}
