namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EventAudienceMaps", Schema = "collaboration")]
    public partial class EventAudienceMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long EventAudienceMapIID { get; set; }

        public long? EventID { get; set; }

        public byte? EventAudienceTypeID { get; set; }

        public int? StudentCategoryID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Class Class { get; set; }

        public virtual Event Event { get; set; }

        public virtual EventAudienceType EventAudienceType { get; set; }

        public virtual Section Section { get; set; }

        public virtual StudentCategory StudentCategory { get; set; }
    }
}
