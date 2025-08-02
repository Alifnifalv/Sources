using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ClassFeeMasters", Schema = "schools")]
    public partial class ClassFeeMaster
    {
        public ClassFeeMaster()
        {
            FeeCollections = new HashSet<FeeCollection>();
            FeeDueFeeTypeMaps = new HashSet<FeeDueFeeTypeMap>();
            FeeMasterClassMaps = new HashSet<FeeMasterClassMap>();
        }

        [Key]
        public long ClassFeeMasterIID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? ClassID { get; set; }
        public int? AcadamicYearID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("AcadamicYearID")]
        [InverseProperty("ClassFeeMasters")]
        public virtual AcademicYear AcadamicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ClassFeeMasters")]
        public virtual Class Class { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ClassFeeMasters")]
        public virtual School School { get; set; }
        [InverseProperty("ClassFeeMaster")]
        public virtual ICollection<FeeCollection> FeeCollections { get; set; }
        [InverseProperty("ClassFeeMaster")]
        public virtual ICollection<FeeDueFeeTypeMap> FeeDueFeeTypeMaps { get; set; }
        [InverseProperty("ClassFeeMaster")]
        public virtual ICollection<FeeMasterClassMap> FeeMasterClassMaps { get; set; }
    }
}
