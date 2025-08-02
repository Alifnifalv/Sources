using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductClassMaps", Schema = "catalog")]
    public partial class ProductClassMap
    {
        [Key]
        public long ProductClassMapIID { get; set; }
        public int? ClassID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public int? FeeMasterID { get; set; }
        //public int? ProductClassTypeID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public int? StreamID { get; set; }
        public int? SubjectID { get; set; }

        //public virtual Class Class { get; set; }
    }
}
