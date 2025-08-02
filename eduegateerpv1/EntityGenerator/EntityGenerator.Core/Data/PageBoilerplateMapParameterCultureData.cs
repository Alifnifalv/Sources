using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PageBoilerplateMapParameterCultureDatas", Schema = "cms")]
    public partial class PageBoilerplateMapParameterCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long PageBoilerplateMapParameterID { get; set; }
        [Required]
        [StringLength(150)]
        public string ParameterValue { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("PageBoilerplateMapParameterCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("PageBoilerplateMapParameterID")]
        [InverseProperty("PageBoilerplateMapParameterCultureDatas")]
        public virtual PageBoilerplateMapParameter PageBoilerplateMapParameter { get; set; }
    }
}
