using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class CountryMasterMap : EntityTypeConfiguration<CountryMaster>
    {
        public CountryMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryID);

            // Properties
            this.Property(t => t.CountryCode)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.CountryNameEn)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.CountryNameAr)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.BaseCurrency)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("CountryMaster");
            this.Property(t => t.CountryID).HasColumnName("CountryID");
            this.Property(t => t.RefGroupID).HasColumnName("RefGroupID");
            this.Property(t => t.CountryCode).HasColumnName("CountryCode");
            this.Property(t => t.CountryNameEn).HasColumnName("CountryNameEn");
            this.Property(t => t.CountryNameAr).HasColumnName("CountryNameAr");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Operation).HasColumnName("Operation");
            this.Property(t => t.BaseCurrency).HasColumnName("BaseCurrency");
            this.Property(t => t.ConversionRate).HasColumnName("ConversionRate");
            this.Property(t => t.ConversionRateSAR).HasColumnName("ConversionRateSAR");
            this.Property(t => t.NoofDecimals).HasColumnName("NoofDecimals");
            this.Property(t => t.DataFeedDateTime).HasColumnName("DataFeedDateTime");

            // Relationships
            this.HasRequired(t => t.CountryGroup)
                .WithMany(t => t.CountryMasters)
                .HasForeignKey(d => d.RefGroupID);

        }
    }
}
