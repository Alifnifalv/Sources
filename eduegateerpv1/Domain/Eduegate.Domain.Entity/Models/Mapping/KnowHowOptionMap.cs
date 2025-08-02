using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class KnowHowOptionMap : EntityTypeConfiguration<KnowHowOption>
    {
        public KnowHowOptionMap()
        {
            // Primary Key
            this.HasKey(t => t.KnowHowOptionIID);

            // Properties
            this.Property(t => t.KnowHowOptionText)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("KnowHowOption", "cms");
            this.Property(t => t.KnowHowOptionIID).HasColumnName("KnowHowOptionIID");
            this.Property(t => t.KnowHowOptionText).HasColumnName("KnowHowOptionText");
            this.Property(t => t.IsEditable).HasColumnName("IsEditable");
        }
    }
}
