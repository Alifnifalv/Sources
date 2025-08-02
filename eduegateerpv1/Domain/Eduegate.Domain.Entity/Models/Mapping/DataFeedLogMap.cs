using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedLogMap : EntityTypeConfiguration<DataFeedLog>
    {
        public DataFeedLogMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedLogIID);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(250);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DataFeedLogs", "feed");
            this.Property(t => t.DataFeedLogIID).HasColumnName("DataFeedLogIID");
            this.Property(t => t.DataFeedTypeID).HasColumnName("DataFeedTypeID");
            this.Property(t => t.DataFeedStatusID).HasColumnName("DataFeedStatusID");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.DataFeedStatus)
                .WithMany(t => t.DataFeedLogs)
                .HasForeignKey(d => d.DataFeedStatusID);
            this.HasOptional(t => t.DataFeedType)
                .WithMany(t => t.DataFeedLogs)
                .HasForeignKey(d => d.DataFeedTypeID);

        }
    }
}
