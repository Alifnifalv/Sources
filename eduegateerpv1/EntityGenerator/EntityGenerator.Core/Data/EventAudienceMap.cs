using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EventAudienceMaps", Schema = "collaboration")]
    public partial class EventAudienceMap
    {
        [Key]
        public long EventAudienceMapIID { get; set; }
        public long? EventID { get; set; }
        public byte? EventAudienceTypeID { get; set; }
        public int? StudentCategoryID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("EventAudienceMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("EventID")]
        [InverseProperty("EventAudienceMaps")]
        public virtual Event Event { get; set; }
        [ForeignKey("EventAudienceTypeID")]
        [InverseProperty("EventAudienceMaps")]
        public virtual EventAudienceType EventAudienceType { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("EventAudienceMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentCategoryID")]
        [InverseProperty("EventAudienceMaps")]
        public virtual StudentCategory StudentCategory { get; set; }
    }
}
