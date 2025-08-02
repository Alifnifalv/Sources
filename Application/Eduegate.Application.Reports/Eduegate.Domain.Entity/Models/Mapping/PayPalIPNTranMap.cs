using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PayPalIPNTranMap : EntityTypeConfiguration<PayPalIPNTran>
    {
        public PayPalIPNTranMap()
        {
            // Primary Key
            this.HasKey(t => t.RecordID);

            // Properties
            this.Property(t => t.SessionIP)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Response)
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("PayPalIPNTrans");
            this.Property(t => t.RecordID).HasColumnName("RecordID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.SessionIP).HasColumnName("SessionIP");
            this.Property(t => t.Response).HasColumnName("Response");
        }
    }
}
