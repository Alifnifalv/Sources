using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ProductDigitalReturnMap : EntityTypeConfiguration<ProductDigitalReturn>
    {
        public ProductDigitalReturnMap()
        {
            // Primary Key
            this.HasKey(t => t.ReturnID);

            // Properties
            this.Property(t => t.ReturnReason)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.RMAVoucherNo)
                .HasMaxLength(20);

            this.Property(t => t.Remarks)
                .HasMaxLength(300);

            this.Property(t => t.ProvidedCode1)
                .HasMaxLength(20);

            this.Property(t => t.ProvidedCode2)
                .HasMaxLength(20);

            this.Property(t => t.ProvidedCode3)
                .HasMaxLength(20);

            this.Property(t => t.ProvidedCodeRef1)
                .HasMaxLength(20);

            this.Property(t => t.ProvidedCodeRef2)
                .HasMaxLength(20);

            this.Property(t => t.ProvidedCodeRef3)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProductDigitalReturn");
            this.Property(t => t.ReturnID).HasColumnName("ReturnID");
            this.Property(t => t.RefProductDigitalID).HasColumnName("RefProductDigitalID");
            this.Property(t => t.ReturnReason).HasColumnName("ReturnReason");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.RMAVoucherNo).HasColumnName("RMAVoucherNo");
            this.Property(t => t.RMAUserID).HasColumnName("RMAUserID");
            this.Property(t => t.RMAUpdatedOn).HasColumnName("RMAUpdatedOn");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.ProvidedCode1).HasColumnName("ProvidedCode1");
            this.Property(t => t.ProvidedCode2).HasColumnName("ProvidedCode2");
            this.Property(t => t.ProvidedCode3).HasColumnName("ProvidedCode3");
            this.Property(t => t.ProvidedCodeRef1).HasColumnName("ProvidedCodeRef1");
            this.Property(t => t.ProvidedCodeRef2).HasColumnName("ProvidedCodeRef2");
            this.Property(t => t.ProvidedCodeRef3).HasColumnName("ProvidedCodeRef3");

            // Relationships
            this.HasRequired(t => t.ProductDigital)
                .WithMany(t => t.ProductDigitalReturns)
                .HasForeignKey(d => d.RefProductDigitalID);

        }
    }
}
