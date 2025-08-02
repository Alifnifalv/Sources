using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OrderMasterCoutureEmailLogMap : EntityTypeConfiguration<OrderMasterCoutureEmailLog>
    {
        public OrderMasterCoutureEmailLogMap()
        {
            // Primary Key
            this.HasKey(t => t.CoutureEmailLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OrderMasterCoutureEmailLog");
            this.Property(t => t.CoutureEmailLogID).HasColumnName("CoutureEmailLogID");
            this.Property(t => t.RefOrderID).HasColumnName("RefOrderID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
