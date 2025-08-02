using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SocialMediaPostings", Schema = "marketing")]
    public partial class SocialMediaPosting
    {
        public SocialMediaPosting()
        {
            SocialMediaPostingMaps = new HashSet<SocialMediaPostingMap>();
        }

        [Key]
        public long SocialMediaPostingIID { get; set; }
        public string Title { get; set; }
        public string MessageContent { get; set; }
        [StringLength(1000)]
        public string ShortSummary { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("SocialMediaPosting")]
        public virtual ICollection<SocialMediaPostingMap> SocialMediaPostingMaps { get; set; }
    }
}
