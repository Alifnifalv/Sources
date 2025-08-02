namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.RouteStopMaps")]
    public partial class RouteStopMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RouteStopMap()
        {
            //StaffRouteStopMaps = new HashSet<StaffRouteStopMap>();
            //StaffRouteStopMaps1 = new HashSet<StaffRouteStopMap>();
            //StaffRouteStopMaps2 = new HashSet<StaffRouteStopMap>();
            StudentRouteStopMaps = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMaps1 = new HashSet<StudentRouteStopMap>();
            StudentRouteStopMaps2 = new HashSet<StudentRouteStopMap>();
            //StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            //StudentRouteStopMapLogs1 = new HashSet<StudentRouteStopMapLog>();
            //StudentRouteStopMapLogs2 = new HashSet<StudentRouteStopMapLog>();
        }

        [Key]
        public long RouteStopMapIID { get; set; }

        public int RouteID { get; set; }

        [StringLength(50)]
        public string StopName { get; set; }

        public decimal? OneWayFee { get; set; }

        public decimal? TwoWayFee { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? Created { get; set; }

        public int? Updated { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(20)]
        public string StopCode { get; set; }

        public bool? IsActive { get; set; }

        //public virtual AcademicYear AcademicYear { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StaffRouteStopMap> StaffRouteStopMaps2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMap> StudentRouteStopMaps2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs2 { get; set; }
    }
}