namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("setting.Sites")]
    public partial class Sites1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SiteID { get; set; }

        [StringLength(100)]
        public string SiteName { get; set; }

        public int? Created { get; set; }

        public int? Updated { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
