using Eduegate.Framework;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.SignUp.SignUps;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Services.Contracts.Commons;
using System.Linq;
using Eduegate.Framework.Contracts.Common.Enums;
using System;
using Eduegate.Domain.Mappers.School.School;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Mappers.School.Students;
using System.Globalization;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Domain.Mappers.SignUp.Meeting;

namespace Eduegate.Domain.SignUp.SignUps
{
    public class SignUpBL
    {
        private CallContext Context { get; set; }

        public SignUpBL(CallContext context)
        {
            Context = context;
        }

        public SignUpGroupDTO FillSignUpDetailsByGroupID(int groupID)
        {
            var signUpDTOs = new List<SignUpDTO>();
            var studentList = new List<KeyValueDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);

            var signUpGroup = SignUpGroupMapper.Mapper(Context).GetGroupDetailsByID(groupID);

            var signUpDTOList = SignUpMapper.Mapper(Context).FillSignUpDetailsByGroupID(groupID);

            var parentID = parentDetail != null ? parentDetail.ParentIID : 0;

            var parentStudents = StudentMapper.Mapper(Context).GetActiveStudentsDetailsByParent(parentID);

            var studentKeyValueList = new List<KeyValueDTO>();
            studentKeyValueList.AddRange(parentStudents.Select(s => new KeyValueDTO()
            {
                Key = s.StudentIID.ToString(),
                Value = s.StudentFullName
            }));

