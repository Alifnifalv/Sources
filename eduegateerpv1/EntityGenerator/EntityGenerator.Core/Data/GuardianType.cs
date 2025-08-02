using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("GuardianTypes", Schema = "schools")]
    public partial class GuardianType
    {
        public GuardianType()
        {
            Parents = new HashSet<Parent>();
            StudentApplicationFatherStudentRelationShips = new HashSet<StudentApplication>();
            StudentApplicationGuardianStudentRelationShips = new HashSet<StudentApplication>();
            StudentApplicationMotherStudentRelationShips = new HashSet<StudentApplication>();
            StudentApplicationPrimaryContacts = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte GuardianTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("MotherStudentRelationShip")]
        public virtual ICollection<Parent> Parents { get; set; }
        [InverseProperty("FatherStudentRelationShip")]
        public virtual ICollection<StudentApplication> StudentApplicationFatherStudentRelationShips { get; set; }
        [InverseProperty("GuardianStudentRelationShip")]
        public virtual ICollection<StudentApplication> StudentApplicationGuardianStudentRelationShips { get; set; }
        [InverseProperty("MotherStudentRelationShip")]
        public virtual ICollection<StudentApplication> StudentApplicationMotherStudentRelationShips { get; set; }
        [InverseProperty("PrimaryContact")]
        public virtual ICollection<StudentApplication> StudentApplicationPrimaryContacts { get; set; }
        [InverseProperty("PrimaryContact")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
