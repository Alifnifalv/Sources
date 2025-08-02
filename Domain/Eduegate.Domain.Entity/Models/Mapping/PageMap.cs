using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            // Primary Key
            this.HasKey(t => t.PageID);

            // Properties
            this.Property(t => t.PageID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageName)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(100);

            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            this.Property(t => t.PlaceHolder)
                .HasMaxLength(50);

            //this.Property(t => t.TimeStamps)
            //    .IsFixedLength()
            //    .HasMaxLength(8)
            //    .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Pages", "cms");
            this.Property(t => t.PageID).HasColumnName("PageID");
            this.Property(t => t.SiteID).HasColumnName("SiteID");
            this.Property(t => t.PageName).HasColumnName("PageName");
            this.Property(t => t.PageTypeID).HasColumnName("PageTypeID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.PlaceHolder).HasColumnName("PlaceHolder");
            this.Property(t => t.ParentPageID).HasColumnName("ParentPageID");
            this.Property(t => t.MasterPageID).HasColumnName("MasterPageID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.IsCache).HasColumnName("IsCache");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");

            // Relationships
            this.HasOptional(t => t.Page1)
                .WithMany(t => t.Pages1)
                .HasForeignKey(d => d.ParentPageID);
            this.HasOptional(t => t.PageType)
                .WithMany(t => t.Pages)
                .HasForeignKey(d => d.PageTypeID);
            this.HasOptional(t => t.Site)
                .WithMany(t => t.Pages)
                .HasForeignKey(d => d.SiteID);
        }
    }
}
