using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Families", Schema = "communities")]
    public partial class Family
    {
        public Family()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public long FamilyIID { get; set; }
        [StringLength(50)]
        public string AreaCode { get; set; }
        [StringLength(50)]
        public string RegisterNumber { get; set; }
        [StringLength(2000)]
        public string HouseName { get; set; }
        [StringLength(100)]
        public string HouseNumber { get; set; }
        [StringLength(500)]
        public string Landmark { get; set; }
        [StringLength(500)]
        public string Place { get; set; }
        [StringLength(50)]
        public string Pin { get; set; }
        [StringLength(500)]
        public string DivisionName { get; set; }
        [StringLength(2000)]
        public string DivisionDetails { get; set; }
        [StringLength(50)]
        public string WardNumber { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? BlockID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LandPhone { get; set; }
        [StringLength(1000)]
        public string HouseGuardianPhoto { get; set; }
        [StringLength(10)]
        public string HouseOwnerShipType { get; set; }
        [StringLength(10)]
        public string HouseType { get; set; }
        [StringLength(50)]
        public string FamilyIncome { get; set; }
        [StringLength(50)]
        public string PropertyDetails { get; set; }
        [StringLength(50)]
        public string Informer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string Status { get; set; }
        public int? Grade { get; set; }
        public int? MembersCount { get; set; }
        public long? HeadMemberID { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Family")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
