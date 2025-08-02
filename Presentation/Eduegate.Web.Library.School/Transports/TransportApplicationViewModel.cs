using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Eduegate.Web.Library.School.Transports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TransportApplication", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class TransportApplicationViewModel : BaseMasterViewModel
    {
        public TransportApplicationViewModel()
        {
            TransportApplicationStudentMaps = new List<TransportApplicationStudentMapViewModel>() { new TransportApplicationStudentMapViewModel() };
            TransportLocation = new TransportApplicationLocationDetailsViewModel();
            IsRouteDifferent = false;
            IsNewStops = false;
        }

        public long TransportApplicationIID { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("ApplicationNumber")]
        [StringLength(50)]
        public string ApplicationNumber { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[CustomDisplay("Academic Year")]
        //[Select2("AcademicYear", "Numeric", false)]
        //[LookUp("LookUps.AcademicYear")]

        public KeyValueViewModel AcademicYear { get; set; }

        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FatherName")]
        [StringLength(50)]
        public string FatherName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("FatherContactNumber")]
        [StringLength(50)]
        public string FatherContactNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("FatherEmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "Invalid Email Address")]

        public string FatherEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]

        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherName")]
        [StringLength(50)]
        public string MotherName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MotherContactNumber")]
        [StringLength(50)]
        public string MotherContactNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("MotherEmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "Invalid Email Address")]
        public string MotherEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Building/FlatNo")]
        [StringLength(20)]
        public string Building_FlatNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("StreetNo")]
        [StringLength(50)]
        public string StreetNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ZoneNo")]
        [StringLength(50)]
        public string ZoneNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LocationNo")]
        [StringLength(50)]
        public string LocationNo { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("StreetName")]
        [StringLength(50)]
        public string StreetName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LocationName")]
        [StringLength(50)]
        public string LocationName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("LandMark")]
        [StringLength(200)]
        public string LandMark { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("City")]
        //[StringLength(50)]
        //public string City { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("EmergencyContactNumber")]
        [StringLength(50)]
        public string EmergencyContactNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("EmergencyEmailID")]
        [RegularExpression("[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?", ErrorMessage = "Invalid Email Address")]
        public string EmergencyEmailID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=CRUDModel.ViewModel.NewStops==true")]
        [CustomDisplay("PickUpStop")]
        [LookUp("LookUps.PickupStopALL")]
        public string PickupStopMap { get; set; }
        public long? PickupStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=CRUDModel.ViewModel.NewStops==true")]
        [CustomDisplay("DropStop")]
        [LookUp("LookUps.DropStopALL")]
        public string DropStopMap { get; set; }
        public long? DropStopMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsPicKUp/DropDifferent")]
        public bool? IsRouteDifferent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("OtherStops")]
        public bool? IsNewStops { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine7 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsNewStops!=true")]
        [CustomDisplay("PickUpStop")]
        [StringLength(75)]
        public string PickUpStop { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=CRUDModel.ViewModel.IsNewStops!=true")]
        [CustomDisplay("DropStop")]
        [StringLength(75)]
        public string DropOffStop { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Campus")]
        //[LookUp("LookUps.School")]
        //public string School { get; set; }
        //public byte? SchoolID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Remarks")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; }

        public short? ZoneID { get; set; }

        public DateTime? PickUpTime { get; set; }

        public DateTime? DropOffTime { get; set; }

        public short? StreetID { get; set; }

        //public string Remarks { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "TransportLocation", "TransportLocation")]
        [CustomDisplay("LocationDetails")]
        public TransportApplicationLocationDetailsViewModel TransportLocation { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StudentDetails")]
        public List<TransportApplicationStudentMapViewModel> TransportApplicationStudentMaps { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TransportApplicationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransportApplicationViewModel>(jsonString);
        }

        public TransportApplicationViewModel ToVM(TransportApplicationDTO dto)
        {
            Mapper<TransportApplicationDTO, TransportApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<TransportApplicationStudentMapDTO, TransportApplicationStudentMapViewModel>.CreateMap();
            var applicationDto = dto as TransportApplicationDTO;
            var vm = Mapper<TransportApplicationDTO, TransportApplicationViewModel>.Map(applicationDto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //vm.CreatedDateString = applicationDto.CreatedDate.HasValue ? applicationDto.CreatedDate.Value.ToLongDateString() : null;
            //vm.CreatedDate = applicationDto.CreatedDate;

            return vm;
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TransportApplicationDTO, TransportApplicationViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<TransportApplicationStudentMapDTO, TransportApplicationStudentMapViewModel>.CreateMap();
            var trDtO = dto as TransportApplicationDTO;
            var vm = Mapper<TransportApplicationDTO, TransportApplicationViewModel>.Map(trDtO);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            //vm.AcademicYear = trDtO.AcademicYearID.HasValue ? new KeyValueViewModel()
            //{
            //    Key = trDtO.AcademicYearID.ToString(),
            //    Value = trDtO.AcademicYear.Value
            //} : null;

            vm.ApplicationNumber = trDtO.ApplicationNumber;
            vm.PickupStopMapID = trDtO.PickupStopMapID;
            vm.PickupStopMap = trDtO.PickupStopMapID.HasValue ? trDtO.PickupStopMapID.ToString() : null;
            vm.DropStopMapID = trDtO.DropStopMapID;
            vm.Remarks = trDtO.Remarks;
            vm.DropStopMap = trDtO.DropStopMapID.HasValue ? trDtO.DropStopMapID.ToString() : null;
            vm.IsRouteDifferent = trDtO.IsRouteDifferent;
            vm.TransportLocation = new TransportApplicationLocationDetailsViewModel()
            {
                BuildingNo_Drop = trDtO.BuildingNo_Drop,
                LandMark_Drop = trDtO.LandMark_Drop,
                LocationName_Drop = trDtO.LocationName_Drop,
                LocationNo_Drop = trDtO.LocationNo_Drop,
                StreetName_Drop = trDtO.StreetName_Drop,
                StreetNo_Drop = trDtO.StreetNo_Drop,
                ZoneNo_Drop = trDtO.ZoneNo_Drop,
            };

            vm.TransportApplicationStudentMaps = new List<TransportApplicationStudentMapViewModel>();
            foreach (var map in trDtO.TransportApplicationStudentMaps)
            {
                if (map.StudentID.HasValue)
                {
                    vm.TransportApplicationStudentMaps.Add(new TransportApplicationStudentMapViewModel()
                    {
                        TransportApplctnStudentMapIID = map.TransportApplctnStudentMapIID,
                        TransportApplicationID = map.TransportApplicationID,
                        Student = map.StudentID.HasValue ? new KeyValueViewModel()
                        {
                            Key = map.StudentID.ToString(),
                            Value = map.Student.Value
                        } : null,
                        StudentName = map.FirstName + " " + map.MiddleName + " " + map.LastName,
                        FirstName = map.FirstName,
                        MiddleName = map.MiddleName,
                        LastName = map.LastName,
                        SchoolID = map.SchoolID,
                        StudentID = map.StudentID,
                        SchoolName = map.SchoolName,
                        StartDate = string.IsNullOrEmpty(map.StartDateString) ? (DateTime?)null : DateTime.ParseExact(map.StartDateString, dateFormat, CultureInfo.InvariantCulture),
                        //ClassName = map.ClassName,
                        //Class = map.ClassID.HasValue ? new KeyValueViewModel() { Key = map.ClassID.ToString(), Value = map.ClassName } : new KeyValueViewModel(),
                        Gender = map.GenderID.HasValue ? map.GenderID.ToString() : null,
                        StartDateString = map.StartDate.HasValue ? map.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        TransportApplcnStatusID = map.TransportApplcnStatusID,
                        ApplicationStatus = map.TransportApplcnStatusID.HasValue ? map.TransportApplcnStatusID.ToString() : null,
                        CreatedDateString = map.CreatedDate.HasValue ? map.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        IsActive = map.IsActive,
                        IsNewRider = map.IsNewRider,
                        LocationChange = map.LocationChange,
                        CreatedByID = map.CreatedBy,
                        CreatedDate = map.CreatedDate,
                        Remarks1 = map.Remarks1,
                        IsMedicalCondition = map.IsMedicalCondition,
                        CheckBoxStudent = map.StudentID.HasValue ? true : map.CheckBoxStudent,
                        Remarks = map.Remarks
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TransportApplicationViewModel, TransportApplicationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<TransportApplicationStudentMapViewModel, TransportApplicationStudentMapDTO>.CreateMap();
            Mapper<TransportApplicationLocationDetailsViewModel, TransportApplicationDTO>.CreateMap();
            var dto = Mapper<TransportApplicationViewModel, TransportApplicationDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.TransportApplicationIID = this.TransportApplicationIID;
            dto.AcademicYearID = this.AcademicYearID != null ? int.Parse(this.AcademicYear.Key) : (int?)null;
            dto.PickupStopMapID = this.PickupStopMapID.HasValue ? this.PickupStopMapID : null;
            dto.DropStopMapID = this.DropStopMapID.HasValue ? this.DropStopMapID : null;
            dto.PickupStopMapID = string.IsNullOrEmpty(this.PickupStopMap) ? (long?)null : long.Parse(this.PickupStopMap);
            dto.DropStopMapID = string.IsNullOrEmpty(this.DropStopMap) ? (long?)null : long.Parse(this.DropStopMap);
            //dto.SchoolID = string.IsNullOrEmpty(this.School) ? (byte?)null : byte.Parse(this.School);
            dto.TransportApplicationStudentMaps = new List<TransportApplicationStudentMapDTO>();
            dto.IsRouteDifferent = this.IsRouteDifferent;
            dto.IsNewStops = this.IsNewStops;
            dto.MotherEmailID = this.MotherEmailID;
            dto.Remarks = this.Remarks;
            dto.BuildingNo_Drop = this.TransportLocation.BuildingNo_Drop != null ? this.TransportLocation.BuildingNo_Drop : null;
            dto.LandMark_Drop = this.TransportLocation.LandMark_Drop != null ? this.TransportLocation.LandMark_Drop : null;
            dto.LocationName_Drop = this.TransportLocation.LocationName_Drop != null ? this.TransportLocation.LocationName_Drop : null;
            dto.LocationNo_Drop = this.TransportLocation.LocationNo_Drop != null ? this.TransportLocation.LocationNo_Drop : null;
            dto.StreetName_Drop = this.TransportLocation.StreetName_Drop != null ? this.TransportLocation.StreetName_Drop : null;
            dto.StreetNo_Drop = this.TransportLocation.StreetNo_Drop != null ? this.TransportLocation.StreetNo_Drop : null;
            dto.ZoneNo_Drop = this.TransportLocation.ZoneNo_Drop != null ? this.TransportLocation.ZoneNo_Drop : null;

            foreach (var map in this.TransportApplicationStudentMaps)
            {
                if (map.StudentID.HasValue && map.CheckBoxStudent == true || map.Student != null && map.CheckBoxStudent == true)
                {
                    dto.TransportApplicationStudentMaps.Add(new TransportApplicationStudentMapDTO()
                    {
                        TransportApplctnStudentMapIID = map.TransportApplctnStudentMapIID,
                        TransportApplicationID = map.TransportApplicationID,
                        ApplicationNumber = this.ApplicationNumber,
                        //ClassID = string.IsNullOrEmpty(map.Class.Key) ? (int?)null : int.Parse(map.Class.Key),
                        //GenderID = string.IsNullOrEmpty(map.Gender) ? (byte?)null : byte.Parse(map.Gender),
                        //TransportApplcnStatusID = string.IsNullOrEmpty(map.ApplicationStatus) ? (byte?)null : byte.Parse(map.ApplicationStatus),
                        GenderID = map.GenderID,
                        //StartDate = string.IsNullOrEmpty(map.StartDateString) ? (DateTime?)null : DateTime.ParseExact(map.StartDateString, dateFormat, CultureInfo.InvariantCulture),
                        StartDate = string.IsNullOrEmpty(map.StartDateString) ? (DateTime?)null : DateTime.ParseExact(map.StartDateString, dateFormat, CultureInfo.InvariantCulture),
                        StudentID = map.ClassID.HasValue ? map.StudentID : string.IsNullOrEmpty(map.Student.Key) ? (long?)null : long.Parse(map.Student.Key),
                        FirstName = map.FirstName,
                        MiddleName = map.MiddleName,
                        LastName = map.LastName,
                        IsActive = map.IsActive,
                        Remarks1 = map.Remarks1,
                        LocationChange = map.LocationChange,
                        IsNewRider = map.IsNewRider,
                        SchoolID = map.SchoolID,
                        CreatedBy = map.CreatedByID,
                        CreatedDate = string.IsNullOrEmpty(map.CreatedDateString) ? (DateTime?)null : DateTime.ParseExact(map.CreatedDateString, dateFormat, CultureInfo.InvariantCulture),
                        TransportApplcnStatusID = map.ApplicationStatus != null ? byte.Parse(map.ApplicationStatus) : map.TransportApplcnStatusID,
                        ApplicationStatus = map.ApplicationStatus,
                        IsMedicalCondition = map.IsMedicalCondition,
                        CheckBoxStudent = map.CheckBoxStudent,
                        Remarks = map.Remarks,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TransportApplicationDTO>(jsonString);
        }
    }
}