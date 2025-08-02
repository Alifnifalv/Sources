using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class AttachmentMap : EntityTypeConfiguration<Attachment>
    {
        public AttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AttachmentIID);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Attachments", "mutual");
            this.Property(t => t.AttachmentIID).HasColumnName("AttachmentIID");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.AttachmentName).HasColumnName("AttachmentName");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");

            // Relationships
            //this.HasRequired(t => t.EntityType)
            //    .WithMany(t => t.Comments)
            //    .HasForeignKey(d => d.EntityTypeID);

        }
    }
}
