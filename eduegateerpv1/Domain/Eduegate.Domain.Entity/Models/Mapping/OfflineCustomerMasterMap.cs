using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class OfflineCustomerMasterMap : EntityTypeConfiguration<OfflineCustomerMaster>
    {
        public OfflineCustomerMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerName);

            // Properties
            this.Property(t => t.OfflineCustomerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(200);

            this.Property(t => t.Block)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Street)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BuildingNo)
                .HasMaxLength(50);

            this.Property(t => t.Floor)
                .HasMaxLength(50);

            this.Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(14);

            // Table & Column Mappings
            this.ToTable("OfflineCustomerMaster");
            this.Property(t => t.OfflineCustomerID).HasColumnName("OfflineCustomerID");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.RefAreaID).HasColumnName("RefAreaID");
            this.Property(t => t.Block).HasColumnName("Block");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.BuildingNo).HasColumnName("BuildingNo");
            this.Property(t => t.Floor).HasColumnName("Floor");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedByID).HasColumnName("CreatedByID");
            this.Property(t => t.CreatedDatetime).HasColumnName("CreatedDatetime");
            this.Property(t => t.UpdatedByID).HasColumnName("UpdatedByID");
            this.Property(t => t.UpdatedDateTime).HasColumnName("UpdatedDateTime");
        }
    }
}
