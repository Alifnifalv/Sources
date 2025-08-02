using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class FilterColumnUserValueMap : EntityTypeConfiguration<FilterColumnUserValue>
    {
        public FilterColumnUserValueMap()
        {
            // Primary Key
            this.HasKey(t => t.FilterColumnUserValueIID);

            // Properties
            this.Property(t => t.Value1)
                .HasMaxLength(500);

            this.Property(t => t.Value2)
                .HasMaxLength(500);

            this.Property(t => t.Value3)
                .HasMaxLength(500);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("FilterColumnUserValues", "setting");
            this.Property(t => t.FilterColumnUserValueIID).HasColumnName("FilterColumnUserValueIID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.LoginID).HasColumnName("LoginID");
            this.Property(t => t.FilterColumnID).HasColumnName("FilterColumnID");
            this.Property(t => t.ConditionID).HasColumnName("ConditionID");
            this.Property(t => t.Value1).HasColumnName("Value1");
            this.Property(t => t.Value2).HasColumnName("Value2");
            this.Property(t => t.Value3).HasColumnName("Value3");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Login)
                .WithMany(t => t.FilterColumnUserValues)
                .HasForeignKey(d => d.LoginID);
            this.HasOptional(t => t.Condition)
                .WithMany(t => t.FilterColumnUserValues)
                .HasForeignKey(d => d.ConditionID);
            this.HasOptional(t => t.FilterColumn)
                .WithMany(t => t.FilterColumnUserValues)
                .HasForeignKey(d => d.FilterColumnID);
            this.HasOptional(t => t.View)
                .WithMany(t => t.FilterColumnUserValues)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
