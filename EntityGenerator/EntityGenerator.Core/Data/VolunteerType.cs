using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VolunteerType", Schema = "schools")]
    public partial class VolunteerType
    {
        public VolunteerType()
        {
            ParentCanYouVolunteerToHelpOnes = new HashSet<Parent>();
            ParentCanYouVolunteerToHelpTwoes = new HashSet<Parent>();
            StudentApplicationCanYouVolunteerToHelpOnes = new HashSet<StudentApplication>();
            StudentApplicationCanYouVolunteerToHelpTwoes = new HashSet<StudentApplication>();
        }

        [Key]
        public int VolunteerTypeID { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("CanYouVolunteerToHelpOne")]
        public virtual ICollection<Parent> ParentCanYouVolunteerToHelpOnes { get; set; }
        [InverseProperty("CanYouVolunteerToHelpTwo")]
        public virtual ICollection<Parent> ParentCanYouVolunteerToHelpTwoes { get; set; }
        [InverseProperty("CanYouVolunteerToHelpOne")]
        public virtual ICollection<StudentApplication> StudentApplicationCanYouVolunteerToHelpOnes { get; set; }
        [InverseProperty("CanYouVolunteerToHelpTwo")]
        public virtual ICollection<StudentApplication> StudentApplicationCanYouVolunteerToHelpTwoes { get; set; }
    }
}
