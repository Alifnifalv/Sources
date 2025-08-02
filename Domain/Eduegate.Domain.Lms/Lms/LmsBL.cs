using Eduegate.Framework;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Lms.Lms;
using Eduegate.Services.Contracts.Commons;
using System.Linq;
using Eduegate.Framework.Contracts.Common.Enums;
using System;
using Eduegate.Domain.Mappers.School.School;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Mappers.School.Students;
using System.Globalization;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Domain.Mappers.Lms.Lms;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Domain.Mappers.School.Academics;

namespace Eduegate.Domain.Lms.Lms
{
    public class LmsBL
    {
        private CallContext Context { get; set; }

        public LmsBL(CallContext context)
        {
            Context = context;
        }

        public LmsGroupDTO FillSignUpDetailsByGroupID(int groupID)
        {
            var signUpDTOs = new List<LmsDTO>();
            var studentList = new List<KeyValueDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);

            var signUpGroup = LmsGroupMapper.Mapper(Context).GetGroupDetailsByID(groupID);

            var signUpDTOList = LmsMapper.Mapper(Context).FillSignUpDetailsByGroupID(groupID);

            var parentID = parentDetail != null ? parentDetail.ParentIID : 0;

            var parentStudents = StudentMapper.Mapper(Context).GetStudentsDetailsByParent(parentID);

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

        private void FillSignUpSlotMapDetails(LmsDTO signUpDTO, LmsGroupDTO groupDTO = null)
        {
            var currentDate = DateTime.Now;
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var cancelSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);
            var closedSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CLOSED", 4);

            var groupedSlots = signUpDTO.SignupSlotMaps.GroupBy(g => g.SlotDate.Value.Date).ToList().OrderBy(x => x.Key);

            var slotAvailableCount = 0;

            var timeMaps = new List<LmsSlotMapDTO>();

            signUpDTO.SignupSlotMaps = new List<LmsSlotMapDTO>();

