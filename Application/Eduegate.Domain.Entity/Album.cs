namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("collaboration.Albums")]
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            AlbumImageMaps = new HashSet<AlbumImageMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlbumID { get; set; }

        [StringLength(500)]
        public string AlbumName { get; set; }

        public byte? AlbumTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlbumImageMap> AlbumImageMaps { get; set; }

        public virtual AlbumType AlbumType { get; set; }
    }
}
