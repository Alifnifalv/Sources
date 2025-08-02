using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class TicketTagMap : EntityTypeConfiguration<TicketTag>
    {
        public TicketTagMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketTagsID);

            // Properties
            this.Property(t => t.TicketTagsID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TagName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TicketTags", "cs");
            this.Property(t => t.TicketTagsID).HasColumnName("TicketTagsID");
            this.Property(t => t.TagName).HasColumnName("TagName");
        }
    }
}