            foreach (var groupedSlot in groupedSlots)
            {
                timeMaps = new List<LmsSlotMapDTO>();

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

                    if (slotMap.SlotMapStatusID == cancelSlotStatusID || slotMap.SlotMapStatusID == closedSlotStatusID)
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

                    var studentDTO = new LmsStudentDTO();
                    var studentDetails = slotMap.StudentID.HasValue ? StudentMapper.Mapper(Context).GetStudentDetailFromStudentID(slotMap.StudentID.Value) : null;

                    if (studentDetails != null)
                    {
                        studentDTO = new LmsStudentDTO()
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

                    timeMaps.Add(new LmsSlotMapDTO()
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

                signUpDTO.SignupSlotMaps.Add(new LmsSlotMapDTO()
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

        private List<LmsSlotAllocationMapDTO> FillSlotAllocationMapDetails(LmsSlotMapDTO slotMap, LmsDTO signUpDTO)
        {
            var slotAllocationMaps = new List<LmsSlotAllocationMapDTO>();

            foreach (var allocMap in slotMap.SignupSlotAllocationMaps)
            {
                allocMap.SignupSlotRemarkMap = LmsSlotRemarkMapMapper.Mapper(Context).GetSlotDetailsByAllocationID(allocMap.SignupSlotAllocationMapIID);

                if (allocMap.SignupSlotRemarkMap.SignupSlotRemarkMapIID == 0)
                {
                    allocMap.SignupSlotRemarkMap = FillSlotAllocationRemarksMapDetails(signUpDTO, slotMap, allocMap);
                }

                slotAllocationMaps.Add(allocMap);
            }

            return slotAllocationMaps;
        }

        private LmsSlotRemarkMapDTO FillSlotAllocationRemarksMapDetails(LmsDTO signUpDTO, LmsSlotMapDTO slotMap, LmsSlotAllocationMapDTO allocMap = null)
        {
            var slotAllocationRemarkMap = new LmsSlotRemarkMapDTO()
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

        public OperationResultDTO SaveSelectedSignUpSlot(LmsSlotMapDTO signUpSlotMap)
        {
            var returnResult = new OperationResultDTO();

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);

            var slotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            var signUpAllocationDTO = new LmsSlotAllocationMapDTO()
            {
                SignupSlotAllocationMapIID = 0,
                SignupSlotMapID = signUpSlotMap.SignupSlotMapIID,
                ParentID = signUpSlotMap.ParentID.HasValue ? signUpSlotMap.ParentID : parentDetail?.ParentIID,
                StudentID = signUpSlotMap.StudentID.HasValue ? signUpSlotMap.StudentID : (long?)null,
                EmployeeID = signUpSlotMap.EmployeeID.HasValue ? signUpSlotMap.EmployeeID : (long?)null,
                SchoolID = signUpSlotMap.SchoolID.HasValue ? signUpSlotMap.SchoolID : (byte?)null,
                AcademicYearID = signUpSlotMap.AcademicYearID.HasValue ? signUpSlotMap.AcademicYearID : (int?)null,
                SlotMapStatusID = slotMapStatsID,
                GuardianEmailID = parentDetail?.GaurdianEmail,
                SlotDateString = signUpSlotMap.SlotDateString,
                SlotTimeString = signUpSlotMap.StartTimeString,
                SignupOrganizerEmployeeName = signUpSlotMap.SignupOrganizerEmployeeName,
            };

            try
            {
                var result = LmsMapper.Mapper(Context).SaveSelectedSignUpSlot(signUpAllocationDTO);

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
                    LmsMapper.Mapper(Context).UpdateSignupSlotMapStatus(signUpSlotMap.SignupSlotMapIID, slotMapStatsID);

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

        public List<LmsGroupDTO> GetActiveSignupGroups()
        {
            var returnSignupGroups = new List<LmsGroupDTO>();

            var parentDetail = ParentMapper.Mapper(Context).GetParentDetailsByLoginID(Context.LoginID);
            var signupGroups = LmsGroupMapper.Mapper(Context).GetActiveSignupGroups();

            var parentID = parentDetail != null ? parentDetail.ParentIID : 0;

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var parentStudents = StudentMapper.Mapper(Context).GetStudentsDetailsByParent(parentID);

            if (signupGroups != null && signupGroups.Count > 0)
            {
                foreach (var signupGroup in signupGroups)
                {
                    var signUpDTOList = LmsMapper.Mapper(Context).FillSignUpDetailsByGroupID(signupGroup.SignupGroupID);

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

        public OperationResultDTO CancelSelectedSignUpSlot(LmsSlotMapDTO signUpSlotMap)
        {
            var returnResult = new OperationResultDTO();

            var openedSlotMapStatsID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);

            try
            {
                var result = LmsMapper.Mapper(Context).CancelSelectedSignUpSlot(signUpSlotMap);

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
                    LmsMapper.Mapper(Context).UpdateSignupSlotMapStatus(signUpSlotMap.SignupSlotMapIID, openedSlotMapStatsID);

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

        public List<LmsDTO> GetEmployeesActiveSignups(long employeeID)
        {
            var signUpDTOs = new List<LmsDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signupDTOList = LmsMapper.Mapper(Context).GetActiveSignUpDetailsByEmployeeID(employeeID);

            foreach (var signUpDTO in signupDTOList)
            {
                if (signUpDTO.SignupStatusID == signupPublishedStatusID)
                {
                    signUpDTO.IsExpand = true;

                    if (signUpDTO.SignupSlotMaps.Count > 0)
                    {
                        FillSignUpSlotMapDetails(signUpDTO, null);

                        signUpDTO.SignupSlotMaps = signUpDTO.SignupSlotMaps
                            .OrderByDescending(o => o.SlotDate == DateTime.Today) // Place today's date at the top
                            .ThenByDescending(o => o.SlotDate) // Then order remaining items by date descending
                            .ToList();
                    }

                    signUpDTOs.Add(signUpDTO);
                }
            }

            return signUpDTOs;
        }

        public List<LmsDTO> GetMeetingRequestSlotsByEmployeeID(long employeeID, string reqSlotDateString, long? studentID = null, int? classID = null, int? sectionID = null)
        {
            var signUpDTOs = new List<LmsDTO>();

            var signupPublishedStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_STATUSID_PUBLISH", 3);

            var signUpDTOList = LmsMapper.Mapper(Context).GetActiveMeetingRequestDetailsByEmployeeID(employeeID);

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

        private void GetSignUpSlotMapForRequest(LmsDTO signUpDTO, LmsGroupDTO groupDTO = null, string reqSlotDateString = null)
        {
            var currentDate = DateTime.Now;
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var cancelSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);
            var closedSlotStatusID = new Domain.Setting.SettingBL(Context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CLOSED", 4);

            var groupedSlots = signUpDTO.SignupSlotMaps.GroupBy(g => g.SlotDate.Value.Date).ToList().OrderBy(x => x.Key);

            signUpDTO.SignupSlotMaps = new List<LmsSlotMapDTO>();

            var reqDate = string.IsNullOrEmpty(reqSlotDateString) ? (DateTime?)null : DateTime.ParseExact(reqSlotDateString, dateFormat, CultureInfo.InvariantCulture);

            foreach (var groupedSlot in groupedSlots)
            {
                if (groupedSlot.Key == reqDate)
                {
                    var timeMaps = new List<LmsSlotMapDTO>();

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
                        if (slotMap.SlotMapStatusID != cancelSlotStatusID || slotMap.SlotMapStatusID != closedSlotStatusID)
                        {
                            timeMaps.Add(new LmsSlotMapDTO()
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

                    signUpDTO.SignupSlotMaps.Add(new LmsSlotMapDTO()
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

        public LessonPlanDTO GetLessonPlanByLessonID(long LessonPlanID)
        {
            return LessonPlanMapper.Mapper(Context)
                 .GetLessonPlanByLessonID(LessonPlanID);
        }

        public List<SubjectWiseLessonPlanDTO> GetLessonPlanListBySubject(long studentID)
        {
            return LessonPlanMapper.Mapper(Context)
                 .GetLessonPlanListBySubject(studentID);
        }

        public AssignmentDTO GetAssignmentByAssignmentID(long AssignmentID)
        {
            return AssignmentMapper.Mapper(Context)
                 .GetAssignmentByAssignmentID(AssignmentID);
        }


        public OperationResultDTO SubmitStudentAssignment(AssignmentDTO assignmentDTO)
        {
            var returnResult = new OperationResultDTO();

            if (assignmentDTO == null)
            {
                return new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "Assignment data is null."
                };
            }

            // Ensure StudentAssignmentMaps is initialized
            if (assignmentDTO.StudentAssignmentMaps == null || !assignmentDTO.StudentAssignmentMaps.Any())
            {
                return new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = "No student assignment data to save."
                };
            }

            var studentAssignmentDTO = new StudentAssignmentDTO()
            {
                AssignmentID = assignmentDTO.AssignmentIID,
                AssignmentName = assignmentDTO.Title,
                StudentId = (long?)assignmentDTO.Student,
                ClassID = assignmentDTO.ClassID,
                SectionID = assignmentDTO.SectionID,
                SubmissionDate = assignmentDTO.DateOfSubmission,
                SubmittedFilePath = assignmentDTO.AssignmentAttachmentMaps?.FirstOrDefault()?.FilePath,
                SubmittedFileName = assignmentDTO.AssignmentAttachmentMaps?.FirstOrDefault()?.FileName,
                Remarks = assignmentDTO.Description,

                // ✅ Ensure `StudentAssignmentMaps` is properly assigned
                StudentAssignmentMaps = assignmentDTO.StudentAssignmentMaps.Select(map => new StudentAssignmentMapDTO
                {
                    StudentId = map.StudentId ?? (long?)assignmentDTO.Student,  // Ensure Student ID is set
                    AssignmentID = assignmentDTO.AssignmentIID,
                    AttachmentName = map.AttachmentName,
                    Description = map.Description,
                    Notes = map.Notes,
                    AttachmentReferenceId = map.AttachmentReferenceId,
                    Remarks = map.Remarks,
                    DateOfSubmission = DateTime.Now,
                    AssignmentStatusID = map.AssignmentStatusID ?? 1, // Default status if missing
                    CreatedBy = map.CreatedBy ?? (int?)Context.LoginID,
                    UpdatedBy = map.UpdatedBy ?? (int?)Context.LoginID,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }).ToList()
            };

            try
            {
                var result = AssignmentMapper.Mapper(Context).SaveStudentAssignment(studentAssignmentDTO);

                if (result == 0)
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = "Saving failed!",
                    };
                }
                else
                {
                    returnResult = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Assignment submitted successfully!",
                    };
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;

                returnResult = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage,
                };
            }

            return returnResult;
        }



    }
}