using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Transports;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class TransportApplicationMapper : DTOEntityDynamicMapper
    {
        public static TransportApplicationMapper Mapper(CallContext context)
        {
            var mapper = new TransportApplicationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TransportApplicationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TransportApplicationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TransportApplications
                    .Where(X => X.TransportApplicationIID == IID)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.Student)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.Class)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.School)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private TransportApplicationDTO ToDTO(TransportApplication entity)
        {
            var transportApplication = new TransportApplicationDTO()
            {
                TransportApplicationIID = entity.TransportApplicationIID,
                ApplicationNumber = entity.ApplicationNumber,
                LoginID = entity.LoginID,
                ParentID = entity.ParentID,
                LandMark = entity.LandMark,
                FatherName = entity.FatherName,
                MotherName = entity.MotherName,
                AcademicYearID = entity.AcademicYearID,
                AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.ToString(), Value = entity.AcademicYear.Description + " " + entity.AcademicYear.AcademicYearCode } : new KeyValueDTO(),
                MotherContactNumber = entity.MotherContactNumber,
                MotherEmailID = entity.MotherEmailID,
                Building_FlatNo = entity.Building_FlatNo,
                StreetNo = entity.StreetNo,
                StreetName = entity.StreetName,
                LocationNo = entity.LocationNo,
                LocationName = entity.LocationName,
                ZoneNo = entity.ZoneNo,
                ZoneID = entity.ZoneID,
                City = entity.City,
                EmergencyContactNumber = entity.EmergencyContactNumber,
                EmergencyEmailID = entity.EmergencyEmailID,
                PickUpTime = entity.PickUpTime,
                DropOffTime = entity.DropOffTime,
                PickUpStop = entity.PickUpStop,
                DropOffStop = entity.DropOffStop,
                StreetID = entity.StreetID,
                FatherContactNumber = entity.FatherContactNumber,
                FatherEmailID = entity.FatherEmailID,
                SchoolID = entity.SchoolID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                PickupStopMapID = entity.PickupStopMapID,
                DropStopMapID = entity.DropStopMapID,
                BuildingNo_Drop = entity.BuildingNo_Drop,
                LocationName_Drop = entity.LocationName_Drop,
                LocationNo_Drop = entity.LocationNo_Drop,
                StreetName_Drop = entity.StreetName_Drop,
                StreetNo_Drop = entity.StreetNo_Drop,
                ZoneNo_Drop = entity.ZoneNo_Drop,
                LandMark_Drop = entity.LandMark_Drop,
                IsRouteDifferent = entity.IsRouteDifferent,
                IsNewStops = entity.IsNewStops,
                Remarks = entity.Remarks,
            };

            transportApplication.TransportApplicationStudentMaps = new List<TransportApplicationStudentMapDTO>();

            foreach (var map in entity.TransportApplctnStudentMaps)
            {
                transportApplication.TransportApplicationStudentMaps.Add(new TransportApplicationStudentMapDTO
                {
                    TransportApplctnStudentMapIID = map.TransportApplctnStudentMapIID,
                    TransportApplicationID = map.TransportApplicationID,
                    ClassID = map.ClassID,
                    ClassName = map.ClassID.HasValue ? map.Class.ClassDescription : null,
                    StudentID = map.StudentID,
                    Student = map.StudentID.HasValue ? new KeyValueDTO() { Key = map.StudentID.ToString(), Value = map.Student.AdmissionNumber + "-" + map.Student.FirstName + " " + map.Student.MiddleName + " " + map.Student.LastName } : new KeyValueDTO(),
                    FirstName = map.FirstName,
                    MiddleName = map.MiddleName,
                    LastName = map.LastName,
                    GenderID = map.GenderID,
                    StartDate = map.StartDate,
                    Remarks1 = map.Remarks1,
                    IsActive = map.IsActive,
                    IsNewRider = map.IsNewRider,
                    LocationChange = map.IsNewRider != true ? true : false,
                    SchoolID = map.SchoolID,
                    SchoolName = map.SchoolID.HasValue ? map.School.SchoolName : null,
                    TransportApplcnStatusID = map.TransportApplcnStatusID,
                    IsMedicalCondition = map.IsMedicalCondition,
                    Remarks = map.Remarks,
                    CreatedBy = map.CreatedBy,
                    CreatedDate = map.CreatedDate,
                    UpdatedBy = map.UpdatedBy,
                    UpdatedDate = map.UpdatedDate,
                });
            }

            return transportApplication;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();

            var toDto = dto as TransportApplicationDTO;

            if (toDto.TransportApplicationStudentMaps.Count == 0)
            {
                throw new Exception("Please Select Student for Application");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var allocatedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_ALLOCATED_STATUS_ID");
                var cancelStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_CANCEL_REQUESTID");
                var rejectStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_REJECT_STATUS_ID");

                //Checking datas and Validations
                foreach (var dtoList in toDto.TransportApplicationStudentMaps)
                {
                    if (dtoList.CheckBoxStudent == true && dtoList.StartDate == null)
                    {
                        throw new Exception("Please Select Start Date !");
                    }

                    var todayDate = DateTime.Now.Date;

                    if (_context.UserRole == "Parent Portal" && dtoList.StartDate < todayDate)
                    {
                        throw new Exception("Start date must be greater than or equal to today's date");
                    }

                    if (dtoList.IsMedicalCondition == true && dtoList.Remarks == null)
                    {
                        throw new Exception("Please Explian about Medical Condition !");
                    }

                    if (dtoList.TransportApplicationID != 0 && byte.Parse(allocatedStatusID) == dtoList.TransportApplcnStatusID
                        || dtoList.TransportApplicationID != 0 && byte.Parse(cancelStatusID) == dtoList.TransportApplcnStatusID)
                    {
                        throw new Exception("Can't Change Status to 'Allocated'/'Cancel' from screen");
                    }


                    //if (dtoList.IsNewRider == true && dtoList.LocationChange == true)
                    //{
                    //    throw new Exception("please select any one  'New rider' or 'Location change'");
                    //}


                    if (_context.UserRole != "Parent Portal" && dtoList.TransportApplcnStatusID == byte.Parse(rejectStatusID) && dtoList.Remarks1 == null)
                    {
                        throw new Exception("Please fill remark field");
                    }

                    #region old code to delete edit data and re-entry
                    //var oldData = dbContext.TransportApplctnStudentMaps.Where(s => s.StudentID == dtoList.StudentID && s.IsActive == true).ToList();
                    //foreach (var del in oldData)
                    //{
                    //    dbContext.TransportApplctnStudentMaps.RemoveRange(oldData);
                    //    toDto.TransportApplicationIID = 0;
                    //    toDto.ApplicationNumber = null;
                    //    dtoList.TransportApplctnStudentMapIID = 0;
                    //}
                    //dbContext.SaveChanges();
                    #endregion

                }

                if (toDto.TransportApplicationIID == 0)
                {
                    try
                    {
                        sequence = mutualRepository.GetNextSequence("TransportApplicationCode", null);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Please generate sequence with 'TransportApplicationCode'");
                    }
                }

                //convert the dto to entity and pass to the repository.
                var entity = new TransportApplication()
                {
                    TransportApplicationIID = toDto.TransportApplicationIID,
                    ApplicationNumber = toDto.TransportApplicationIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.ApplicationNumber,
                    LoginID = toDto.LoginID,
                    ParentID = toDto.ParentID,
                    LandMark = toDto.LandMark,
                    FatherName = toDto.FatherName,
                    MotherName = toDto.MotherName,
                    MotherContactNumber = toDto.MotherContactNumber,
                    MotherEmailID = toDto.MotherEmailID,
                    Building_FlatNo = toDto.Building_FlatNo,
                    StreetNo = toDto.StreetNo,
                    StreetName = toDto.StreetName,
                    LocationNo = toDto.LocationNo,
                    LocationName = toDto.LocationName,
                    ZoneNo = toDto.ZoneNo,
                    ZoneID = toDto.ZoneID,
                    City = toDto.City,
                    Remarks = toDto.Remarks,
                    EmergencyContactNumber = toDto.EmergencyContactNumber,
                    EmergencyEmailID = toDto.EmergencyEmailID,
                    PickUpTime = toDto.PickUpTime,
                    DropOffTime = toDto.DropOffTime,
                    PickUpStop = toDto.PickUpStop,
                    DropOffStop = toDto.DropOffStop,
                    StreetID = toDto.StreetID,
                    FatherContactNumber = toDto.FatherContactNumber,
                    FatherEmailID = toDto.FatherEmailID,
                    PickupStopMapID = toDto.PickupStopMapID,
                    DropStopMapID = toDto.DropStopMapID,
                    BuildingNo_Drop = toDto.BuildingNo_Drop,
                    LocationName_Drop = toDto.LocationName_Drop,
                    LocationNo_Drop = toDto.LocationNo_Drop,
                    StreetName_Drop = toDto.StreetName_Drop,
                    StreetNo_Drop = toDto.StreetNo_Drop,
                    ZoneNo_Drop = toDto.ZoneNo_Drop,
                    LandMark_Drop = toDto.LandMark_Drop,
                    IsNewStops = toDto.IsNewStops,
                    IsRouteDifferent = toDto.IsRouteDifferent,
                    SchoolID = (byte?)(toDto.SchoolID != null ? toDto.SchoolID : _context.SchoolID),
                    AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : _context.AcademicYearID,
                    CreatedBy = toDto.TransportApplicationIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    CreatedDate = toDto.TransportApplicationIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedBy = toDto.TransportApplicationIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    UpdatedDate = toDto.TransportApplicationIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                //delete maps
                //var entities = dbContext.TransportApplctnStudentMaps.Where(x =>
                //    x.TransportApplicationID == entity.TransportApplicationIID &&
                //    !IIDs.Contains(x.TransportApplctnStudentMapIID)).ToList();

                //if (entities.IsNotNull())
                //    dbContext.TransportApplctnStudentMaps.RemoveRange(entities);

                entity.TransportApplctnStudentMaps = new List<TransportApplctnStudentMap>();

                foreach (var map in toDto.TransportApplicationStudentMaps)
                {
                    var studentDet = map.StudentID.HasValue ? dbContext.Students.Where(x => x.StudentIID == map.StudentID && x.IsActive == true).AsNoTracking().FirstOrDefault() : null;
                    var academicData = map.StartDate.HasValue ? dbContext.AcademicYears.Where(ac => map.StartDate >= ac.StartDate && map.StartDate <= ac.EndDate && ac.SchoolID == studentDet.SchoolID).AsNoTracking().FirstOrDefault() : null;

                    entity.TransportApplctnStudentMaps.Add(new TransportApplctnStudentMap
                    {
                        TransportApplctnStudentMapIID = map.TransportApplctnStudentMapIID,
                        TransportApplicationID = map.TransportApplicationID,
                        ClassID = map.ClassID != null ? map.ClassID : studentDet.ClassID,
                        StudentID = map.StudentID,
                        FirstName = map.FirstName != null ? map.FirstName : studentDet.FirstName,
                        MiddleName = map.MiddleName != null ? map.MiddleName : studentDet.MiddleName,
                        LastName = map.LastName != null ? map.LastName : studentDet.LastName,
                        GenderID = map.GenderID != null ? map.GenderID : studentDet.GenderID,
                        StartDate = map.StartDate,
                        IsActive = map.TransportApplcnStatusID == byte.Parse(rejectStatusID) ? false : map.IsActive,
                        IsNewRider = map.IsNewRider,
                        Remarks1 = map.Remarks1,
                        SchoolID = studentDet.SchoolID.HasValue ? studentDet.SchoolID : null,
                        TransportApplcnStatusID = map.TransportApplctnStudentMapIID != 0 ? map.ApplicationStatus != null ? byte.Parse(map.ApplicationStatus) : map.TransportApplcnStatusID : 1, //1 for applied status
                        IsMedicalCondition = map.IsMedicalCondition,
                        Remarks = map.Remarks,
                        AcademicYearID = map.AcademicYearID == null ? academicData.AcademicYearID : map.AcademicYearID,
                        CreatedBy = map.TransportApplctnStudentMapIID == 0 ? (int)_context.LoginID : map.CreatedBy,
                        CreatedDate = map.TransportApplctnStudentMapIID == 0 ? DateTime.Now : map.CreatedDate,
                        UpdatedBy = map.TransportApplctnStudentMapIID > 0 ? (int)_context.LoginID : map.UpdatedBy,
                        UpdatedDate = map.TransportApplctnStudentMapIID > 0 ? DateTime.Now : map.UpdatedDate,
                    });
                }

                dbContext.TransportApplications.Add(entity);

                if (entity.TransportApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                if (entity.TransportApplctnStudentMaps.Count > 0)
                {
                    var allocStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_ALLOCATED_STATUS_ID");
                    foreach (var studMap in entity.TransportApplctnStudentMaps)
                    {
                        var studentTransport = dbContext.StudentRouteStopMaps.Where(s => s.StudentID == studMap.StudentID && s.IsActive == true).AsNoTracking().ToList();
                        if (studentTransport.Count > 0)
                        {
                            studMap.IsNewRider = false;
                        }
                        else
                        {
                            studMap.IsNewRider = true;
                        }

                        if (studMap.TransportApplctnStudentMapIID == 0)
                        {
                            //old application against the student set to InActive when create new application
                            var oldData = dbContext.TransportApplctnStudentMaps.Where(s => s.StudentID == studMap.StudentID && s.IsActive == true).AsNoTracking().OrderByDescending(o => o.TransportApplctnStudentMapIID).FirstOrDefault();
                            if (oldData != null)
                            {
                                oldData.IsActive = false;
                                dbContext.Entry(oldData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            dbContext.Entry(studMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                        }
                        else
                        {
                            dbContext.Entry(studMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }
                }
                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.TransportApplicationIID));
            }
        }

        //For Parent Portal
        public TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id)
        {
            var studentMapList = new List<TransportApplicationStudentMapDTO>();
            var applicationDTO = new TransportApplicationDTO();


            using (var dbContext = new dbEduegateSchoolContext())
            {
                var appliedStsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_APPLIED_REQUESTID");
                var allocatedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_ALLOCATED_STATUS_ID");
                var cancelStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_CANCEL_REQUESTID");

                //id != 0 --then edit here id is passing TransportApplctnStudentMapIID else LoginID
                var students = id != 0 ? dbContext.Students.Where(s => s.TransportApplctnStudentMaps.Any(x => x.TransportApplctnStudentMapIID == id))
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Gender)
                    .Include(i => i.Parent)
                    .Include(i => i.TransportApplctnStudentMaps).ThenInclude(i => i.TransportApplication)
                    .AsNoTracking().ToList() :
                    dbContext.Students.Where(s => s.Parent.LoginID == _context.LoginID.Value && s.IsActive == true)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Gender)
                    .Include(i => i.Parent)
                    .Include(i => i.TransportApplctnStudentMaps).ThenInclude(i => i.TransportApplication)
                    .AsNoTracking().ToList();

                if (students.Count > 0)
                {
                    foreach (var stud in students)
                    {
                        var getStudMapData = id != 0 ?
                            dbContext.TransportApplctnStudentMaps.Where(f => f.TransportApplctnStudentMapIID == id)
                            .Include(i => i.TransportApplication).ThenInclude(i => i.Parent)
                            .AsNoTracking().FirstOrDefault() :
                            dbContext.TransportApplctnStudentMaps.Where(d => d.StudentID == stud.StudentIID)
                            .Include(i => i.TransportApplication)
                            .OrderByDescending(o => o.TransportApplctnStudentMapIID)
                            .AsNoTracking().FirstOrDefault();

                        bool? isNewEntry = id != 0 ? false : true;

                        if (id == 0)
                        {
                            if (getStudMapData?.TransportApplcnStatusID == byte.Parse(allocatedStatusID) || getStudMapData?.TransportApplcnStatusID == byte.Parse(cancelStatusID))
                            {
                                isNewEntry = true;
                            }
                        }

                        bool? fillAppliedDatas = false;

                        if (id == 0 && getStudMapData?.TransportApplcnStatusID == byte.Parse(appliedStsID) || id != 0 && getStudMapData?.TransportApplcnStatusID == byte.Parse(cancelStatusID))
                        {
                            fillAppliedDatas = false;
                        }
                        else
                        {
                            fillAppliedDatas = true;
                        }

                        if (fillAppliedDatas == true)
                        {

                            studentMapList.Add(new TransportApplicationStudentMapDTO()
                            {
                                TransportApplctnStudentMapIID = isNewEntry == true ? 0 : getStudMapData != null ? getStudMapData.TransportApplctnStudentMapIID : 0,
                                TransportApplicationID = isNewEntry == true ? 0 : getStudMapData != null ? getStudMapData.TransportApplicationID : 0,
                                ApplicationNumber = id != 0 ? getStudMapData != null ? getStudMapData.TransportApplication.ApplicationNumber : null : null,
                                StudentID = stud.StudentIID,
                                FirstName = stud.FirstName,
                                MiddleName = stud.MiddleName,
                                LastName = stud.LastName,
                                GenderID = stud.GenderID,
                                GenderName = stud.GenderID.HasValue ? stud.Gender.Description : null,
                                ClassID = stud.ClassID,
                                ClassName = stud.ClassID.HasValue ? stud.Class.ClassDescription : null,
                                SchoolID = stud.SchoolID,
                                ApplicationStatus = id != 0 ? getStudMapData?.TransportApplcnStatusID?.ToString() : "1",
                                TransportApplcnStatusID = id != 0 ? getStudMapData?.TransportApplcnStatusID : 1,
                                IsNewRider = getStudMapData?.IsNewRider,
                                IsActive = getStudMapData != null && id != 0 ? getStudMapData.IsActive : true,
                                LocationChange = id != 0 ? true : false,
                                StartDate = id == 0 ? DateTime.Now : getStudMapData.StartDate,
                                IsMedicalCondition = getStudMapData != null ? getStudMapData.IsMedicalCondition : false,
                                Remarks = getStudMapData?.Remarks,
                                CreatedBy = getStudMapData?.CreatedBy,
                                CreatedDate = getStudMapData?.CreatedDate,
                                UpdatedBy = getStudMapData?.UpdatedBy,
                                UpdatedDate = getStudMapData?.UpdatedDate,
                            });

                            if (stud.Parent != null)
                            {
                                //var locationData = students.Select(s => s.TransportApplctnStudentMaps?.FirstOrDefault()?.TransportApplication).FirstOrDefault();
                                var locationData = getStudMapData?.TransportApplication;

                                applicationDTO = new TransportApplicationDTO()
                                {
                                    TransportApplicationIID = isNewEntry == true ? 0 : (long)(locationData != null ? locationData?.TransportApplicationIID : 0),
                                    FatherEmailID = locationData != null ? locationData?.FatherEmailID : stud.Parent?.GaurdianEmail,
                                    FatherContactNumber = locationData != null ? locationData?.FatherContactNumber : stud.Parent?.GuardianPhone,
                                    MotherEmailID = locationData != null ? locationData?.MotherEmailID : stud.Parent?.MotherEmailID,
                                    ApplicationNumber = studentMapList?.FirstOrDefault().ApplicationNumber,
                                    MotherContactNumber = locationData != null ? locationData?.MotherContactNumber : stud.Parent?.MotherPhone,
                                    EmergencyEmailID = locationData?.EmergencyEmailID,
                                    EmergencyContactNumber = locationData?.EmergencyContactNumber,
                                    LoginID = stud.Parent?.LoginID,
                                    LocationName = locationData?.LocationName,
                                    ParentID = stud.Parent?.ParentIID,
                                    FatherName = stud.Parent?.FatherFirstName + "" + stud.Parent?.FatherMiddleName + "" + stud.Parent?.FatherLastName,
                                    MotherName = stud.Parent?.MotherFirstName + "" + stud.Parent?.MotherMiddleName + "" + stud.Parent?.MotherLastName,
                                    LandMark = locationData?.LandMark,
                                    Building_FlatNo = locationData?.Building_FlatNo,
                                    StreetNo = locationData?.StreetNo,
                                    ZoneNo = locationData?.ZoneNo,
                                    SchoolID = locationData?.SchoolID,
                                    Remarks = locationData?.Remarks,
                                    CreatedBy = locationData?.CreatedBy,
                                    CreatedDate = locationData?.CreatedDate,
                                    UpdatedBy = locationData?.UpdatedBy,
                                    UpdatedDate = locationData?.UpdatedDate,
                                    TransportApplicationStudentMaps = studentMapList,
                                };
                            }
                        }
                    }
                }
                return applicationDTO;
            }
        }

        public List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new List<TransportApplicationStudentMapDTO>();
                var mainDatas = dbContext.TransportApplications.Where(a => a.LoginID == loginID)
                    .Include(i => i.TransportApplctnStudentMaps).ThenInclude(i => i.Student)
                    .Include(i => i.TransportApplctnStudentMaps).ThenInclude(i => i.Class)
                    .Include(i => i.TransportApplctnStudentMaps).ThenInclude(i => i.TransportApplicationStatus)
                    .AsNoTracking().ToList();

                foreach (var datas in mainDatas)
                {
                    foreach (var application in datas.TransportApplctnStudentMaps)
                    {
                        dtos.Add(new TransportApplicationStudentMapDTO()
                        {
                            StudentID = application.StudentID,
                            FirstName = application?.FirstName,
                            MiddleName = application?.MiddleName,
                            LastName = application?.LastName,
                            ClassName = application?.Class.ClassDescription,
                            ApplicationNumber = datas?.ApplicationNumber,
                            TransportApplctnStudentMapIID = (long)(application?.TransportApplctnStudentMapIID),
                            UpdatedDateString = application.UpdatedDate.HasValue ? application.UpdatedDate.Value.ToString() : null,
                            IsActiveOrNot = application.IsActive == true ? "Active" : "Inactive",
                            ContactNumber = datas?.FatherContactNumber,
                            CreatedBy = application?.CreatedBy,
                            CreatedDate = application?.CreatedDate,
                            UpdatedBy = application?.UpdatedBy,
                            UpdatedDate = application?.UpdatedDate,
                            StartDate = application?.StartDate,
                            AdmissionNumber = application?.Student?.AdmissionNumber,
                            ApplicationStatus = application?.IsActive == false ? "Inactive" : application?.TransportApplicationStatus.Description,
                            TransportApplicationID = application.TransportApplicationID,
                            TransportApplcnStatusID = application.TransportApplcnStatusID,
                            Remarks = datas?.Remarks,
                            Remarks1 = application?.Remarks1,
                        });
                    }
                }
                return dtos;
            }
        }

        public List<TransportApplicationDTO> GetTransportApplicationsByLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new List<TransportApplicationDTO>();
                var applications = dbContext.TransportApplications.Where(a => a.LoginID == loginID)
                    .Include(x => x.AcademicYear)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.Student)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.Class)
                    .Include(x => x.TransportApplctnStudentMaps).ThenInclude(i => i.School)
                    .AsNoTracking().ToList();

                foreach (var application in applications)
                {
                    dtos.Add(ToDTO(application));
                }

                return dtos;
            }
        }

        public int GetTransportApplicationCount(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var transportApplications = dbContext.TransportApplications.Where(a => a.LoginID == loginID).AsNoTracking().ToList();

                return transportApplications != null ? transportApplications.Count() : 0;
            }
        }

        public TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dtos = new TransportApplicationDTO();
                var studData = dbContext.TransportApplctnStudentMaps.Where(x => x.TransportApplctnStudentMapIID == TransportApplctnStudentMapIID)
                    .Include(i => i.AcademicYears)
                    .Include(i => i.Student)
                    .AsNoTracking().FirstOrDefault();

                var applications = dbContext.TransportApplications.AsNoTracking().FirstOrDefault(a => a.TransportApplicationIID == studData.TransportApplicationID);

                dtos.AcademicYearID = studData?.AcademicYearID;
                dtos.AcademicYear = studData.AcademicYearID.HasValue ? new KeyValueDTO()
                {
                    Key = studData.AcademicYearID.ToString(),
                    Value = studData.AcademicYears.Description + " " + studData.AcademicYears.AcademicYearCode
                } : new KeyValueDTO();
                dtos.ApplicationNumber = applications.ApplicationNumber;
                dtos.TransportApplctnStudentMapID = studData?.TransportApplctnStudentMapIID;
                dtos.Student = studData.StudentID.HasValue ? new KeyValueDTO()
                {
                    Key = studData.StudentID.ToString(),
                    Value = studData.Student.AdmissionNumber + '-' + studData.FirstName + " " + studData.MiddleName + " " + studData.LastName
                } : new KeyValueDTO();
                dtos.DateFrom = studData.StartDate.HasValue ? studData.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                dtos.SchoolID = studData?.SchoolID;

                return dtos;
            }
        }

        public void DeleteTransportApplication(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studData = dbContext.TransportApplctnStudentMaps.AsNoTracking().FirstOrDefault(x => x.TransportApplctnStudentMapIID == IID);

                var applications = dbContext.TransportApplications.AsNoTracking().FirstOrDefault(a => a.TransportApplicationIID == studData.TransportApplicationID);

                if (applications.TransportApplctnStudentMaps.Count() == 1)
                {
                    dbContext.TransportApplctnStudentMaps.Remove(studData);
                    dbContext.TransportApplications.Remove(applications);
                    dbContext.SaveChanges();
                }
                else
                {
                    dbContext.TransportApplctnStudentMaps.Remove(studData);
                    dbContext.SaveChanges();
                }
            }
        }

        //cancel from Parent portal listview
        public string CancelTransportApplication(long mapIID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studData = dbContext.TransportApplctnStudentMaps.AsNoTracking().FirstOrDefault(x => x.TransportApplctnStudentMapIID == mapIID);

                var cancelStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TRANSPORT_APPLICATION_CANCEL_REQUESTID");

                if (studData != null)
                {
                    studData.TransportApplcnStatusID = byte.Parse(cancelStatsID);
                    dbContext.Entry(studData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    return "Application Cancelled Successfully!";
                }
                else
                {
                    return null;
                }
            }
        }

    }
}