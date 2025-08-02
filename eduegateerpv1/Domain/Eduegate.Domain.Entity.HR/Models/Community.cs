namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Communitys", Schema = "schools")]
    public partial class Community
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Community()
        {
            //Students = new HashSet<Student>();
        }

        public byte CommunityID { get; set; }

        [StringLength(50)]
        public string CommunityDescription { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }
    }
}
