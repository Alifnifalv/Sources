using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoRequestRemarkMap : EntityTypeConfiguration<IntlPoRequestRemark>
    {
        public IntlPoRequestRemarkMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoRequestRemarkID);

            // Properties
            this.Property(t => t.Remarks)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("IntlPoRequestRemark");
            this.Property(t => t.IntlPoRequestRemarkID).HasColumnName("IntlPoRequestRemarkID");
            this.Property(t => t.RefIntlPoRequestID).HasColumnName("RefIntlPoRequestID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasRequired(t => t.IntlPoRequest)
                .WithMany(t => t.IntlPoRequestRemarks)
                .HasForeignKey(d => d.RefIntlPoRequestID);

        }
    }
}
