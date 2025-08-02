using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        public string PayerIBAN { get; set; }
        [StringLength(15)]
        public string PayerBankShortName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsMainOperating { get; set; }

        [ForeignKey("BankID")]
        [InverseProperty("SchoolPayerBankDetailMaps")]
        public virtual Bank Bank { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SchoolPayerBankDetailMaps")]
        public virtual School School { get; set; }
        [InverseProperty("PayerBankDetailI")]
        public virtual ICollection<WPSDetail> WPSDetails { get; set; }
    }
}
