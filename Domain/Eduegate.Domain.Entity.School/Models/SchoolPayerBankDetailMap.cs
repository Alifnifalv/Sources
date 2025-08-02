using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("SchoolPayerBankDetailMap", Schema = "schools")]
    public partial class SchoolPayerBankDetailMap
    {
        public SchoolPayerBankDetailMap()
        {
            WPSDetails = new HashSet<WPSDetail>();
        }

        [Key]
        public long PayerBankDetailIID { get; set; }

        public byte? SchoolID { get; set; }

        public long? BankID { get; set; }

        public bool? IsMainOperating { get; set; }

        public string PayerIBAN { get; set; }

        [StringLength(15)]
        public string PayerBankShortName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        //public byte[] TimeStamps { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual Schools School { get; set; }

        public virtual ICollection<WPSDetail> WPSDetails { get; set; }
    }
}