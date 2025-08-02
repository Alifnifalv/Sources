namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.PageBoilerplateMapParameters")]
    public partial class PageBoilerplateMapParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageBoilerplateMapParameterCultureData> PageBoilerplateMapParameterCultureDatas { get; set; }

        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
    }
}
