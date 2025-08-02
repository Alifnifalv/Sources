using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Payroll
{
    [DataContract]
    public class SectorTicketAirfareDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SectorTicketAirfareIID { get; set; }
        [DataMember]
        public DateTime? ValidityFrom { get; set; }
        [DataMember]
        public DateTime? ValidityTo { get; set; }
        
        [DataMember]
        public long? DepartmentID { get; set; }     

        [DataMember]
        public List<SectorTicketAirfareDetailDTO> SectorTicketAirfareDetail { get; set; }


    }
    [DataContract]
    public class SectorTicketAirfareDetailDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SectorTicketAirfareIID { get; set; }
        
        [DataMember]
        public bool? IsTwoWay { get; set; }
        [DataMember]
        public short? FlightClassID { get; set; }
        [DataMember]
        public long? AirportID { get; set; }
        [DataMember]
        public long? ReturnAirportID { get; set; }
        [DataMember]
        public string GenerateTravelSector { get; set; }
        [DataMember]
        public decimal? Rate { get; set; }
       
        [DataMember]
        public virtual KeyValueDTO FlightClass { get; set; }
        [DataMember]
        public virtual KeyValueDTO Airport { get; set; }
        [DataMember]
        public virtual KeyValueDTO ReturnAirport { get; set; }
    }
    }
