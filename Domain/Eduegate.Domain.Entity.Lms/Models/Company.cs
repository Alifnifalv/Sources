using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("Companies", Schema = "mutual")]
    public partial class Company
    {
        public Company()
        {            
            Employees = new HashSet<Employee>();
            Schools = new HashSet<School>();
        }

        [Key]
        public int CompanyID { get; set; }

        [StringLength(100)]
        public string CompanyCode { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        public int? CompanyGroupID { get; set; }

        public int? CountryID { get; set; }

        public int? BaseCurrencyID { get; set; }

        public int? LanguageID { get; set; }

        [StringLength(50)]
        public string RegistraionNo { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        //public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }
        
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<School> Schools { get; set; }

    }
}