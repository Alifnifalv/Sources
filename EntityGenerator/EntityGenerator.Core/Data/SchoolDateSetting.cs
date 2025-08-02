using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SchoolDateSettings", Schema = "schools")]
    public partial class SchoolDateSetting
    {
        public SchoolDateSetting()
        {
            SchoolDateSettingMaps = new HashSet<SchoolDateSettingMap>();
        }

        [Key]
        public long SchoolDateSettingIID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SchoolDateSettings")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SchoolDateSettings")]
        public virtual School School { get; set; }
        [InverseProperty("SchoolDateSetting")]
        public virtual ICollection<SchoolDateSettingMap> SchoolDateSettingMaps { get; set; }
    }
}
