namespace Eduegate.Domain.Entity.School.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CategoryFeeMaps", Schema = "catalog")]
    public partial class CategoryFeeMap
    {
        [Key]
        public long CategoryFeeMapIID { get; set; }

        public long? FeeMasterID { get; set; }

        public long? CategoryID { get; set; }

        public bool? IsPrimary { get; set; }

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
