namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChannelTypes", Schema = "communities")]
    public partial class ChannelType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChannelTypeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }
}
