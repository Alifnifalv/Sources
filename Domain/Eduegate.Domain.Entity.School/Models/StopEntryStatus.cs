using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StopEntryStatuses", Schema = "schools")]
    public partial class StopEntryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StopEntryStatus()
        {
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StopEntryStatusID { get; set; }

        [StringLength(250)]
        public string StatusNameEn { get; set; }

        [StringLength(250)]
        public string StatusNameAr { get; set; }

        [StringLength(100)]
        public string StatusTitleEn { get; set; }

        [StringLength(100)]
        public string StatusTitleAr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
    }
}
