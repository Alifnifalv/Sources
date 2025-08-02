using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoRequestStatusLogMap : EntityTypeConfiguration<IntlPoRequestStatusLog>
    {
        public IntlPoRequestStatusLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoRequestStatusLogID);

            // Properties
            this.Property(t => t.RequestStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoRequestStatusLog");
            this.Property(t => t.IntlPoRequestStatusLogID).HasColumnName("IntlPoRequestStatusLogID");
            this.Property(t => t.RefIntlPoRequestID).HasColumnName("RefIntlPoRequestID");
            this.Property(t => t.RequestStatus).HasColumnName("RequestStatus");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.IntlPoRequest)
                .WithMany(t => t.IntlPoRequestStatusLogs)
                .HasForeignKey(d => d.RefIntlPoRequestID);

        }
    }
}
