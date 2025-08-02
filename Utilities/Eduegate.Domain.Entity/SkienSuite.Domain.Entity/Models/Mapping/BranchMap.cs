using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.BranchIID);

            // Properties
            this.Property(t => t.BranchName)
                .HasMaxLength(255);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Logo)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Branches", "mutual");
            this.Property(t => t.BranchIID).HasColumnName("BranchIID");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.BranchGroupID).HasColumnName("BranchGroupID");
            this.Property(t => t.WarehouseID).HasColumnName("WarehouseID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.IsMarketPlace).HasColumnName("IsMarketPlace");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.BranchGroup)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.BranchGroupID);
            this.HasOptional(t => t.BranchStatus)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.Warehouse)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.WarehouseID);

        }
    }
}
