using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PageBoilerplateMapParameters", Schema = "cms")]
    public partial class PageBoilerplateMapParameter
    {
        public PageBoilerplateMapParameter()
        {
            PageBoilerplateMapParameterCultureDatas = new HashSet<PageBoilerplateMapParameterCultureData>();
        }

        [Key]
        public long PageBoilerplateMapParameterIID { get; set; }
        public long? PageBoilerplateMapID { get; set; }
        [StringLength(50)]
        public string ParameterName { get; set; }
        [StringLength(50)]
        public string ParameterValue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PageBoilerplateMapID")]
        [InverseProperty("PageBoilerplateMapParameters")]
        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
        [InverseProperty("PageBoilerplateMapParameter")]
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }
    }
}
