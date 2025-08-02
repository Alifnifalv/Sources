using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Nationalities", Schema = "mutual")]
    public partial class Nationality
    {
        public Nationality()
        {
            Employees = new HashSet<Employee>();
            JobSeekers = new HashSet<JobSeeker>();
            Leads = new HashSet<Lead>();
            ParentFatherCountries = new HashSet<Parent>();
            ParentGuardianNationalities = new HashSet<Parent>();
            ParentMotherCountries = new HashSet<Parent>();
            StudentApplicationFatherCountries = new HashSet<StudentApplication>();
            StudentApplicationGuardianNationalities = new HashSet<StudentApplication>();
            StudentApplicationMotherCountries = new HashSet<StudentApplication>();
            StudentApplicationNationalities = new HashSet<StudentApplication>();
            StudentPassportDetails = new HashSet<StudentPassportDetail>();
        }

        [Key]
        public int NationalityIID { get; set; }
        [StringLength(50)]
        public string NationalityName { get; set; }
        [StringLength(50)]
        public string NationalityCode { get; set; }
        public int? CountryID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLead { get; set; }

        [InverseProperty("Nationality")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Nationality")]
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
        [InverseProperty("Nationality")]
        public virtual ICollection<Lead> Leads { get; set; }
        [InverseProperty("FatherCountry")]
        public virtual ICollection<Parent> ParentFatherCountries { get; set; }
        [InverseProperty("GuardianNationality")]
        public virtual ICollection<Parent> ParentGuardianNationalities { get; set; }
        [InverseProperty("MotherCountry")]
        public virtual ICollection<Parent> ParentMotherCountries { get; set; }
        [InverseProperty("FatherCountry")]
        public virtual ICollection<StudentApplication> StudentApplicationFatherCountries { get; set; }
        [InverseProperty("GuardianNationality")]
        public virtual ICollection<StudentApplication> StudentApplicationGuardianNationalities { get; set; }
        [InverseProperty("MotherCountry")]
        public virtual ICollection<StudentApplication> StudentApplicationMotherCountries { get; set; }
        [InverseProperty("Nationality")]
        public virtual ICollection<StudentApplication> StudentApplicationNationalities { get; set; }
        [InverseProperty("Nationality")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetails { get; set; }
    }
}
