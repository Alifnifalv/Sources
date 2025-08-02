using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeAllowedCountryMaps", Schema = "inventory")]
    public partial class DeliveryTypeAllowedCountryMap
    {
        [Key]
        public int DeliveryTypeID { get; set; }
        [Key]
        public int FromCountryID { get; set; }
        [Key]
        public int ToCountryID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeAllowedCountryMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("FromCountryID")]
        [InverseProperty("DeliveryTypeAllowedCountryMapFromCountries")]
        public virtual Country FromCountry { get; set; }
        [ForeignKey("ToCountryID")]
        [InverseProperty("DeliveryTypeAllowedCountryMapToCountries")]
        public virtual Country ToCountry { get; set; }
    }
}
