using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.Mapping
{
    public class EntityPropertyMapMap : EntityTypeConfiguration<Eduegate.Domain.Entity.Models.EntityPropertyMap>
    {
        public EntityPropertyMapMap()
        {
            // Primary Key
            this.HasKey(t => t.EntityPropertyMapIID);

            // Properties
            this.Property(t => t.Value1)
                .HasMaxLength(250);

            this.Property(t => t.Value2)
                .HasMaxLength(250);

            this.Property(t => t.TimeStamps)
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EntityPropertyMaps", "mutual");
            this.Property(t => t.EntityPropertyMapIID).HasColumnName("EntityPropertyMapIID");
            this.Property(t => t.EntityTypeID).HasColumnName("EntityTypeID");
            this.Property(t => t.EntityPropertyTypeID).HasColumnName("EntityPropertyTypeID");
            this.Property(t => t.EntityPropertyID).HasColumnName("EntityPropertyID");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Sequence).HasColumnName("Sequence");
            this.Property(t => t.Value1).HasColumnName("Value1");
            this.Property(t => t.Value2).HasColumnName("Value2");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.TimeStamps).HasColumnName("TimeStamps");
        }
    }
}
