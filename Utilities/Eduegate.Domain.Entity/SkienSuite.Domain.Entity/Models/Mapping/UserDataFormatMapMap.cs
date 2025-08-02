using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class UserDataFormatMapMap : EntityTypeConfiguration<UserDataFormatMap>
    {
        public UserDataFormatMapMap()
        {
            // Primary Key
            this.HasKey(t => t.UserDataFormatIID);

            // Properties
            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UserDataFormatMaps", "setting");
            this.Property(t => t.UserDataFormatIID).HasColumnName("UserDataFormatIID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.DataFormatTypeID).HasColumnName("DataFormatTypeID");
            this.Property(t => t.DataFormatID).HasColumnName("DataFormatID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.UserDataFormatMaps)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.DataFormat)
                .WithMany(t => t.UserDataFormatMaps)
                .HasForeignKey(d => d.DataFormatID);
            this.HasOptional(t => t.DataFormatType)
                .WithMany(t => t.UserDataFormatMaps)
                .HasForeignKey(d => d.DataFormatTypeID);

        }
    }
}
