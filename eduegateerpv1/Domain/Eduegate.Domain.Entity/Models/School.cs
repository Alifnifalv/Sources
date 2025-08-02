using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Schools", Schema = "schools")]
    public partial class Schools
    {
        public Schools()
        {
            AcadamicCalendars = new HashSet<AcadamicCalendar>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            TransactionHeads = new HashSet<TransactionHead>();
            Department1 = new HashSet<Department1>();
        }

        [Key]
        public byte SchoolID { get; set; }

        [StringLength(50)]
        public string SchoolName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Address1 { get; set; }

        [StringLength(500)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string RegistrationID { get; set; }

        public int? CompanyID { get; set; }

        //public virtual Company Company { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public string SchoolCode { get; set; }

        public string Place { get; set; }

        public long? SchoolProfileID { get; set; }

        [StringLength(10)]
        public string SchoolShortName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcadamicCalendar> AcadamicCalendars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }

        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        public virtual ICollection<Department1> Department1 { get; set; }

    }
}