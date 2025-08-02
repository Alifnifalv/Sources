namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("feed.DataFeedLogs")]
    public partial class DataFeedLog
    {
        [Key]
        public long DataFeedLogIID { get; set; }

        public int? DataFeedTypeID { get; set; }

        public short? DataFeedStatusID { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        [Column(TypeName = "xml")]
        public string DataXML { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public virtual DataFeedStatus DataFeedStatus { get; set; }

        public virtual DataFeedType DataFeedType { get; set; }
    }
}
