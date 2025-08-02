using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoRequestQuantityStatuMap : EntityTypeConfiguration<IntlPoRequestQuantityStatu>
    {
        public IntlPoRequestQuantityStatuMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoRequestQuantityStatusID);

            // Properties
            this.Property(t => t.Details)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("IntlPoRequestQuantityStatus");
            this.Property(t => t.IntlPoRequestQuantityStatusID).HasColumnName("IntlPoRequestQuantityStatusID");
            this.Property(t => t.RefIntlPoRequestID).HasColumnName("RefIntlPoRequestID");
            this.Property(t => t.RefIntlPoRequestActionID).HasColumnName("RefIntlPoRequestActionID");
            this.Property(t => t.QtyRequestAction).HasColumnName("QtyRequestAction");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.IntlPoRequest)
                .WithMany(t => t.IntlPoRequestQuantityStatus)
                .HasForeignKey(d => d.RefIntlPoRequestID);
            this.HasRequired(t => t.IntlPoRequestAction)
                .WithMany(t => t.IntlPoRequestQuantityStatus)
                .HasForeignKey(d => d.RefIntlPoRequestActionID);

        }
    }
}
