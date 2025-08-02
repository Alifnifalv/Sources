using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DataFeedLogs", Schema = "feed")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("DataFeedStatusID")]
        [InverseProperty("DataFeedLogs")]
        public virtual DataFeedStatus DataFeedStatus { get; set; }
        [ForeignKey("DataFeedTypeID")]
        [InverseProperty("DataFeedLogs")]
        public virtual DataFeedType DataFeedType { get; set; }
    }
}
