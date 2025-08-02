using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Transports
{
    [DataContract]
    public class VehicleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public VehicleDTO()
        {
            RoutesDetails = new List<RoutesDTO>();
        }

        [DataMember]
        public long VehicleIID { get; set; }

        [DataMember]
        public short? VehicleTypeID { get; set; }

        [DataMember]
        public string VehicleType { get; set; }

        [DataMember]
        public short? VehicleOwnershipTypeID { get; set; }

        [DataMember]
        public string VehicleOwnershipType { get; set; }

        [DataMember]
        public string VehicleRegistrationNumber { get; set; }

        [DataMember]
        public string VehicleCode { get; set; }

        [DataMember]
        public string FleetCode { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string RegistrationNo { get; set; }

        [DataMember]
        public System.DateTime? PurchaseDate { get; set; }

        //[DataMember]
        //public DateTime? RegistrationExpire { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public int? YearMade { get; set; }

        [DataMember]
        public int? VehicleAge { get; set; }

        [DataMember]
        public byte? TransmissionID { get; set; }

        [DataMember]
        public string Transmission { get; set; }

        [DataMember]
        public string ManufactureName { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string Power { get; set; }

        [DataMember]
        public int? AllowSeatingCapacity { get; set; }

        [DataMember]
        public int? MaximumSeatingCapacity { get; set; }

        [DataMember]
        public bool? IsSecurityEnabled { get; set; }

        [DataMember]
        public bool? IsCameraEnabled { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public long? LoginID { get; set; }

        #region Mobile app use
        [DataMember]
        public List<RoutesDTO> RoutesDetails { get; set; }

        [DataMember]
        public int? RouteID { get; set; }

        [DataMember]
        public long? EmployeeID { get; set; }
        #endregion
    }
}