using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserVisitLogMap : EntityTypeConfiguration<UserVisitLog>
    {
        public UserVisitLogMap()
        {
            // Primary Key
            this.HasKey(t => t.UserVisitLogID);

            // Properties
            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PageName)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("UserVisitLog");
            this.Property(t => t.UserVisitLogID).HasColumnName("UserVisitLogID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.RefModuleID).HasColumnName("RefModuleID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.PageName).HasColumnName("PageName");
            this.Property(t => t.VisitStatus).HasColumnName("VisitStatus");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
