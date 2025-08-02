using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageTypeMap : EntityTypeConfiguration<PageType>
    {
        public PageTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PageTypeID);

            // Properties
            this.Property(t => t.TypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PageTypes", "cms");
            this.Property(t => t.PageTypeID).HasColumnName("PageTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
        }
    }
}
