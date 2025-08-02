namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.ChartRowTypes")]
    public partial class ChartRowType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChartRowType()
        {
            ChartOfAccountMaps = new HashSet<ChartOfAccountMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChartRowTypeID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}
