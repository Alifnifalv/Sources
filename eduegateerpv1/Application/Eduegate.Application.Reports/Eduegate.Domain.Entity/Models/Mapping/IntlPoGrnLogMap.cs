using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class IntlPoGrnLogMap : EntityTypeConfiguration<IntlPoGrnLog>
    {
        public IntlPoGrnLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IntlPoGrnLogID);

            // Properties
            this.Property(t => t.GrnStatus)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("IntlPoGrnLog");
            this.Property(t => t.IntlPoGrnLogID).HasColumnName("IntlPoGrnLogID");
            this.Property(t => t.RefIntlPoGrnMasterID).HasColumnName("RefIntlPoGrnMasterID");
            this.Property(t => t.GrnStatus).HasColumnName("GrnStatus");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");

            // Relationships
            this.HasRequired(t => t.IntlPoGrnMaster)
                .WithMany(t => t.IntlPoGrnLogs)
                .HasForeignKey(d => d.RefIntlPoGrnMasterID);

        }
    }
}
