using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models.Mapping
{
    public class DepartmentTagMap : EntityTypeConfiguration<DepartmentTag>
    {
        public DepartmentTagMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentTagIID);

            // Properties
            // Table & Column Mappings
            this.ToTable("DepartmentTags", "hr");
            this.Property(t => t.DepartmentTagIID).HasColumnName("DepartmentTagIID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.DepartmentTags)
                .HasForeignKey(d => d.DepartmentID);
        }
    }
}
