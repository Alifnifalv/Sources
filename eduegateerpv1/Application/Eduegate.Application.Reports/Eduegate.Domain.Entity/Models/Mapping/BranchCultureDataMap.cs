using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class BranchCultureDataMap : EntityTypeConfiguration<BranchCultureData>
    {
        public BranchCultureDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CultureID, t.BranchID });

            // Properties
            this.Property(t => t.BranchID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BranchName)
                .HasMaxLength(200);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("BranchCultureDatas", "mutual");
            this.Property(t => t.CultureID).HasColumnName("CultureID");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.BranchCultureDatas)
                .HasForeignKey(d => d.BranchID);
            this.HasRequired(t => t.Culture)
                .WithMany(t => t.BranchCultureDatas)
                .HasForeignKey(d => d.CultureID);

        }
    }
}
