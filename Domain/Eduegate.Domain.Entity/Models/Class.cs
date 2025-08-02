namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Classes", Schema = "schools")]
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Students = new HashSet<Student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClassID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string ClassDescription { get; set; }

        public byte? ShiftID { get; set; }

        public byte? SchoolID { get; set; }

        public int? CostCenterID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? ORDERNO { get; set; }

        public int? AcademicYearID { get; set; }

        public long? ClassGroupID { get; set; }

        public bool? IsVisible { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public virtual Schools School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }      
    }
}