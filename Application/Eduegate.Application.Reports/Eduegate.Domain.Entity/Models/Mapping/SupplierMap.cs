using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        public SupplierMap()
        {
            // Primary Key
            this.HasKey(t => t.SupplierIID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.MiddleName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.VendorCR)
                .HasMaxLength(50);

            this.Property(t => t.VendorNickName)
                .HasMaxLength(255);

            this.Property(t => t.CompanyLocation)
                .HasMaxLength(255);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.SupplierCode)
                .HasMaxLength(50);
            
            this.Property(t => t.SupplierAddress)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Suppliers", "mutual");
            this.Property(t => t.SupplierIID).HasColumnName("SupplierIID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.TitleID).HasColumnName("TitleID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.VendorCR).HasColumnName("VendorCR");
            this.Property(t => t.CRExpiry).HasColumnName("CRExpiry");
            this.Property(t => t.VendorNickName).HasColumnName("VendorNickName");
            this.Property(t => t.CompanyLocation).HasColumnName("CompanyLocation");
            this.Property(t => t.StatusID).HasColumnName("StatusID");
            this.Property(t => t.SupplierCode).HasColumnName("SupplierCode");
            this.Property(t => t.SupplierAddress).HasColumnName("SupplierAddress");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.EmployeeID).HasColumnName("EmployeeID");
            this.Property(t => t.IsMarketPlace).HasColumnName("IsMarketPlace");
            this.Property(t => t.BranchID).HasColumnName("BranchID");
            this.Property(t => t.BlockedBranchID).HasColumnName("BlockedBranchID");
            this.Property(t => t.ReturnMethodID).HasColumnName("ReturnMethodID");
            this.Property(t => t.ReceivingMethodID).HasColumnName("ReceivingMethodID");
            this.Property(t => t.SupplierEmail).HasColumnName("SupplierEmail");
            this.Property(t => t.Telephone).HasColumnName("Telephone");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.Branch)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.BlockedBranchID);
            this.HasOptional(t => t.Company)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.CompanyID);
            //this.HasOptional(t => t.Employee)
            //    .WithMany(t => t.Suppliers)
            //    .HasForeignKey(d => d.EmployeeID);
            this.HasOptional(t => t.SupplierStatus)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.StatusID);
            this.HasOptional(t => t.ReceivingMethod)
               .WithMany(t => t.Suppliers)
               .HasForeignKey(d => d.ReceivingMethodID);
            this.HasOptional(t => t.ReturnMethod)
                .WithMany(t => t.Suppliers)
                .HasForeignKey(d => d.ReturnMethodID);

        }
    }
}
