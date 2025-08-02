using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class NewsTypeMap : EntityTypeConfiguration<NewsType>
    {
        public NewsTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsTypeID);

            // Properties
            this.Property(t => t.NewsTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NewsTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NewsTypes", "cms");
            this.Property(t => t.NewsTypeID).HasColumnName("NewsTypeID");
            this.Property(t => t.NewsTypeName).HasColumnName("NewsTypeName");
        }
    }
}