            foreach (var signUpDTO in signUpDTOList)
            {
                if (signUpDTO.SignupStatusID == signupPublishedStatusID)
                {
                    if (signUpDTO.StudentID.HasValue)
                    {
                        if (!parentStudents.Any(s => s.StudentIID == signUpDTO.StudentID.Value))
                        {
                            continue;
                        }
                    }
                    else if (signUpDTO.ClassID.HasValue)
                    {
                        if (signUpDTO.SectionID.HasValue)
                        {
                            if (!parentStudents.Any(s => s.ClassID == signUpDTO.ClassID && s.SectionID == signUpDTO.SectionID))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (!parentStudents.Any(s => s.ClassID == signUpDTO.ClassID))
                            {
                                continue;
                            }
                        }

                    }
                    else if (!signUpDTO.ClassID.HasValue && signUpDTO.SectionID.HasValue)
                    {
                        if (!parentStudents.Any(s => s.SectionID == signUpDTO.SectionID))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (signUpDTO.SchoolID.HasValue)
                        {
                            if (!parentStudents.Any(s => s.SchoolID == signUpDTO.SchoolID))
                            {
                                continue;
                            }
                        }
                    }

                    signUpDTO.IsExpand = false;

                    if (signUpDTO.SignupSlotMaps.Count > 0)
                    {
                        FillSignUpSlotMapDetails(signUpDTO, signUpGroup);
                    }

                    signUpDTOs.Add(signUpDTO);
                }

                if (signUpDTO.StudentID.HasValue)
                {
                    if (parentStudents.Any(s => s.StudentIID == signUpDTO.StudentID))
                    {
                        if (!studentList.Any(l => l.Key.Contains(signUpDTO.StudentID.ToString())))
                        {
                            studentList.Add(new KeyValueDTO()
                            {
                                Key = signUpDTO.StudentID.ToString(),
                                Value = signUpDTO.StudentName
                            });
                        }
                    }
                }
                else
                {
                    if (signUpDTO.ClassID.HasValue)
                    {
                        foreach (var student in parentStudents)
                        {
                            if (signUpDTO.SectionID.HasValue)
                            {
                                if (student.ClassID == signUpDTO.ClassID && student.SectionID == signUpDTO.SectionID)
                                {
                                    if (!studentList.Any(l => l.Key.Contains(student.StudentIID.ToString())))
                                    {
                                        studentList.Add(new KeyValueDTO()
                                        {
                                            Key = student.StudentIID.ToString(),
                                            Value = student.StudentFullName
                                        });
                                    }
                                }
                            }
                            else
                            {
                                if (student.ClassID == signUpDTO.ClassID)
                                {
                                    if (!studentList.Any(l => l.Key.Contains(student.StudentIID.ToString())))
                                    {
                                        studentList.Add(new KeyValueDTO()
                                        {
                                            Key = student.StudentIID.ToString(),
                                            Value = student.StudentFullName
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (signUpGroup.CurrentDate > signUpGroup.DueDate)
            {
                signUpGroup.IsExpired = true;
            }

            if (studentList.Count == 0)
            {
                studentList = studentKeyValueList;
            }

            signUpGroup.Students = studentList;
            signUpGroup.SignUpDTOs = signUpDTOs;

            return signUpGroup;
        }

        private void FillSignUpSlotMapDetails(SignUpDTO signUpDTO, SignUpGroupDTO groupDTO = null)
        {
            var currentDate = DateTime.Now;
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var openSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);
            var assignedSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);
            var cancelSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);
            var closedSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CLOSED", 4);
            var breakSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_BREAK", 5);
            var holdSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_HOLD", 6);

            var groupedSlots = signUpDTO.SignupSlotMaps.GroupBy(g => g.SlotDate.Value.Date).ToList().OrderBy(x => x.Key);

            var slotAvailableCount = 0;

            var timeMaps = new List<SignupSlotMapDTO>();

            signUpDTO.SignupSlotMaps = new List<SignupSlotMapDTO>();

            foreach (var groupedSlot in groupedSlots)
            {
                timeMaps = new List<SignupSlotMapDTO>();

                bool isDateActive = true;

                bool isSlotCurrentDate = false;
                if (currentDate.Date == groupedSlot.Key)
                {
                    isSlotCurrentDate = true;
                }

                foreach (var slotMap in groupedSlot.ToList())
                {
                    if (slotMap.IsSlotAllocated == false)
                    {
                        slotAvailableCount += 1;
                    }

                    bool isActiveSlot = true;
                    if (groupDTO != null)
                    {
                        if (groupDTO.DueDate == currentDate.Date)
                        {
                            if (slotMap.SlotDate.Value.Date == currentDate.Date)
                            {
                                if (currentDate.TimeOfDay >= slotMap.StartTime.Value)
                                {
                                    isActiveSlot = false;
                                }
                            }
                        }
                        else if (currentDate.Date == groupedSlot.Key)
                        {
                            if (slotMap.SlotDate.Value.Date == currentDate.Date)
                            {
                                if (currentDate.TimeOfDay >= slotMap.StartTime.Value)
                                {
                                    isActiveSlot = false;
                                }
                            }
                        }
                    }

                    //if (slotMap.SlotMapStatusID == cancelSlotStatusID || slotMap.SlotMapStatusID == closedSlotStatusID)
                    //{
                    //    isActiveSlot = false;
                    //}

                    if (slotMap.SlotMapStatusID != openSlotStatusID && slotMap.SlotMapStatusID != assignedSlotStatusID)
                    {
                        isActiveSlot = false;
                    }

                    var isTimeExpired = false;
                    if (isSlotCurrentDate == true)
                    {
                        if (currentDate.TimeOfDay > slotMap.EndTime)
                        {
                            isTimeExpired = true;
                        }
                    }

                    var studentDTO = new SignupStudentDTO();
                    var studentDetails = slotMap.StudentID.HasValue ? StudentMapper.Mapper(Context).GetStudentDetailFromStudentID(slotMap.StudentID.Value) : null;

                    if (studentDetails != null)
                    {
                        studentDTO = new SignupStudentDTO()
                        {
                            StudentIID = studentDetails.StudentIID,
                            ClassID = studentDetails.ClassID,
                            ClassName = studentDetails.ClassName,
                            SectionID = studentDetails.SectionID,
                            SectionName = studentDetails.SectionName,
                            SchoolID = studentDetails.SchoolID,
                            AcademicYearID = studentDetails.AcademicYearID,
                        };
                    }

                    var slotAllocationMaps = FillSlotAllocationMapDetails(slotMap, signUpDTO);
                    var slotAllocationRemarkMap = FillSlotAllocationRemarksMapDetails(signUpDTO, slotMap, null);

                    timeMaps.Add(new SignupSlotMapDTO()
                    {
                        SignupSlotMapIID = slotMap.SignupSlotMapIID,
                        SignupID = slotMap.SignupID,
                        SignupSlotTypeID = slotMap.SignupSlotTypeID,
                        SignupSlotType = slotMap.SignupSlotType,
                        SlotDate = slotMap.SlotDate,
                        SlotDateString = slotMap.SlotDateString,
                        StartTime = slotMap.StartTime,
                        StartTimeString = slotMap.StartTimeString,
                        EndTime = slotMap.EndTime,
                        EndTimeString = slotMap.EndTimeString,
                        Duration = slotMap.Duration,
                        SchoolID = slotMap.SchoolID,
                        School = slotMap.School,
                        AcademicYearID = slotMap.AcademicYearID,
                        AcademicYear = slotMap.AcademicYear,
                        SlotMapStatusID = slotMap.SlotMapStatusID,
                        SlotMapStatusName = slotMap.SlotMapStatusName,
                        IsSlotAllocated = slotMap.IsSlotAllocated,
                        SignupSlotAllocationMaps = slotAllocationMaps,
                        SignupSlotAllocationRemarkMap = slotAllocationRemarkMap,
                        EmployeeID = slotMap.EmployeeID,
                        Employee = slotMap.Employee,
                        StudentID = slotMap.StudentID,
                        Student = slotMap.Student,
                        ParentID = slotMap.ParentID,
                        Parent = slotMap.Parent,
                        IsSlotActive = isActiveSlot,
                        SignupStudent = studentDTO,
                        IsTimeExpired = isTimeExpired,
                        CreatedBy = slotMap.CreatedBy,
                        CreatedDate = slotMap.CreatedDate,
                        UpdatedBy = slotMap.UpdatedBy,
                        UpdatedDate = slotMap.UpdatedDate,
                    });
                }

                if (currentDate.Date > groupedSlot.FirstOrDefault().SlotDate.Value.Date)
                {
                    isDateActive = false;
                }

                signUpDTO.SignupSlotMaps.Add(new SignupSlotMapDTO()
                {
                    SlotDate = groupedSlot.Key,
                    SlotDateString = groupedSlot.Key.ToString(dateFormat),
                    SignupSlotMapTimes = timeMaps,
                    IsSlotActive = isDateActive,
                    IsSlotDateCurrentDate = isSlotCurrentDate,
                });
            }

            signUpDTO.SlotAvailableCount = slotAvailableCount;
        }

        private List<SignupSlotAllocationMapDTO> FillSlotAllocationMapDetails(SignupSlotMapDTO slotMap, SignUpDTO signUpDTO)
        {
            var slotAllocationMaps = new List<SignupSlotAllocationMapDTO>();

            foreach (var allocMap in slotMap.SignupSlotAllocationMaps)
            {
                allocMap.SignupSlotRemarkMap = SignupSlotRemarkMapMapper.Mapper(Context).GetSlotDetailsByAllocationID(allocMap.SignupSlotAllocationMapIID);

                if (allocMap.SignupSlotRemarkMap.SignupSlotRemarkMapIID == 0)
                {
                    allocMap.SignupSlotRemarkMap = FillSlotAllocationRemarksMapDetails(signUpDTO, slotMap, allocMap);
                }
                //else
                //{
                //    allocMap.SignupSlotRemarkMap.ParentRemarks = !string.IsNullOrEmpty(allocMap.SignupSlotRemarkMap.ParentRemarks) ? allocMap.SignupSlotRemarkMap.ParentRemarks.Replace("\n", "<br/>") : allocMap.SignupSlotRemarkMap.ParentRemarks;
                //}

                slotAllocationMaps.Add(allocMap);
            }

            return slotAllocationMaps;
        }

        private SignupSlotRemarkMapDTO FillSlotAllocationRemarksMapDetails(SignUpDTO signUpDTO, SignupSlotMapDTO slotMap, SignupSlotAllocationMapDTO allocMap = null)
        {
            var slotAllocationRemarkMap = new SignupSlotRemarkMapDTO()
            {
                SignupSlotRemarkMapIID = 0,
                SignupSlotAllocationMapID = allocMap != null ? allocMap.SignupSlotAllocationMapIID : null,
                SignupSlotMapID = slotMap.SignupSlotMapIID,
                SignupID = signUpDTO.SignupIID,
                TeacherRemarks = null,
                ParentRemarks = null,
            };

            return slotAllocationRemarkMap;
        }

        public OperationResultDTO SaveSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var returnResult = new OperationResultDTO();

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);
            var employeeLoginDetail = Setting.Mappers.CommonDataMapper.Mapper(Context).GetLoginDetailByEmployeeID(signUpSlotMap.SignupOrganizerEmployeeID);

            var slotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            var signUpAllocationDTO = new SignupSlotAllocationMapDTO()
            {
                SignupSlotAllocationMapIID = 0,
                SignupSlotMapID = signUpSlotMap.SignupSlotMapIID,
                ParentID = signUpSlotMap.ParentID.HasValue ? signUpSlotMap.ParentID : parentDetail?.ParentIID,
                StudentID = signUpSlotMap.StudentID.HasValue ? signUpSlotMap.StudentID : (long?)null,
                EmployeeID = signUpSlotMap.EmployeeID.HasValue ? signUpSlotMap.EmployeeID : (long?)null,
                SchoolID = signUpSlotMap.SchoolID.HasValue ? signUpSlotMap.SchoolID : (byte?)null,
                AcademicYearID = signUpSlotMap.AcademicYearID.HasValue ? signUpSlotMap.AcademicYearID : (int?)null,
                SlotMapStatusID = slotMapStatsID,
                ParentLoginID = parentDetail?.LoginID,
                GuardianEmailID = parentDetail?.GaurdianEmail,
                SlotDateString = signUpSlotMap.SlotDateString,
                SlotTimeString = signUpSlotMap.StartTimeString + " - " + signUpSlotMap.EndTimeString,
                OrganizerEmployeeID = signUpSlotMap.SignupOrganizerEmployeeID,
                OrganizerEmployeeName = signUpSlotMap.SignupOrganizerEmployeeName,
                OrganizerEmployeeLoginID = employeeLoginDetail?.LoginIID,
                OrganizerEmployeeEmailID = employeeLoginDetail?.LoginEmailID,
            };

            try
            {
                var result = SignUpMapper.Mapper(Context).SaveSelectedSignUpSlot(signUpAllocationDTO);

                if (result.operationResult == OperationResult.Error)
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = result.Message,
                    };
                }
                else
                {
                    SignUpMapper.Mapper(Context).UpdateSignupSlotMapStatus(signUpSlotMap.SignupSlotMapIID, slotMapStatsID);

                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = result.Message,
                    };
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.Contains("inner") && ex.Message.Contains("exception") ? ex.InnerException?.Message : ex.Message;

                returnResult = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage,
                };
            }

            return returnResult;
        }

