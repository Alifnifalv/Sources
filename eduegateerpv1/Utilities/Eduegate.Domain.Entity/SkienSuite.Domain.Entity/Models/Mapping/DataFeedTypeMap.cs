using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class DataFeedTypeMap : EntityTypeConfiguration<DataFeedType>
    {
        public DataFeedTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DataFeedTypeID);

            // Properties
            this.Property(t => t.DataFeedTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(250);

            this.Property(t => t.TemplateName)
                .HasMaxLength(250);

            this.Property(t => t.ProcessingSPName)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("DataFeedTypes", "feed");
            this.Property(t => t.DataFeedTypeID).HasColumnName("DataFeedTypeID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.OperationID).HasColumnName("OperationID");
            this.Property(t => t.ProcessingSPName).HasColumnName("ProcessingSPName");

            // Relationships
            this.HasOptional(t => t.DataFeedOperation)
                .WithMany(t => t.DataFeedTypes)
                .HasForeignKey(d => d.OperationID);

        }
    }
}
