using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserAccessMap : EntityTypeConfiguration<UserAccess>
    {
        public UserAccessMap()
        {
            // Primary Key
            this.HasKey(t => t.UserAccessID);

            // Properties
            this.Property(t => t.AccessValues)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserAccess");
            this.Property(t => t.UserAccessID).HasColumnName("UserAccessID");
            this.Property(t => t.RefUserID).HasColumnName("RefUserID");
            this.Property(t => t.AccessValues).HasColumnName("AccessValues");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