        public List<SignUpGroupDTO> GetActiveSignupGroups()
        {
            var returnSignupGroups = new List<SignUpGroupDTO>();

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);
            var signupGroups = SignUpGroupMapper.Mapper(Context).GetActiveSignupGroups();

            var parentID = parentDetail != null ? parentDetail.ParentIID : 0;

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var parentStudents = StudentMapper.Mapper(Context).GetActiveStudentsDetailsByParent(parentID);

            if (signupGroups != null && signupGroups.Count > 0)
            {
                foreach (var signupGroup in signupGroups)
                {
                    var signUpDTOList = SignUpMapper.Mapper(Context).FillSignUpDetailsByGroupID(signupGroup.SignupGroupID);

                    foreach (var signUpDTO in signUpDTOList)
                    {
                        if (signUpDTO.SignupStatusID == signupPublishedStatusID)
                        {
                            if (signUpDTO.StudentID.HasValue)
                            {
                                if (!parentStudents.Any(s => s.StudentIID == signUpDTO.StudentID.Value))
                                {
                                    continue;
                                }
                            }

                            if (signUpDTO.StudentID.HasValue)
                            {
                                if (parentStudents.Any(s => s.StudentIID == signUpDTO.StudentID))
                                {
                                    signupGroup.SignUpDTOs.Add(signUpDTO);
                                }
                            }
                            else
                            {
                                if (signUpDTO.ClassID.HasValue)
                                {
                                    foreach (var student in parentStudents)
                                    {
                                        if (signUpDTO.SectionID.HasValue)
                                        {
                                            if (student.ClassID == signUpDTO.ClassID && student.SectionID == signUpDTO.SectionID)
                                            {
                                                signupGroup.SignUpDTOs.Add(signUpDTO);
                                            }
                                        }
                                        else
                                        {
                                            if (student.ClassID == signUpDTO.ClassID)
                                            {
                                                signupGroup.SignUpDTOs.Add(signUpDTO);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    signupGroup.SignUpDTOs.Add(signUpDTO);
                                }
                            }
                        }
                    }

                    if (signupGroup.SignUpDTOs.Count > 0)
                    {
                        returnSignupGroups.Add(signupGroup);
                    }
                }

                if (returnSignupGroups.Count > 0)
                {
                    returnSignupGroups.FirstOrDefault().IsSelected = true;
                }
            }

            return returnSignupGroups;
        }

        public OperationResultDTO CancelSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var returnResult = new OperationResultDTO();

            var openedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);

            try
            {
                var result = SignUpMapper.Mapper(Context).CancelSelectedSignUpSlot(signUpSlotMap);

                if (result.operationResult == OperationResult.Error)
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = result.Message,
                    };
                }
                else
                {
                    SignUpMapper.Mapper(Context).UpdateSignupSlotMapStatus(signUpSlotMap.SignupSlotMapIID, openedSlotMapStatsID);

                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = result.Message,
                    };
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.Contains("inner") && ex.Message.Contains("exception") ? ex.InnerException?.Message : ex.Message;

