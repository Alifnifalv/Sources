using System;
using System.Data.Entity.ModelConfiguration;


namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class ActionLinkTypeMap : EntityTypeConfiguration<ActionLinkType>
    {
        public ActionLinkTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ActionLinkTypeID);

            // Properties
            this.Property(t => t.ActionLinkTypeName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ActionLinkTypes", "mutual");
            this.Property(t => t.ActionLinkTypeID).HasColumnName("ActionLinkTypeID");
            this.Property(t => t.ActionLinkTypeName).HasColumnName("ActionLinkTypeName");
        }
    }
}
