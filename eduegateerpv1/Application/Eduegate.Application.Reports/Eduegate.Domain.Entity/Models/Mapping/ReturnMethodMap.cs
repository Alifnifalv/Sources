using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ReturnMethodMap : EntityTypeConfiguration<ReturnMethod>
    {
        public ReturnMethodMap()
        {
            // Primary Key
            this.HasKey(t => t.ReturnMethodID);

            // Properties
            this.Property(t => t.ReturnMethodID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ReturnMethodName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ReturnMethods", "distribution");
            this.Property(t => t.ReturnMethodID).HasColumnName("ReturnMethodID");
            this.Property(t => t.ReturnMethodName).HasColumnName("ReturnMethodName");
        }
    }
}