                returnResult = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage,
                };
            }

            return returnResult;
        }

        public List<SignUpDTO> GetEmployeesActiveSignups(long employeeID)
        {
            var signUpDTOs = new List<SignUpDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signupDTOList = SignUpMapper.Mapper(Context).GetActiveSignUpDetailsByEmployeeID(employeeID);

            foreach (var signUpDTO in signupDTOList)
            {
                if (signUpDTO.SignupStatusID == signupPublishedStatusID)
                {
                    signUpDTO.IsExpand = true;

                    if (signUpDTO.SignupSlotMaps.Count > 0)
                    {
                        FillSignUpSlotMapDetails(signUpDTO, null);

                        signUpDTO.SignupSlotMaps = signUpDTO.SignupSlotMaps
                            .OrderBy(o => o.SlotDate < DateTime.Today) // Push past dates to the end
                            .ThenBy(o => o.SlotDate != DateTime.Today) // Put today's date first
                            .ThenBy(o => o.SlotDate) // Sort future dates ascending
                            .ToList();
                    }

                    signUpDTOs.Add(signUpDTO);
                }
            }

            return signUpDTOs;
        }

        public List<SignUpDTO> GetMeetingRequestSlotsByEmployeeID(long employeeID, string reqSlotDateString, long? studentID = null, int? classID = null, int? sectionID = null)
        {
            var signUpDTOs = new List<SignUpDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signUpDTOList = SignUpMapper.Mapper(Context).GetActiveMeetingRequestDetailsByEmployeeID(employeeID);

            foreach (var signUpDTO in signUpDTOList)
            {
                if (signUpDTO.IsSlotShowToUser == true)
                {
                    bool isStudentAvailable = false;
                    if (signUpDTO.SignupStatusID == signupPublishedStatusID)
                    {
                        if (signUpDTO.StudentID.HasValue && studentID.HasValue && signUpDTO.StudentID != studentID)
                        {
                            isStudentAvailable = false;
                            continue;
                        }

                        if (signUpDTO.StudentID.HasValue && studentID.HasValue && signUpDTO.StudentID == studentID)
                        {
                            isStudentAvailable = true;
                        }
                        else
                        {
                            if (signUpDTO.ClassID.HasValue && classID.HasValue)
                            {
                                if (signUpDTO.SectionID.HasValue && sectionID.HasValue)
                                {
                                    if (signUpDTO.ClassID == classID && signUpDTO.SectionID == sectionID)
                                    {
                                        isStudentAvailable = true;
                                    }
                                }
                                else
                                {
                                    if (signUpDTO.ClassID == classID)
                                    {
                                        isStudentAvailable = true;
                                    }
                                }
                            }
                            else
                            {
                                isStudentAvailable = true;
                            }
                        }

                        if (isStudentAvailable)
                        {
                            signUpDTO.IsExpand = true;
                            if (signUpDTO.SignupSlotMaps.Count > 0)
                            {
                                GetSignUpSlotMapForRequest(signUpDTO, null, reqSlotDateString);
                            }

                            signUpDTOs.Add(signUpDTO);
                        }
                    }
                }
            }

            return signUpDTOList;
        }

        private void GetSignUpSlotMapForRequest(SignUpDTO signUpDTO, SignUpGroupDTO groupDTO = null, string reqSlotDateString = null)
        {
            var currentDate = DateTime.Now;
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var slotOpenStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);

            var groupedSlots = signUpDTO.SignupSlotMaps.GroupBy(g => g.SlotDate.Value.Date).ToList().OrderBy(x => x.Key);

            signUpDTO.SignupSlotMaps = new List<SignupSlotMapDTO>();

            var reqDate = string.IsNullOrEmpty(reqSlotDateString) ? (DateTime?)null : DateTime.ParseExact(reqSlotDateString, dateFormat, CultureInfo.InvariantCulture);

            foreach (var groupedSlot in groupedSlots)
            {
                if (groupedSlot.Key == reqDate)
                {
                    var timeMaps = new List<SignupSlotMapDTO>();

                    bool isDateActive = true;

                    bool isSlotCurrentDate = false;
                    if (currentDate.Date == groupedSlot.Key)
                    {
                        isSlotCurrentDate = true;
                    }

                    foreach (var slotMap in groupedSlot.ToList())
                    {
                        bool isActiveSlot = true;

                        var isTimeExpired = false;
                        if (isSlotCurrentDate == true)
                        {
                            if (currentDate.TimeOfDay > slotMap.EndTime)
                            {
                                isTimeExpired = true;
                            }
                        }

                        if (slotMap.SlotMapStatusID == slotOpenStatusID)
                        {
                            timeMaps.Add(new SignupSlotMapDTO()
                            {
                                SignupSlotMapIID = slotMap.SignupSlotMapIID,
                                SignupID = slotMap.SignupID,
                                SignupSlotTypeID = slotMap.SignupSlotTypeID,
                                SignupSlotType = slotMap.SignupSlotType,
                                SlotDate = slotMap.SlotDate,
                                SlotDateString = slotMap.SlotDateString,
                                StartTime = slotMap.StartTime,
                                StartTimeString = slotMap.StartTimeString,
                                EndTime = slotMap.EndTime,
                                EndTimeString = slotMap.EndTimeString,
                                Duration = slotMap.Duration,
                                SchoolID = slotMap.SchoolID,
                                School = slotMap.School,
                                AcademicYearID = slotMap.AcademicYearID,
                                AcademicYear = slotMap.AcademicYear,
                                SlotMapStatusID = slotMap.SlotMapStatusID,
                                SlotMapStatusName = slotMap.SlotMapStatusName,
                                IsSlotAllocated = slotMap.IsSlotAllocated,
                                EmployeeID = slotMap.EmployeeID,
                                Employee = slotMap.Employee,
                                StudentID = slotMap.StudentID,
                                Student = slotMap.Student,
                                ParentID = slotMap.ParentID,
                                Parent = slotMap.Parent,
                                IsSlotActive = isActiveSlot,
                                IsTimeExpired = isTimeExpired,
                                CreatedBy = slotMap.CreatedBy,
                                CreatedDate = slotMap.CreatedDate,
                                UpdatedBy = slotMap.UpdatedBy,
                                UpdatedDate = slotMap.UpdatedDate,
                            });
                        }
                    }

                    if (currentDate.Date > groupedSlot.FirstOrDefault().SlotDate.Value.Date)
                    {
                        isDateActive = false;
                    }

                    signUpDTO.SignupSlotMaps.Add(new SignupSlotMapDTO()
                    {
                        SlotDate = groupedSlot.Key,
                        SlotDateString = groupedSlot.Key.ToString(dateFormat),
                        SignupSlotMapTimes = timeMaps,
                        IsSlotActive = isDateActive,
                        IsSlotDateCurrentDate = isSlotCurrentDate,
                    });
                }
            }
        }

        public OperationResultDTO SaveMeetingRequest(MeetingRequestDTO requestDTO)
        {
            var returnResult = new OperationResultDTO();

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);

            var requestStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("MEETING_REQUEST_STATUS_REQUESTED", 1);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            if (requestDTO.MeetingRequestIID == 0)
            {
                requestDTO.MeetingRequestStatusID = requestStatusID;
            }

            var meetingRequestDTO = new MeetingRequestDTO()
            {
                MeetingRequestIID = requestDTO.MeetingRequestIID,
                StudentID = requestDTO.Student != null && string.IsNullOrEmpty(requestDTO.Student.Key) ? (long?)null : long.Parse(requestDTO.Student.Key),
                ParentID = requestDTO.ParentID.HasValue ? requestDTO.ParentID : parentDetail?.ParentIID,
                FacultyID = requestDTO.Faculty != null && string.IsNullOrEmpty(requestDTO.Faculty.Key) ? (long?)null : long.Parse(requestDTO.Faculty.Key),
                Faculty = requestDTO.Faculty,
                SchoolID = requestDTO.SchoolID,
                AcademicYearID = requestDTO.AcademicYearID,
                ClassID = requestDTO.ClassID,
                SectionID = requestDTO.SectionID,
                RequestedSignupSlotMapID = requestDTO.RequestedSignupSlotMapID,
                ApprovedSignupSlotMapID = requestDTO.ApprovedSignupSlotMapID,
                MeetingRequestStatusID = requestDTO.MeetingRequestStatusID,
                MeetingRequestStatusName = requestDTO.MeetingRequestStatusName,
                RequestedDate = string.IsNullOrEmpty(requestDTO.RequestedDateString) ? (DateTime?)null : DateTime.ParseExact(requestDTO.RequestedDateString, dateFormat, CultureInfo.InvariantCulture),
                ApprovedDate = string.IsNullOrEmpty(requestDTO.ApprovedDateString) ? (DateTime?)null : DateTime.ParseExact(requestDTO.ApprovedDateString, dateFormat, CultureInfo.InvariantCulture),
                GuardianEmailID = parentDetail?.GaurdianEmail,
                ParentLoginID = parentDetail?.LoginID,
                RequesterRemark = requestDTO.RequesterRemark,
                FacultyRemark = requestDTO.FacultyRemark,
                IsSendNotification = true
            };

            if (string.IsNullOrEmpty(meetingRequestDTO.MeetingRequestStatusName))
            {
                meetingRequestDTO.MeetingRequestStatusName = MeetingRequestMapper.Mapper(Context).GetMeetingRequestStatusNameByID(meetingRequestDTO.MeetingRequestStatusID);
            }

            var employeeLogin = Setting.Mappers.CommonDataMapper.Mapper(Context).GetLoginDetailByEmployeeID(meetingRequestDTO.FacultyID);
            meetingRequestDTO.FacultyLoginID = employeeLogin.LoginIID;
            meetingRequestDTO.FacultyEmailID = employeeLogin.LoginEmailID;

            try
            {
                var meetingRequestIID = MeetingRequestMapper.Mapper(Context).SaveMeetingRequest(meetingRequestDTO);

                meetingRequestDTO.MeetingRequestIID = meetingRequestIID.HasValue ? meetingRequestIID.Value : 0;

                if (meetingRequestDTO.MeetingRequestIID == 0)
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Saving failed!",
                    };
                }
                else
                {
                    if (meetingRequestDTO.IsSendNotification == true)
                    {
                        string messageType = "applied";

                        // To send mail and notification to parent
                        MeetingRequestMapper.Mapper(Context).SendPushNotificationToParent(meetingRequestDTO, messageType);
                        MeetingRequestMapper.Mapper(Context).SendEmailNotificationToParent(meetingRequestDTO, messageType);

                        // To send mail and notification to employee
                        MeetingRequestMapper.Mapper(Context).SendPushNotificationToFaculty(meetingRequestDTO, messageType);
                        MeetingRequestMapper.Mapper(Context).SendEmailNotificationToFaculty(meetingRequestDTO, messageType);
                    }

                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!",
                    };
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.Contains("inner") && ex.Message.Contains("exception") ? ex.InnerException?.Message : ex.Message;

                returnResult = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage,
                };
            }

            return returnResult;
        }

        public List<MeetingRequestDTO> GetMeetingRequestsByParentID(long? parentID)
        {
            if (!parentID.HasValue)
            {
                var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);
                parentID = parentDetail.ParentIID;
            }

            var meetingRequestDTOs = MeetingRequestMapper.Mapper(Context).GetMeetingRequestsByParentID(parentID);

            return meetingRequestDTOs;
        }

        public List<SignupSlotAllocationMapDTO> GetParentAllotedMeetings(long? parentID)
        {
            if (!parentID.HasValue)
            {
                var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);
                parentID = parentDetail.ParentIID;
            }

            var allotedSlotDTOs = SignupAllocationMapper.Mapper(Context).GetParentAllotedMeetings(parentID);

            return allotedSlotDTOs;
        }

    }
}