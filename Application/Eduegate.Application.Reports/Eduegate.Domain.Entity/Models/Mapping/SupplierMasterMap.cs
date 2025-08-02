using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierMasterMap : EntityTypeConfiguration<SupplierMaster>
    {
        public SupplierMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierID);

            // Properties
            this.Property(t => t.SupplierCode)
                .HasMaxLength(10);

            this.Property(t => t.SupplierName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SupplierAddress)
                .HasMaxLength(500);

            this.Property(t => t.ContactPerson)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ContactEmail1)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ContactEmail2)
                .HasMaxLength(100);

            this.Property(t => t.ContactPhone1)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ContactPhone2)
                .HasMaxLength(50);

            this.Property(t => t.Website)
                .HasMaxLength(150);

            this.Property(t => t.ContactEmail3)
                .HasMaxLength(100);

            this.Property(t => t.BankName)
                .HasMaxLength(100);

            this.Property(t => t.BranchName)
                .HasMaxLength(100);

            this.Property(t => t.SwiftCode)
                .HasMaxLength(10);

            this.Property(t => t.AccountTitle)
                .HasMaxLength(50);

            this.Property(t => t.ChequeName)
                .HasMaxLength(50);

            this.Property(t => t.SupplierType)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.ContactEmail4)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SupplierMaster");
            this.Property(t => t.SupplierID).HasColumnName("SupplierID");
            this.Property(t => t.SupplierCode).HasColumnName("SupplierCode");
            this.Property(t => t.SupplierName).HasColumnName("SupplierName");
            this.Property(t => t.SupplierActive).HasColumnName("SupplierActive");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.SupplierAddress).HasColumnName("SupplierAddress");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.ContactEmail1).HasColumnName("ContactEmail1");
            this.Property(t => t.ContactEmail2).HasColumnName("ContactEmail2");
            this.Property(t => t.ContactPhone1).HasColumnName("ContactPhone1");
            this.Property(t => t.ContactPhone2).HasColumnName("ContactPhone2");
            this.Property(t => t.RefAreaID).HasColumnName("RefAreaID");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.ProductManagerID).HasColumnName("ProductManagerID");
            this.Property(t => t.ContactEmail3).HasColumnName("ContactEmail3");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.SwiftCode).HasColumnName("SwiftCode");
            this.Property(t => t.AccountNo).HasColumnName("AccountNo");
            this.Property(t => t.AccountTitle).HasColumnName("AccountTitle");
            this.Property(t => t.ChequeName).HasColumnName("ChequeName");
            this.Property(t => t.SupplierType).HasColumnName("SupplierType");
            this.Property(t => t.ContactEmail4).HasColumnName("ContactEmail4");
        }
    }
}
