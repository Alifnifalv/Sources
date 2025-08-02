namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Channels", Schema = "communities")]
    public partial class Channel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChannelIID { get; set; }

        [StringLength(100)]
        public string ChannelName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? ChannelTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }
    }
}
