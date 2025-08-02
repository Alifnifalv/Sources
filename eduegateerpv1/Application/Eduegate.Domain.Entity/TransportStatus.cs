namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.TransportStatuses")]
    public partial class TransportStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransportStatus()
        {
            StaffRouteShiftMapLogs = new HashSet<StaffRouteShiftMapLog>();
            StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TransportStatusID { get; set; }

        [StringLength(100)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteShiftMapLog> StaffRouteShiftMapLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }
    }
}
