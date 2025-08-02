using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Cities", Schema = "mutual")]
    public partial class City
    {
        public City()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        public int CityID { get; set; }
        public int? CountryID { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Cities")]
        public virtual Country Country { get; set; }
        [InverseProperty("RigistrationCity")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
