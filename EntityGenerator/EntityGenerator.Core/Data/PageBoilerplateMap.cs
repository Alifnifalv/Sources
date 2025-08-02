using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PageBoilerplateMaps", Schema = "cms")]
    public partial class PageBoilerplateMap
    {
        public PageBoilerplateMap()
        {
            CategoryPageBoilerPlatMaps = new HashSet<CategoryPageBoilerPlatMap>();
            PageBoilerplateMapParameters = new HashSet<PageBoilerplateMapParameter>();
        }

        [Key]
        public long PageBoilerplateMapIID { get; set; }
        public long? PageID { get; set; }
        public long? ReferenceID { get; set; }
        public long? BoilerplateID { get; set; }
        public int? SerialNumber { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BoilerplateID")]
        [InverseProperty("PageBoilerplateMaps")]
        public virtual BoilerPlate Boilerplate { get; set; }
        [ForeignKey("PageID")]
        [InverseProperty("PageBoilerplateMaps")]
        public virtual Page Page { get; set; }
        [InverseProperty("PageBoilerplateMap")]
        public virtual ICollection<CategoryPageBoilerPlatMap> CategoryPageBoilerPlatMaps { get; set; }
        [InverseProperty("PageBoilerplateMap")]
        public virtual ICollection<PageBoilerplateMapParameter> PageBoilerplateMapParameters { get; set; }
    }
}
