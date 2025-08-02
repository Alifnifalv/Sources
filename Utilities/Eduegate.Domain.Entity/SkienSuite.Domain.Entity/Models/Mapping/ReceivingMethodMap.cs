using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ReceivingMethodMap : EntityTypeConfiguration<ReceivingMethod>
    {
        public ReceivingMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.ReceivingMethodID);

            // Properties
            this.Property(t => t.ReceivingMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ReceivingMethodName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ReceivingMethods", "distribution");
            this.Property(t => t.ReceivingMethodID).HasColumnName("ReceivingMethodID");
            this.Property(t => t.ReceivingMethodName).HasColumnName("ReceivingMethodName");
        }
    }
}
