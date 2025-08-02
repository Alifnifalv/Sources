using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityTypePaymentMethodMapMap : EntityTypeConfiguration<EntityTypePaymentMethodMap>
    {
        public EntityTypePaymentMethodMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityTypePaymentMethodMapIID);

            // Properties
            this.Property(t => t.AccountName)
                .HasMaxLength(50);

            this.Property(t => t.AccountID)
                .HasMaxLength(50);

            this.Property(t => t.BankName)
                .HasMaxLength(50);

            this.Property(t => t.BankBranch)
                .HasMaxLength(50);

            this.Property(t => t.IBANCode)
                .HasMaxLength(50);

            this.Property(t => t.SWIFTCode)
                .HasMaxLength(50);

            this.Property(t => t.IFSCCode)
                .HasMaxLength(50);

            this.Property(t => t.NameOnCheque)
                .HasMaxLength(50);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityTypePaymentMethodMaps", "mutual");
            this.Property(t => t.EntityTypePaymentMethodMapIID).HasColumnName("EntityTypePaymentMethodMapIID");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.PaymentMethodID).HasColumnName("PaymentMethodID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.EntityPropertyID).HasColumnName("EntityPropertyID");
            this.Property(t => t.EntityPropertyTypeID).HasColumnName("EntityPropertyTypeID");
            this.Property(t => t.AccountName).HasColumnName("AccountName");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankBranch).HasColumnName("BankBranch");
            this.Property(t => t.IBANCode).HasColumnName("IBANCode");
            this.Property(t => t.SWIFTCode).HasColumnName("SWIFTCode");
            this.Property(t => t.IFSCCode).HasColumnName("IFSCCode");
            this.Property(t => t.NameOnCheque).HasColumnName("NameOnCheque");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.EntityPropertyType)
                .WithMany(t => t.EntityTypePaymentMethodMaps)
                .HasForeignKey(d => d.EntityPropertyTypeID);
            this.HasOptional(t => t.EntityType)
                .WithMany(t => t.EntityTypePaymentMethodMaps)
                .HasForeignKey(d => d.EntityTypeID);
            this.HasOptional(t => t.PaymentMethod)
                .WithMany(t => t.EntityTypePaymentMethodMaps)
                .HasForeignKey(d => d.PaymentMethodID);
        }
    }
}
