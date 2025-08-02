using Eduegate.Domain;
using Eduegate.Domain.Entity.OnlineExam.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Accounts;
using Eduegate.Domain.Mappers.Catalog;
using Eduegate.Domain.Mappers.HR.Payroll;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notifications;
using Eduegate.Domain.Mappers.OnlineExam.Exam;
using Eduegate.Domain.Mappers.Payment;
using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.Mappers.School.Accounts;
using Eduegate.Domain.Mappers.School.Attendences;
using Eduegate.Domain.Mappers.School.Circulars;
using Eduegate.Domain.Mappers.School.CounselorHub;
using Eduegate.Domain.Mappers.School.Exams;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Domain.Mappers.School.Fines;
using Eduegate.Domain.Mappers.School.Galleries;
using Eduegate.Domain.Mappers.School.Inventory;
using Eduegate.Domain.Mappers.School.LedgerAccount;
using Eduegate.Domain.Mappers.School.Library;
using Eduegate.Domain.Mappers.School.Mutual;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Domain.Mappers.School.Transports;
using Eduegate.Domain.Payment;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Accounts;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Services.Contracts.School.Circulars;
using Eduegate.Services.Contracts.School.CounselorHub;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Services.Contracts.School.Galleries;
using Eduegate.Services.Contracts.School.Inventory;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Payment;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.Schools;
using Eduegate.Services.Contracts.Settings;
using UglyToad.PdfPig.Content;

namespace Eduegate.Services.Schools
{
    public class SchoolService : BaseService, ISchoolService
    {
        public OperationResult DeleteApplication(long applicationId)
        {
            try
            {
                StudentApplicationMapper.Mapper(CallContext)
                    .DeleteApplication(applicationId);
                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Error;
            }
        }

        public OperationResult DeleteSiblingApplication(long applicationId)
        {
            try
            {
                StudentApplicationMapper.Mapper(CallContext)
                    .DeleteSiblingApplication(applicationId);
                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Error;
            }
        }

        public StudentApplicationDTO GetApplication(long applicationId)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetApplication(applicationId);
        }

        public List<StudentApplicationDTO> GetStudentApplication(long loginID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetStudentApplicationsByLogin(loginID);
        }

        public StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date)
        {
            return StudentAttendenceMapper.Mapper(CallContext).GetStudentAttendence(studentID, date);
        }

        public StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date)
        {
            return StaffAttendenceMapper.Mapper(CallContext).GetStaffAttendence(staffID, date);
        }

        public string SaveStudentAttendence(StudentAttendenceDTO attendence)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SaveStudentAttendence(attendence);
        }

        public string SaveAcademicCalendar(AcadamicCalendarDTO acadamic)
        {
            return AcademicCalendarMapper.Mapper(CallContext).SaveEntity(acadamic);
        }

        public string SaveStaffAttendance(StaffAttendenceDTO attendence)
        {
            return StaffAttendenceMapper.Mapper(CallContext).SaveStaffAttendance(attendence);
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).GetStudentAttendenceByYearMonth(month, year, classId, sectionId);
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).GetStudentAttendenceByYearMonthStudentId(month, year, studentId);
        }

        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            return StaffAttendenceMapper.Mapper(CallContext).GetStaffAttendanceByMonthYear(month, year);
        }

        public List<PresentStatusDTO> GetPresentStatuses()
        {
            return StudentAttendenceMapper.Mapper(CallContext).GetPresentStatuses();
        }

        //public List<ClassSubjectMapDTO> GetSubjectByClassID(int classID)
        //{
        //    return ClassSubjectMapMapper.Mapper(CallContext).GetSubjectByClassID(classID);
        //}

        public string SaveTimeTable(TimeTableAllocationDTO timeTableAlloc)
        {
            return TimeTableAllocationMapper.Mapper(CallContext).SaveEntity(timeTableAlloc);
        }

        public string SaveTimeTableLog(TimeTableLogDTO timeTableLog)
        {
            return TimeTableLogMapper.Mapper(CallContext).SaveEntity(timeTableLog);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByClassID(int classID, int tableMasterId)
        {
            return TimeTableAllocationMapper.Mapper(CallContext).GetTimeTableByClassID(classID, tableMasterId);
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByDate(int classID, int tableMasterId, DateTime timeTableDate)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetTimeTableByDate(classID, tableMasterId, timeTableDate);
        }

        public void DeleteDailyTimeTableEntry(long timeTableLogID)
        {
            TimeTableLogMapper.Mapper(CallContext).DeleteEntity(timeTableLogID);
        }
        public void DeleteTimeTableEntry(long timeTableAllocationID)
        {
            TimeTableAllocationMapper.Mapper(CallContext).DeleteTimeTableEntry(timeTableAllocationID);
        }
        public List<SubjectTeacherMapDTO> GetTeacherBySubject(int classId, int SectionId, int subjectId)
        {
            return SubjectTeacherMapMapper.Mapper(CallContext).GetTeacherBySubject(classId, SectionId, subjectId);
        }
        public string SaveStudentFeeDue(StudentFeeDueDTO feeDueInfo)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).SaveEntity(feeDueInfo);
        }

        public string SaveStudentApplication(StudentApplicationDTO studentApplication)
        {
            return StudentApplicationMapper.Mapper(CallContext).SaveStudentApplication(studentApplication);
        }

        public string SendTodayAttendancePushNotification(int classId, int sectionId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SendTodayAttendancePushNotification(classId, sectionId);
        }

        public string ResentFromLeadLoginCredentials(StudentApplicationDTO studentApplication)
        {
            return StudentApplicationMapper.Mapper(CallContext).ResentFromLeadLoginCredentials(studentApplication);
        }

        public OperationResultDTO GenerateTimeTable(TimeTableAllocationDTO timeTableLog)
        {
            return TimeTableLogMapper.Mapper(CallContext).GenerateTimeTable(timeTableLog);
        }

        public List<StudentFeeDueDTO> FillFeeDue(int classId, long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillFeeDue(classId, studentId);
        }

        public List<FeeDuePaymentDTO> FillFeePaymentDetails(long loginId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillFeePaymentDetails(loginId);
        }

        public List<StudentFeeDueDTO> FillPendingFees(int classId, long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillPendingFees(classId, studentId);
        }
        public List<KeyValueDTO> GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetInvoiceForCreditNote(classId, studentId, feeMasterID, feePeriodID);
        }
        public List<FeeDueMonthlySplitDTO> GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetFeeDueMonthlyDetails(studentFeeDueID, feeMasterID, feePeriodID);
        }
        public List<StudentFeeDueDTO> GetFeesByInvoiceNo(long studentFeeDueID)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetFeesByInvoiceNo(studentFeeDueID);
        }
        public List<KeyValueDTO> GetClassStudents(int classId, int sectionId)
        {
            return StudentMapper.Mapper(CallContext).GetClassStudents(classId, sectionId);
        }

        public List<KeyValueDTO> GetClassStudentsAll(int academicYearID, List<int> classList, List<int> sectionList)
        {
            return StudentMapper.Mapper(CallContext).GetClassStudentsAll(academicYearID, classList, sectionList);
        }

        public List<AcademicYearDTO> GetAcademicYearDataByCalendarID(long calendarID)
        {
            return new AcademicCalendarMapper().GetAcademicYearDataByCalendarID(calendarID);
        }

        public List<AcademicYearDTO> GetAcademicYear(int academicYearID)
        {
            return new AcademicYearMapper().GetAcademicYear(academicYearID);
        }

        public List<StudentDTO> GetClasswiseStudentData(int classId, int sectionId)
        {
            return StudentMapper.Mapper(CallContext).GetClasswiseStudentData(classId, sectionId);
        }

        public List<RemarksEntryStudentsDTO> GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {
            return RemarksEntryMapper.Mapper(CallContext).GetClasswiseRemarksEntryStudentData(classId, sectionId, examGroupID);
        }

        public List<HealthEntryStudentMapDTO> GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {
            return HealthEntryMapper.Mapper(CallContext).GetHealthEntryStudentData(classId, sectionId, academicYearID, examGroupID);
        }

        public List<EventTransportAllocationMapDTO> GetStudentandStaffsByRouteIDforEvent(EventTransportAllocationDTO eventDto)
        {
            return EventTransportAllocationMapper.Mapper(CallContext).GetStudentandStaffsByRouteIDforEvent(eventDto);
        }

        public EventTransportAllocationMapDTO GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetStudentTransportDetailsByStudentID(studentID, IsRouteType);
        }

        public EventTransportAllocationMapDTO GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType)
        {
            return StaffRouteStopMapMapper.Mapper(CallContext).GetStaffTransportDetailsByStaffID(staffID, IsRouteType);
        }

        public List<RouteShiftingStudentMapDTO> GetStudentDatasFromRouteID(int routeId)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetStudentDatasFromRouteID(routeId);
        }

        public List<ClassSectionSubjectListMapDTO> FillClassandSectionWiseSubjects(int classID, int sectionID, int IID)
        {
            return ClassSubjectMapMapper.Mapper(CallContext).FillClassandSectionWiseSubjects(classID, sectionID, IID);
        }

        public List<RouteShiftingStaffMapDTO> GetStaffDatasFromRouteID(int routeId)
        {
            return StaffRouteStopMapMapper.Mapper(CallContext).GetStaffDatasFromRouteID(routeId);
        }

        public List<CampusTransferMapDTO> GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {
            return CampusTrasnsferMapper.Mapper(CallContext).GetClasswiseStudentDataForCampusTransfer(classId, sectionId);

        }


        public List<KeyValueDTO> GetAdvancedAcademicYearBySchool(int schoolID)
        {
            return StudentMapper.Mapper(CallContext).GetAdvancedAcademicYearBySchool(schoolID);
        }


        public List<StudentDTO> GetStudentsSiblings(long parentId)
        {
            return StudentMapper.Mapper(CallContext).GetStudentsSiblings(parentId);
        }

        public List<GalleryDTO> GetGalleryView(long academicYearID)
        {
            return GalleryMapper.Mapper(CallContext).GetGalleryView(academicYearID);
        }

        public List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            return AgeCriteriaMapper.Mapper(CallContext).GetAgeCriteriaByClassID(classId, academicYearID);
        }

        public LibraryBookDTO GetBooKCategoryName(long bookCategoryCodeId)
        {
            return LibraryBookMapper.Mapper(CallContext).GetBooKCategoryName(bookCategoryCodeId);
        }

        public LibraryTransactionDTO GetBookQuantityDetails(string CallAccNo, int? bookMapID)
        {
            return LibraryTransactionMapper.Mapper(CallContext).GetAvailableBookDetails(CallAccNo, bookMapID);
        }

        public LibraryTransactionDTO GetIssuedBookDetails(string CallAccNo, int? bookMapID)
        {
            return LibraryTransactionMapper.Mapper(CallContext).GetIssuedLibraryBookDetails(CallAccNo, bookMapID);
        }

        public List<KeyValueDTO> GetBookDetailsByCallNo(string CallAccNo)
        {
            return LibraryTransactionMapper.Mapper(CallContext).GetBookDetailsByCallNo(CallAccNo);
        }

        public List<CircularListDTO> GetCircularList(long parentId)
        {
            return CircularMapper.Mapper(CallContext).GetCircularList(parentId);
        }

        public List<CounselorHubListDTO> GetCounselorList(long parentId)
        {
            return CounselorHubMapper.Mapper(CallContext).GetCounselorList(parentId);
        }
        public List<CircularListDTO> GetCircularListByStudentID(long studentID)
        {
            return CircularMapper.Mapper(CallContext).GetCircularListByStudentID(studentID);
        }
        public List<AgendaDTO> GetAgendaList(long loginID)

        {
            return AgendaTopicMapper.Mapper(CallContext).GetAgendaList(loginID);
        }

        public List<LessonPlanDTO> GetLessonPlanList(long studentID)
        {
            return LessonPlanMapper.Mapper(CallContext).GetLessonPlanList(studentID);
        }

        public List<FeeCollectionDTO> GetStudentFeeCollection(long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetStudentFeeCollection(studentId);
        }


        public StudentRouteStopMapDTO GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetDropBusSeatAvailabilty(RouteStopMapId, academicYearID);
        }

        public StudentRouteStopMapDTO GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetPickUpBusSeatAvailabilty(RouteStopMapId, academicYearID);
        }

        public StaffRouteStopMapDTO GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return StaffRouteStopMapMapper.Mapper(CallContext).GetDropBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
        }

        public StaffRouteStopMapDTO GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            return StaffRouteStopMapMapper.Mapper(CallContext).GetPickUpBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
        }


        public List<Contracts.School.Academics.AssignmentDTO> GetAssignmentStudentwise(long studentId, int? SubjectID)
        {
            return AssignmentMapper.Mapper(CallContext).GetAssignmentStudentwise(studentId, SubjectID);
        }

        public List<MarkListViewDTO> GetMarkListStudentwise(long studentId)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetMarkListStudentwise(studentId);
        }

        public ExamDTO GetClassByExam(int examId)
        {
            return ExamMapper.Mapper(CallContext).GetClassByExam(examId);
        }

        public List<FeePeriodsDTO> GetFeePeriodMonthlySplit(List<int> feePeriodIds)
        {
            return FeePeriodsMapper.Mapper(CallContext).GetFeePeriodMonthlySplit(feePeriodIds);
        }

        public List<MarkRegisterDetailsSplitDTO> GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetExamSubjectsMarks(studentId, examId, ClassId);
        }
        public List<MarkGradeMapDTO> GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetGradeByExamSubjects(examId, classId, subjectID, typeId);
        }
        public List<CollectFeeAccountDetailDTO> GetCollectFeeAccountData(DateTime fromDate, DateTime toDate, long CashierID)
        {
            return CollectFeeAccountPostingMapper.Mapper(CallContext).GetCollectFeeAccountData(fromDate, toDate, CashierID);
        }
        public List<StudentDTO> GetStudentDetails(long studentId)
        {
            return StudentMapper.Mapper(CallContext).GetStudentDetails(studentId);
        }

        public List<StudentTransportDetailDTO> GetStudentTransportDetails(long studentID)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetStudentTransportDetails(studentID);
        } 

        public TransportCancellationDTO GetStudentTransportCancellationDetails(long studentID)
        {
            return TransportCancellationMapper.Mapper(CallContext).GetStudentTransportCancellationDetails(studentID);
        } 
        
        public OperationResultDTO RevertTransportCancellation(long RequestIID)
        {
            return TransportCancellationMapper.Mapper(CallContext).RevertTransportCancellation(RequestIID);
        }

        public List<StudentBehavioralRemarksDTO> GetStudentBehavioralRemarks(StudentBehavioralRemarksDTO studentBehavioralRemarksDTO)
        {
            return RemarksEntryMapper.Mapper(CallContext).GetStudentBehavioralRemarks(studentBehavioralRemarksDTO);
        }
        public TransportApplicationDTO GetTransportStudentDetailsByParentLoginID(long id)
        {
            return TransportApplicationMapper.Mapper(CallContext).GetTransportStudentDetailsByParentLoginID(id);
        }

        public string FeeAccountPosting(DateTime fromDate, DateTime toDate, long cashierID)
        {
            return CollectFeeAccountPostingMapper.Mapper(CallContext).SaveFeeAccount(fromDate, toDate, cashierID);
        }

        public StudentLeaveApplicationDTO GetLeaveApplication(long leaveapplicationId)
        {
            return StudentLeaveApplicationMapper.Mapper(CallContext).GetLeaveApplication(leaveapplicationId);
        }
        public List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long leaveapplicationId)
        {
            return StudentLeaveApplicationMapper.Mapper(CallContext).GetStudentLeaveApplication(leaveapplicationId);
        }

        public OperationResult DeleteLeaveApplication(long leaveapplicationId)
        {
            try
            {
                StudentLeaveApplicationMapper.Mapper(CallContext).DeleteLeaveApplication(leaveapplicationId);
                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Error;
            }
        }

        public List<StudentFeeDueDTO> GetFineCollected(long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetFineCollected(studentId);
        }

        public List<StudentFeeDueDTO> FillFineDue(int classId, long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillFineDue(classId, studentId);
        }

        public List<TimeTableAllocationDTO> GetClassTimeTable(long studentId)
        {
            return TimeTableAllocationMapper.Mapper(CallContext).GetClassTimeTable(studentId);
        }

        public List<ExamDTO> GetExamLists(long studentId)
        {
            return ExamMapper.Mapper(CallContext).GetExamLists(studentId);
        }

        public StudentTransferRequestDTO GetTransferApplication(long studentId)
        {
            return StudentTransferRequestMapper.Mapper(CallContext).GetTransferApplication(studentId);
        }
        public List<StudentTransferRequestDTO> GetStudentTransferApplication(long studentId)
        {
            return StudentTransferRequestMapper.Mapper(CallContext).GetStudentTransferApplication(studentId);
        }

        public OperationResult DeleteTransferApplication(long studentId)
        {
            try
            {
                StudentTransferRequestMapper.Mapper(CallContext).DeleteTransferApplication(studentId);
                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Error;
            }
        }

        public StudentApplicationDTO GetApplicationByLoginID(long loginID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetApplicationByLoginID(loginID);
        }
        public List<NotificationAlertsDTO> GetNotificationAlerts(long loginID, int page, int pageSize)
        {
            return NotificationAlertMapper.Mapper(CallContext).GetNotificationAlerts(loginID , page,  pageSize);
        }

        public List<NotificationAlertsDTO> GetAllNotificationAlerts(long loginID)
        {
            return NotificationAlertMapper.Mapper(CallContext).GetAllNotificationAlerts(loginID);
        }

        public List<MailBoxDTO> GetSendMailFromParent(long loginID)
        {
            return NotificationAlertMapper.Mapper(CallContext).GetSendMailFromParent(loginID);
        }

        public int GetNotificationAlertsCount(long loginID)
        {
            return NotificationAlertMapper.Mapper(CallContext).GetNotificationAlertsCount(loginID);
        }

        public string MarkNotificationAsRead(long loginID, long notificationAlertIID)
        {
            return NotificationAlertMapper.Mapper(CallContext).MarkNotificationAsRead(loginID, notificationAlertIID);
        }

        public List<ClassClassTeacherMapDTO> GetTeacherDetails(long studentId)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetTeacherDetails(studentId);
        }



        public List<FeeCollectionDTO> GetSiblingDueDetailsFromStudentID(long StudentID)
        {
            return FeeCollectionMapper.Mapper(CallContext).GetSiblingDueDetailsFromStudentID(StudentID);
        }

        public List<StudentSkillRegisterSplitDTO> GetStudentSkills(long skillId)
        {
            return StudentSkillRegisterMapper.Mapper(CallContext).GetStudentSkills(skillId);
        }

        public List<MarkGradeMapDTO> GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            return StudentSkillRegisterMapper.Mapper(CallContext).GetGradeByExamSkill(examId, skillId, subjectId, classId, markGradeID);
        }

        public ProgressReportDTO GetStudentSkillRegister(long studentId, int ClassID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetStudentSkillRegister(studentId, ClassID);
        }

        public ProductBundleDTO GetProductDetails(long productId)
        {
            return QuickCatalogMapper.Mapper(CallContext).GetProductDetails(productId);
        }

        public ProductBundleDTO GetProductBundleData(long productskuId)
        {
            return QuickCatalogMapper.Mapper(CallContext).GetProductBundleData(productskuId);
        }

        public EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        {
            return EmployeeTimeSheetsWeeklyMapper.Mapper(CallContext).GetCollectTimeSheetsData(employeeID, fromDate, toDate);
        }

        public FineMasterStudentMapDTO GetFineAmount(int fineMasterID)
        {
            return FineMasterStudentMapMapper.Mapper(CallContext).GetFineAmount(fineMasterID);
        }

        public List<FeeDueFeeTypeMapDTO> FillFeeDueForSettlement(long studentId, int academicId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillFeeDueForSettlement(studentId, academicId);
        }
        public List<FeeCollectionDTO> FillFeeDueDataForSettlement(long studentId, int academicId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).FillFeeDueDataForSettlement(studentId, academicId);
        }

        public List<FeeCollectionPreviousFeesDTO> GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetIssuedCreditNotesForCollectedFee(studentId);
        }

        public EmployeesDTO GetEmployeeFromEmployeeID(long employeeID)
        {
            return EmployeeTimeSheetsWeeklyMapper.Mapper(CallContext).GetEmployeeFromEmployeeID(employeeID);
        }

        public AccountsGroupDTO GetGroupCodeByParentGroup(long parentGroupID)
        {
            return AccountGroupMapper.Mapper(CallContext).GetGroupCodeByParentGroup(parentGroupID);
        }

        public AccountsDTO GetAccountCodeByGroup(long groupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetAccountCodeByGroup(groupID);
        }

        public List<CurrencyDTO> GetCurrencyDetails()
        {
            return CurrencyMapper.Mapper(CallContext).GetCurrencyDetails();
        }
        public StudentDTO GetStudentDetailFromStudentID(long StudentID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentDetailFromStudentID(StudentID);
        }

        public List<FeeCollectionDTO> AutoReceiptAccountTransactionSync(long accountTransactionHeadIID, long referenceID, int loginId, int type, int isDueAsNegative)
        {
            return FeeCollectionMapper.Mapper(CallContext).AutoReceiptAccountTransactionSync(accountTransactionHeadIID, referenceID, loginId, type, isDueAsNegative);
        }
        public SchoolCreditNoteDTO AutoCreditNoteAccountTransactionSync(long accountTransactionHeadIID, long studentID, int loginId, int type)
        {
            return CreditNoteMapper.Mapper(CallContext).AutoCreditNoteAccountTransactionSync(accountTransactionHeadIID, studentID, loginId, type);
        }
        public GuardianDTO GetParentDetailFromParentID(long ParentID)
        {
            return StudentMapper.Mapper(CallContext).GetParentDetailFromParentID(ParentID);
        }

        public List<KeyValueDTO> GetFeeStructure(int academicYearID)
        {
            return FeeStructureMapper.Mapper(CallContext).GetFeeStructure(academicYearID);
        }

        public List<KeyValueDTO> GetSubGroup(int mainGroupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetSubGroup(mainGroupID);
        }

        public List<KeyValueDTO> GetAccountCodeByLedger(int ledgerGroupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetAccountCodeByLedger(ledgerGroupID);
        }

        public List<KeyValueDTO> GetAccountGroup(int subGroupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetAccountGroup(subGroupID);
        }

        public List<KeyValueDTO> GetAccountByGroupID(int groupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetAccountByGroupID(groupID);
        }

        public List<KeyValueDTO> GetCostCenterByAccount(long accountID)
        {
            return CostCenterMapper.Mapper(CallContext).GetCostCenterByAccount(accountID);
        }

        public List<KeyValueDTO> GetAccountByPayementModeID(long paymentModeID)
        {
            return AccountTransactionHeadMapper.Mapper(CallContext).GetAccountByPayementModeID(paymentModeID);
        }

        public List<KeyValueDTO> GetAccountGroupByAccountID(long accountID)
        {
            return AccountTransactionHeadMapper.Mapper(CallContext).GetAccountGroupByAccountID(accountID);
        }

        public List<KeyValueDTO> GetFeePeriod(int academicYearID, long studentID, int? feeMasterID)
        {
            return FeeStructureMapper.Mapper(CallContext).GetFeePeriod(academicYearID, studentID, feeMasterID);
        }

        public List<KeyValueDTO> GetTransportFeePeriod(int academicYearID)
        {
            return FeePeriodsMapper.Mapper(CallContext).GetTransportFeePeriod(academicYearID);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID)
        {
            return AcademicCalendarMapper.Mapper(CallContext).GetAcademicCalenderByAcademicYear(academicYearID, year, academicCalendarStatusID, academicCalendarID);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            return AcademicCalendarMapper.Mapper(CallContext).GetAcademicCalenderByMonthYear(month, year);
        }

        public void DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            AcademicCalendarMapper.Mapper(CallContext).DeleteEntity(academicYearCalendarEventIID, academicYearCalendarID);
        }

        public int GetStudentsCount()
        {
            return StudentMapper.Mapper(CallContext).GetStudentsCount();
        }

        public List<KeyValueDTO> GetCastByRelegion(int relegionID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetCastByRelegion(relegionID);
        }

        public List<KeyValueDTO> GetToNotificationUsersByUser(int userID, int branchID, string user)
        {
            return PushNotificationMapper.Mapper(CallContext).GetToNotificationUsersByUser(userID, branchID, user);
        }

        public List<KeyValueDTO> GetStreamByStreamGroup(byte? streamGroupID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetStreamByStreamGroup(streamGroupID);
        }

        public List<KeyValueDTO> GetStreamCompulsorySubjects(byte? streamID)
        {
            return StreamSubjectMapMapper.Mapper(CallContext).GetStreamCompulsorySubjects(streamID);
        }

        public List<KeyValueDTO> GetStreamOptionalSubjects(byte? streamID)
        {
            return StreamSubjectMapMapper.Mapper(CallContext).GetStreamOptionalSubjects(streamID);
        }

        public List<StreamDTO> GetFullStreamListDatas()
        {
            return StreamSubjectMapMapper.Mapper(CallContext).GetFullStreamListDatas();
        }

        public List<KeyValueDTO> GetSubSkillByGroup(int skillGroupID)
        {
            return ClassSubjectSkillGroupMapMapper.Mapper(CallContext).GetSubSkillByGroup(skillGroupID);
        }

        public List<KeyValueDTO> GetAcademicYearBySchool(int schoolID, bool? isActive)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetAcademicYearBySchool(schoolID,isActive);
        }

        public List<KeyValueDTO> GetClassesByAcademicYearID(int academicYearID)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetClassesByAcademicYearID(academicYearID);
        }

        public List<KeyValueDTO> GetClassesBySchool(byte schoolID)
        {
            return ClassMapper.Mapper(CallContext).GetClassesBySchool(schoolID);
        }

        public List<KeyValueDTO> GetSectionsBySchool(byte schoolID)
        {
            return StudentMapper.Mapper(CallContext).GetSectionsBySchool(schoolID);
        } 
        
        public List<ClassDTO> GetClassListBySchool(byte schoolID)
        {
            return ClassMapper.Mapper(CallContext).GetClassListBySchool(schoolID);
        }

        public string GetProgressReportName(long schoolID, int? classID)
        {
            return StudentMapper.Mapper(CallContext).GetProgressReportName(schoolID, classID);
        }

        public List<KeyValueDTO> GetRouteStopsByRoute(int routeID)
        {
            return StudentRouteStopMapMapper.Mapper(CallContext).GetRouteStopsByRoute(routeID);
        }

        public List<KeyValueDTO> GetSubjectByClass(int classID)
        {
            return ClassSubjectMapMapper.Mapper(CallContext).GetSubjectByClass(classID);
        }

        public List<KeyValueDTO> GetSubjectbyQuestionGroup(long questionGroupID)
        {
            return OnlineQuestionGroupsMapper.Mapper(CallContext).GetSubjectbyQuestionGroup(questionGroupID);
        }

        public OnlineExamQuestionDTO GetQuestionDetailsByQuestionID(long questionID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetQuestionDetailsByQuestionID(questionID);
        }

        public StaticContentDataDTO GetAboutandContactDetails(long contentID)
        {
            return StaticContentDataMapper.Mapper(CallContext).GetAboutandContactDetails(contentID);
        }

        public LibraryTransactionDTO GetLibraryStudentFromStudentID(long studentID)
        {
            return LibraryTransactionMapper.Mapper(CallContext).GetLibraryStudentFromStudentID(studentID);
        }

        public LibraryTransactionDTO GetLibraryStaffFromEmployeeID(long employeeID)
        {
            return LibraryTransactionMapper.Mapper(CallContext).GetLibraryStaffFromEmployeeID(employeeID);
        }

        public TransportApplicationDTO GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            return TransportApplicationMapper.Mapper(CallContext).GetStudentTransportApplication(TransportApplctnStudentMapIID);
        }

        public List<SubjectMarkEntryDetailDTO> FillClassStudents(long classID, long sectionID)
        {
            return SubjectMarkEntryMapper.Mapper(CallContext).FillClassStudents(classID, sectionID);
        }

        public List<SubjectMarkEntryDetailDTO> GetSubjectsMarkData(long examID, int classID, int sectionID, int subjectID)
        {
            return SubjectMarkEntryMapper.Mapper(CallContext).GetSubjectsMarkData(examID, classID, sectionID, subjectID);
        }

        public ClassSubjectSkillGroupMapDTO GetExamMarkDetails(long subjectID, long examID)
        {
            return ClassSubjectSkillGroupMapMapper.Mapper(CallContext).GetExamMarkDetails(subjectID, examID);
        }

        public List<KeyValueDTO> GetTeacherEmailByParentLoginID(long loginID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetTeacherEmailByParentLoginID(loginID);
        }

        public CandidateDTO GetCandidateDetails(string username, string password)
        {
            return CandidateMapper.Mapper(CallContext).GetCandidateDetails(username, password);
        }

        public GuardianDTO GetGuardianDetails(long studentId)
        {
            return StudentMapper.Mapper(CallContext).GetGuardianDetails(studentId);
        }

        public string ShiftStudentSection(ClassSectionShiftingDTO toDto)
        {
            return StudentMapper.Mapper(CallContext).ShiftStudentSection(toDto);
        }

        public ClassSectionShiftingDTO GetStudentsForShifting(int classID, int sectionID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentsForShifting(classID, sectionID);
        }

        public List<KeyValueDTO> GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetClassWiseExamGroup(classID, sectionID, academicYearID);
        }
        public List<KeyValueDTO> GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillSetByClassExam(classID, sectionID, examID, academicYearID, languageTypeID);
        }
        public List<KeyValueDTO> GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSubjectsBySkillset(markEntrySearchArgsDTO);
        }
        public List<KeyValueDTO> GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillGroupByClassExam(classID, examGroupID, skillSetID, academicYearID);
        }
        public List<KeyValueDTO> GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillsByClassExam(classID, skillGroupID, skillSetID, academicYearID);
        }

        List<KeyValueDTO> ISchoolService.GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetClassWiseExamGroup(classID, sectionID, academicYearID);
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsBySkillset(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSubjectsBySkillset(markEntrySearchArgsDTO);
        }
        List<KeyValueDTO> ISchoolService.GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillSetByClassExam(classID, sectionID, examID, academicYearID, languageTypeID);
        }
        List<KeyValueDTO> ISchoolService.GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillGroupByClassExam(classID, examGroupID, skillSetID, academicYearID);
        }
        List<KeyValueDTO> ISchoolService.GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetSkillsByClassExam(classID, skillGroupID, skillSetID, academicYearID);
        }
        public List<MarkRegisterDetailsDTO> GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetCoScholasticEntry(markEntrySearchArgsDTO);
        }
        List<MarkRegisterDetailsDTO> ISchoolService.GetCoScholasticEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetCoScholasticEntry(markEntrySearchArgsDTO);
        }
        public List<MarkRegisterDetailsDTO> GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetScholasticInternalEntry(markEntrySearchArgsDTO);
        }
        List<MarkRegisterDetailsDTO> ISchoolService.GetScholasticInternalEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetScholasticInternalEntry(markEntrySearchArgsDTO);
        }
        public string SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveCoScholasticEntry(markRegisterDTO);
        }
        string ISchoolService.SaveCoScholasticEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveCoScholasticEntry(markRegisterDTO);
        }

        public string SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveScholasticInternalEntry(markRegisterDTO);
        }
        string ISchoolService.SaveScholasticInternalEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveScholasticInternalEntry(markRegisterDTO);
        }

        public string SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveMarkEntry(markRegisterDTO);
        }
        string ISchoolService.SaveMarkEntry(MarkRegisterDTO markRegisterDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).SaveMarkEntry(markRegisterDTO);
        }
        public List<StudentMarkEntryDTO> GetMarkEntry(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkRegisterMapper.Mapper(CallContext).GetMarkEntry(markEntrySearchArgsDTO);
        }

        List<MarkRegisterDetailsDTO> ISchoolService.GetSubjectsAndMarksToPublish(MarkEntrySearchArgsDTO markEntrySearchArgsDTO)
        {
            return MarkPublishMapper.Mapper(CallContext).GetSubjectsAndMarksToPublish(markEntrySearchArgsDTO);
        }

        public List<KeyValueDTO> GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetSubjectsByClassID(argsDTO);
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsByClassID(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetSubjectsByClassID(argsDTO);
        }

        public List<KeyValueDTO> GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetSubjectsBySubjectType(argsDTO);
        }
        List<KeyValueDTO> ISchoolService.GetSubjectsBySubjectType(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetSubjectsBySubjectType(argsDTO);
        }

        public List<KeyValueDTO> GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetExamsByTermID(argsDTO);
        }
        List<KeyValueDTO> ISchoolService.GetExamsByTermID(MarkEntrySearchArgsDTO argsDTO)
        {
            return ExamMapper.Mapper(CallContext).GetExamsByTermID(argsDTO);
        }

        public List<KeyValueDTO> GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            return MarkPublishMapper.Mapper(CallContext).GetExamsByClassAndGroup(classID, sectionID, examGroupID, academicYearID);
        }  
        
        public List<KeyValueDTO> GetExamGroupsByAcademicYearID(int? academicYearID)
        {
            return ExamGroupMapper.Mapper(CallContext).GetExamGroupsByAcademicYearID(academicYearID);
        }

        public List<KeyValueDTO> GetSubjectByType(byte subjectTypeID)
        {
            return SubjectMapper.Mapper(CallContext).GetSubjectByType(subjectTypeID);
        }

        public List<KeyValueDTO> GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            return MarkPublishMapper.Mapper(CallContext).GetSkillByExamAndClass(classID, sectionID, examID, academicYearID, termID);
        }

        public string UpdateMarkEntryStatus(MarkRegisterDTO dto)
        {
            return MarkPublishMapper.Mapper(CallContext).UpdateMarkEntryStatus(dto);
        }

        public string GetAcademicYearNameByID(int academicYearID)
        {
            var academicYear = new AcademicYearMapper().GetAcademicYear(academicYearID);
            return academicYear == null || academicYear.Count() == 0 ? null : academicYear.FirstOrDefault().Description;
        }

        public string GetSchoolNameByID(int schoolID)
        {
            var school = SchoolsMapper.Mapper(CallContext).ToDTO(long.Parse(schoolID.ToString()));
            return school.SchoolName;
        }

        public List<TransportApplicationStudentMapDTO> GetTransportApplication(long loginID)
        {
            return TransportApplicationMapper.Mapper(CallContext).GetTransportApplication(loginID);
        }

        public List<SchoolGeoLocationDTO> GetGeoSchoolSetting(long schoolID)
        {
            return SchoolGeoLocationLogMapper.Mapper(CallContext).GetGeoSchoolSettingBySchoolID(schoolID);
        }

        public void ClearGeoSchoolSetting(int schoolID)
        {
            SchoolGeoLocationLogMapper.Mapper(CallContext).ClearGeoSchoolSettingBySchoolID(schoolID);
        }

        public void SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> dto)
        {
            SchoolGeoLocationLogMapper.Mapper(CallContext).SaveGeoSchoolSetting(dto);
        }

        public List<AcademicClassMapWorkingDayDTO> GetMonthAndYearByAcademicYearID(int? academicYearID)
        {
            return AcademicClassMapMapper.Mapper(CallContext).GetMonthAndYearByAcademicYearID(academicYearID);
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            return AcademicCalendarMapper.Mapper(CallContext).GetAcademicMonthAndYearByCalendarID(calendarID);
        }

        public List<FeeCollectionDTO> FillCollectedFeesDetails(long studentId, int academicId)
        {
            return RefundMapper.Mapper(CallContext).FillCollectedFeesDetails(studentId, academicId);
        }

        public string CheckAndInsertCalendarEntries(long calendarID)
        {
            return new AcademicCalendarMapper().CheckAndInsertCalendarEntries(calendarID);
        }

        public List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID)
        {
            return AcademicCalendarMasterMapper.Mapper(CallContext).GetCalendarByTypeID(calendarTypeID);
        }

        public List<AcademicCalenderEventDateDTO> GetCalendarEventsByCalendarID(long calendarID)
        {
            return AcademicCalendarMapper.Mapper(CallContext).GetCalendarEventsByCalendarID(calendarID);
        }

        public List<EmployeesDTO> GetEmployeesByCalendarID(long calendarID)
        {
            return EmployeeSalaryStructureMapper.Mapper(CallContext).GetEmployeesByCalendarID(calendarID);
        }

        public List<PresentStatusDTO> GetStaffPresentStatuses()
        {
            return StaffAttendenceMapper.Mapper(CallContext).GetStaffPresentStatuses();
        }

        //public LibraryBookDTO GetBookDetailsChange(long BookID)
        //{
        //    return LibraryTransactionMapper.Mapper(CallContext).GetBookDetailsChange(BookID);
        //}

        public List<SchoolCreditNoteDTO> GetCreditNoteNumber(long? headID, long studentID)
        {
            return CreditNoteMapper.Mapper(CallContext).GetCreditNoteNumber(headID, studentID);
        }

        public string DueFees(int? feemasterID, DateTime? invoiceDate, decimal? amount, long? studentID, long? creditNoteID)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).DueFees(feemasterID, invoiceDate, amount, studentID, creditNoteID);
        }

        public List<KeyValueDTO> GetSchoolsByParentLoginID(long? loginID)
        {
            return SchoolsMapper.Mapper(CallContext).GetSchoolsByParentLoginID(loginID);
        }

        public AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            return AcademicYearMapper.Mapper(CallContext).GetAcademicYearDataByAcademicYearID(academicYearID);
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByCandidateID(long candidateID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetQuestionsByCandidateID(candidateID);
        }

        public StudentApplicationDTO GetAgeCriteriaDetails(int? classID, int? academicID, byte? schoolID, DateTime? dob)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetAgeCriteriaDetails(classID, academicID, schoolID, dob);
        }

        public List<KeyValueDTO> GetUnitByUnitGroup(int groupID)
        {
            return UnitMapper.Mapper(CallContext).GetUnitByUnitGroup(groupID);
        }

        public List<OnlineExamResultDTO> GetOnlineExamsResultByCandidateID(long candidateID)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).GetOnlineExamsResultByCandidateID(candidateID);
        }

        public List<UnitDTO> GetUnitDataByUnitGroup(int groupID)
        {
            return UnitMapper.Mapper(CallContext).GetUnitDataByUnitGroup(groupID);
        }

        public string SubmitAmountAsLog(decimal? totalAmount)
        {
            return PaymentLogMapper.Mapper(CallContext).SubmitAmountAsLog(totalAmount);
        }

        public PaymentLogDTO GetLastLogData()
        {
            return PaymentLogMapper.Mapper(CallContext).GetLastLogData();
        }

        public PaymentMasterVisaDTO GetPaymentMasterVisaData()
        {
            return PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaData();
        }
        public PaymentMasterVisaDTO GetPaymentMasterVisaDataByTrackID(long trackID)
        {
            return PaymentMasterVisaMapper.Mapper(CallContext).GetPaymentMasterVisaDataByTrackID(trackID);
        }

        public PaymentMasterVisaDTO UpdatePaymentMasterVisa(PaymentMasterVisaDTO paymentMasterVisaDTO)
        {
            return PaymentMasterVisaMapper.Mapper(CallContext).UpdatePaymentMasterVisa(paymentMasterVisaDTO);
        }

        public string SaveQPayPayment(PaymentQPAYDTO paymentQPAYDTO)
        {
            return PaymentQPayMapper.Mapper(CallContext).SaveEntity(paymentQPAYDTO);
        }

        public OperationResultDTO FeeCollectionEntry(List<FeeCollectionDTO> feeCollectionList)
        {
            return FeeCollectionMapper.Mapper(CallContext).FeeCollectionEntry(feeCollectionList);
        }

        public List<FeeCollectionDTO> UpdateStudentsFeePaymentStatus(string transactionNo, long? parentLoginID)
        {
            return FeeCollectionMapper.Mapper(CallContext).UpdateStudentsFeePaymentStatus(transactionNo, parentLoginID);
        }

        public List<FeeCollectionDTO> GetStudentFeeCollectionsHistory(StudentDTO studentData, byte? schoolID, int? academicYearID)
        {
            return FeeCollectionMapper.Mapper(CallContext).GetStudentFeeCollectionsHistory(studentData, schoolID, academicYearID);
        }

        public List<AcademicYearDTO> GetCurrentAcademicYearsData()
        {
            return new AcademicYearMapper().GetCurrentAcademicYearsData();
        }

        public List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID)
        {
            return AcademicYearMapper.Mapper(CallContext).GetAllAcademicYearBySchoolID(schoolID);
        }

        public List<FeeCollectionDTO> GetFeeCollectionHistories(List<StudentDTO> studentDatas, byte? schoolID, int? academicYearID)
        {
            return FeeCollectionMapper.Mapper(CallContext).GetFeeCollectionHistories(studentDatas, schoolID, academicYearID);
        }

        public PaymentLogDTO GetAndInsertLogDataByTransactionID(string transID)
        {
            return PaymentLogMapper.Mapper(CallContext).GetAndInsertLogDataByTransactionID(transID);
        }

        public string CheckFeeCollectionStatusByTransactionNumber(string transactionNumber)
        {
            return FeeCollectionMapper.Mapper(CallContext).CheckFeeCollectionStatusByTransactionNumber(transactionNumber);
        }

        public List<FeeCollectionDTO> GetFeeCollectionDetailsByTransactionNumber(string transactionNumber, string mailID, string feeReceiptNo)
        {
            return FeeCollectionMapper.Mapper(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID, feeReceiptNo);
        }

        public OperationResult DeleteTransportApplication(long transportapplicationId)
        {
            try
            {
                TransportApplicationMapper.Mapper(CallContext).DeleteTransportApplication(transportapplicationId);
                return OperationResult.Success;
            }
            catch
            {
                return OperationResult.Error;
            }
        }

        public List<KeyValueDTO> GetRoutesByRouteGroupID(int? routeGroupID)
        {
            return RoutesMapper.Mapper(CallContext).GetRoutesByRouteGroupID(routeGroupID);
        }

        public AcademicYearDTO GetAcademicYearDataByGroupID(int? routeGroupID)
        {
            return RouteGroupMapper.Mapper(CallContext).GetAcademicYearDataByGroupID(routeGroupID);
        }

        public List<KeyValueDTO> GetPickupStopMapsByRouteGroupID(int? routeGroupID)
        {
            return RoutesMapper.Mapper(CallContext).GetPickupStopMapsByRouteGroupID(routeGroupID);
        }

        public List<KeyValueDTO> GetDropStopMapsByRouteGroupID(int? routeGroupID)
        {
            return RoutesMapper.Mapper(CallContext).GetDropStopMapsByRouteGroupID(routeGroupID);
        }

        public List<StudentPickupRequestDTO> GetStudentPickupRequestsByLoginID(long loginID)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).GetStudentPickupRequestsByLoginID(loginID);
        }

        public List<KeyValueDTO> GetSubLedgerByAccount(long loginID)
        {
            return SubLedgerAccountMapper.Mapper(CallContext).GetSubLedgerByAccount(loginID);
        }

        public List<ProgressReportNewDTO> SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            return ProgressReportMapper.Mapper(CallContext).SaveProgressReportData(toDtoList);
        }

        public string UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            return ProgressReportMapper.Mapper(CallContext).UpdatePublishStatus(toDtoList);
        }

        public List<ProgressReportNewDTO> GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            return ProgressReportMapper.Mapper(CallContext).GetProgressReportData(classID, sectionID, academicYearID);
        }

        public List<ProgressReportNewDTO> GetProgressReportList(long studentID, int classID, int sectionID, int academicYearID)
        {
            return ProgressReportMapper.Mapper(CallContext).GetStudentProgressReportData(studentID, classID, sectionID, academicYearID);
        }

        public string CancelStudentPickupRequestByID(long pickupRequestID)
        {
            return StudentPickupRequestMapper.Mapper(CallContext).CancelStudentPickupRequestByID(pickupRequestID);
        }


        public string CancelTransportApplication(long mapIID)
        {
            return TransportApplicationMapper.Mapper(CallContext).CancelTransportApplication(mapIID);
        }


        public TransactionSummaryDetailDTO GetCountByDocumentTypeID(int docTypeID)
        {
            return new TransactionBL(CallContext).GetCountByDocumentTypeID(docTypeID);
        }

        public List<KeyValueDTO> GetStudentDetailsByStaff(long staffID)
        {
            return StudentConcessionMapper.Mapper(CallContext).GetStudentDetailsByStaff(staffID);
        }

        public List<KeyValueDTO> GetStaffDetailsByStudentID(int studentID)
        {
            return StudentConcessionMapper.Mapper(CallContext).GetStaffDetailsByStudentID(studentID);
        }

        public List<KeyValueDTO> GetParentDetailsByStudentID(int studentID)
        {
            return StudentConcessionMapper.Mapper(CallContext).GetParentDetailsByStudentID(studentID);
        }

        public decimal? GetFeeAmount(int studentID, int academicYearID, int feeMasterID, int feePeriodID)
        {
            return StudentConcessionMapper.Mapper(CallContext).GetFeeAmount(studentID, academicYearID, feeMasterID, feePeriodID);
        }

        public string SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SendAttendanceNotificationsToParents(classId, sectionId);
        }

        public DashBoardChartDTO GetCountsForDashBoardMenuCards(int chartID)
        {
            return new BoilerPlateBL(CallContext).DashBoardMenuCardCounts(chartID);
        }

        public DashBoardChartDTO GetTwinViewData(int chartID, string referenceID)
        {
            return new BoilerPlateBL(CallContext).GetTwinViewData(chartID, referenceID);
        }

        //for ClassTeachermap refresh button
        public List<ClassClassTeacherMapDTO> FillEditDatasAndSubjects(int IID, int classID, int sectionID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).FillEditDatasAndSubjects(IID, classID, sectionID);
        }

        public List<StudentFeeConcessionDetailDTO> GetFeeDueForConcession(long studentID, int academicYearID)
        {
            return StudentConcessionMapper.Mapper(CallContext).GetFeeDueForConcession(studentID, academicYearID);
        }

        public List<ClassCoordinatorsDTO> FillClassSectionWiseCoordinators(int classID, int sectionID)
        {
            return ClassCoordinatorMapMapper.Mapper(CallContext).FillClassSectionWiseCoordinators(classID, sectionID);
        }

        public List<MailFeeDueStatementReportDTO> GetFeeDueDatasForReportMail(DateTime asOnDate, int classID, int? sectionID)
        {
            return MailFeeDueStatementMapper.Mapper(CallContext).GetFeeDueDatasForReportMail(asOnDate, classID, sectionID);
        }

        public DirectorsDashBoardDTO GetTeacherRelatedDataForDirectorsDashBoard()
        {
            return new BoilerPlateBL(CallContext).GetTeacherRelatedDataForDirectorsDashBoard();
        }

        public List<KeyValueDTO> GetVehiclesByRoute(int routeID)
        {
            return RouteVehicleMapMapper.Mapper(CallContext).GetVehiclesByRoute(routeID);
        }

        public MailFeeDueStatementReportDTO SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            return MailFeeDueStatementMapper.Mapper(CallContext).SendFeeDueMailReportToParent(gridData);
        }

        public EmployeeDTO UserProfileForDashBoard()
        {
            return new BoilerPlateBL(CallContext).UserProfileForDashBoard();
        }

        public Services.Contracts.Notifications.PushNotificationDTO GetNotificationsForDashBoard()
        {
            return new BoilerPlateBL(CallContext).GetNotificationsForDashBoard();
        }

        public TimeTableDTO GetWeeklyTimeTableForDashBoard(int weekDayID)
        {
            return new BoilerPlateBL(CallContext).GetWeeklyTimeTableForDashBoard(weekDayID);
        }

        public List<FeeDueCancellationDetailDTO> GetFeeDueForDueCancellation(long studentID, int academicYearID)
        {
            return FeeDueCancellationMapper.Mapper(CallContext).GetFeeDueForDueCancellation(studentID, academicYearID);
        }

        public StudentFeeDueDTO GetGridLookUpsForSchoolCreditNote(long studentId)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetGridLookUpsForSchoolCreditNote(studentId);
        }

        public List<EmployeePromotionDTO> GetEmployeeDetailsByEmployeeID(long employeeID)
        {
            return EmployeePromotionMapper.Mapper(CallContext).GetEmployeeDetailsByEmployeeID(employeeID);
        }

        public string UpdateTCStatus(long? studentTransferRequestID, long TCContentID)
        {
            return StudentTransferRequestMapper.Mapper(CallContext).UpdateTCStatus(studentTransferRequestID, TCContentID);
        }

        public string UpdateTCStatusToComplete(long? studentTransferRequestID)
        {
            return StudentTransferRequestMapper.Mapper(CallContext).UpdateTCStatusToComplete(studentTransferRequestID);
        }

        public List<KeyValueDTO> Getstudentsubjectlist(long studentID)
        {
            return AssignmentMapper
                .Mapper(CallContext)
                .Getstudentsubjectlist(studentID);
        }

        public bool InsertProgressReportEntries(List<ProgressReportDTO> progressReportListDTOs, List<SettingDTO> settings)
        {
            return ProgressReportMapper.Mapper(CallContext).InsertProgressReportEntries(progressReportListDTOs, settings);
        } 
        
        public bool UpdateStudentProgressReportStatusID(MarkRegisterDTO dto)
        {
            return ProgressReportMapper.Mapper(CallContext).UpdateStudentProgressReportStatusID(dto);
        }

        public List<ProgressReportDTO> GetStudentProgressReports(ProgressReportDTO progressReport)
        {
            return ProgressReportMapper.Mapper(CallContext).GetStudentProgressReports(progressReport);
        }

        public List<ProgressReportDTO> GetStudentPublishedProgressReports(long studentID, long? examID)
        {
            return ProgressReportMapper.Mapper(CallContext).GetStudentPublishedProgressReports(studentID, examID);
        }

        public BankAccountDTO GetBankDetailsByBankID(long bankID)
        {
            return SchoolsMapper.Mapper(CallContext).GetBankDetailsByBankID(bankID);
        }

        public List<SchoolPayerBankDTO> FillPayerBanksBySchoolID(long schoolID)
        {
            return SchoolsMapper.Mapper(CallContext).FillPayerBanksBySchoolID(schoolID);
        }

        public StudentFeeDueDTO GetStudentFeeDueDetailsByID(long studentFeeDueID)
        {
            return FeeDueGenerationMapper.Mapper(CallContext).GetStudentFeeDueDetailsByID(studentFeeDueID);
        }


        public List<FeeCollectionFeeTypeDTO> GetFeeDuesForCampusTransfer(long studentId, int toSchoolID, int toAcademicYearID, int toClassID)
        {
            return CampusTrasnsferMapper.Mapper(CallContext).GetFeeDuesForCampusTransfer(studentId, toSchoolID, toAcademicYearID, toClassID);
        }

        public StudentDTO GetStudentDetailsByStudentID(long studentID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentDetailsByStudentID(studentID);
        }

        public List<KeyValueDTO> GetStudentsByParameters(int academicYearID, int? classID, int? sectionID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentsByParameters(academicYearID, classID, sectionID);
        }

        public AccountsGroupDTO GetAccountGroupDataByID(int groupID)
        {
            return AccountEntryMapper.Mapper(CallContext).GetAccountGroupDataByID(groupID);
        }

        public List<KeyValueDTO> GetAcademicYearByProgressReport(int studentID)
        {
            return StudentMapper.Mapper(CallContext).GetAcademicYearByProgressReport(studentID);
        }

        public string UpdateScheduleLogStatus(ScheduleLogDTO dto)
        {
            return DriverScheduleLogMapper.Mapper(CallContext).UpdateScheduleLogStatus(dto);
        }

        public FeePaymentDTO GetStudentFeeDetails(long studentID)
        {
            return FeeCollectionMapper.Mapper(CallContext).GetStudentFeeDetails(studentID);
        }
        public List<FeePaymentHistoryDTO> GetFeeCollectionHistory(long studentID)
        {
            return new FeePaymentBL(CallContext).GetFeeCollectionHistory(studentID);
        }
        public StudentTransferRequestDTO FillStudentTransferData(long StudentID)
        {
            return StudentTransferRequestMapper.Mapper(CallContext).FillStudentTransferData(StudentID);
        }

        public List<KeyValueDTO> GetProgressReportIDsByStudPromStatus(int classID, int sectionID, long academicYearID, byte statusID, int examID, int examGroupID)
        {
            return StudentPromotionMapper.Mapper(CallContext).GetProgressReportIDsByStudPromStatus(classID, sectionID, academicYearID, statusID, examID, examGroupID);
        }

        public List<KeyValueDTO> GetSchoolsByLoginIDActiveStuds(long? loginID)
        {
            return SchoolsMapper.Mapper(CallContext).GetSchoolsByLoginIDActiveStuds(loginID);
        }

        public string GetPassageQuestionDetails(long passageQuestionID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetPassageQuestionDetails(passageQuestionID);
        }

        public KeyValueDTO GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetClassHeadTeacher(classID, sectionID, academicYearID);
        }

        public KeyValueDTO GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetClassCoordinator(classID, sectionID, academicYearID);
        }

        public List<KeyValueDTO> GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetAssociateTeachers(classID, sectionID, academicYearID);
        }

        public List<KeyValueDTO> GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetOtherTeachersByClass(classID, sectionID, academicYearID);
        }

        public CandidateDTO GetCandidateDetailsByCandidateID(long candidateID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetCandidateDetailsByCandidateID(candidateID);
        }

        public List<DaysDTO> GetWeekDays()
        {
            try
            {
                return TimeTableLogMapper.Mapper(CallContext).GetWeekDays();
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<TimeTableAllocInfoHeaderDTO> GetSmartTimeTableByDate(int tableMasterId, DateTime timeTableDate, int classID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetSmartTimeTableByDate(tableMasterId, timeTableDate, classID);
        }

        public List<TimeTableListDTO> GetTeacherSummary(int tableMasterID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetTeacherSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummary(int tableMasterID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetClassSummary(tableMasterID);
        }

        public List<TimeTableAllocationDTO> GetClassSectionTimeTableSummary(int tableMasterID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetClassSectionTimeTableSummary(tableMasterID);
        }

        public List<TimeTableListDTO> GetTeacherSummaryByTeacherID(long employeeID, int tableMasterID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetTeacherSummaryByTeacherID(employeeID, tableMasterID);
        }

        public List<TimeTableListDTO> GetClassSummaryDetails(long classID, long sectionID, int tableMasterID)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetClassSummaryDetails(classID,sectionID, tableMasterID);
        }

        public string GenerateSmartTimeTable(int tableMasterId, string timeTableDate)
        {
            return TimeTableLogMapper.Mapper(CallContext).GenerateSmartTimeTable(tableMasterId, timeTableDate);
        }

        public OperationResultDTO ReAssignTimeTable(int tableMasterId, DateTime timeTableDate, int classID)
        {
            return TimeTableLogMapper.Mapper(CallContext).ReAssignTimeTable(tableMasterId, timeTableDate, classID);
        }

        public List<KeyValueDTO> GetClasseByAcademicyear(int academicyearID)
        {
            throw new NotImplementedException();
        }

        public List<SubjectDTO> GetSubjectDetailsByClassID(int classID)
        {
            return ClassSubjectMapMapper.Mapper(CallContext).GetSubjectDetailsByClassID(classID);
        }

        public List<KeyValueDTO> GetAcademicYearBySchoolForApplications(int schoolID, bool? isActive)
        {
            return StudentApplicationMapper.Mapper(CallContext).GetAcademicYearBySchoolForApplications(schoolID, isActive);
        }

        public List<KeyValueDTO> GetWeekDayByDate(string date)
        {
            return TimeTableLogMapper.Mapper(CallContext).GetWeekDayByDate(date);
        }

        public List<KeyValueDTO> GetSetupScreens()
        {
            return TimeTableAllocationMapper.Mapper(CallContext).GetSetupScreens();
        }

        public KeyValueDTO GetCurrentAcademicYearBySchoolID(long schoolID)
        {
            return SchoolsMapper.Mapper(CallContext).GetCurrentAcademicYearBySchoolID(schoolID);
        }

        public List<KeyValueDTO> GetSubjectBySubjectID(byte subjectTypeID)
        {
            return SubjectMapper.Mapper(CallContext).GetSubjectBySubjectID(subjectTypeID);
        }

        public List<KeyValueDTO> GetDashboardCounts(long candidateID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetDashboardCounts(candidateID);
        }

        public List<CandidateOnlineExamMapDTO> GetExamDetailsByCandidateID(long candidateID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetExamDetailsByCandidateID(candidateID);
        }


        public List<LessonPlanDTO> ExtractUploadedFiles()
        {
            return LessonPlanMapper.Mapper(CallContext).ExtractUploadedFiles();
        }

        public List<KeyValueDTO> GetStudentsByParent(long parentID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentsByParent(parentID);
        }

        public List<KeyValueDTO> GetActiveStudentsByParent(long parentID)
        {
            return StudentMapper.Mapper(CallContext).GetActiveStudentsByParent(parentID);
        }

        public List<StudentDTO> GetStudentsDetailsByParent(long parentID)
        {
            return StudentMapper.Mapper(CallContext).GetStudentsDetailsByParent(parentID);
        }

        public List<StudentDTO> GetActiveStudentsDetailsByParent(long parentID)
        {
            return StudentMapper.Mapper(CallContext).GetActiveStudentsDetailsByParent(parentID);
        }

        public List<StudentAttendenceDTO> GetClassWiseAttendanceDatas(int classId, int sectionId)
        {
            return StudentAttendenceMapper.Mapper(CallContext).GetClassWiseAttendanceDatas(classId, sectionId);
        }

        public string SendNotificationsToParentsByAttendance(List<StudentAttendenceDTO> studAttendance)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SendNotificationsToParentsByAttendance(studAttendance);
        }

        public List<KeyValueDTO> GetSubjectBySubjectTypeID(byte subjectTypeID)
        {
            return SubjectMapper.Mapper(CallContext).GetSubjectBySubjectTypeID(subjectTypeID);
        }

        public List<KeyValueDTO> GetTeacherByClassAndSubject(string classIDs, string subjectIDs)
        {
            return ClassTeacherMapMapper.Mapper(CallContext).GetTeacherByClassAndSubject(classIDs, subjectIDs);
        }

        public string SaveAllStudentAttendances(int classID, int sectionID, string attendanceDateString, byte? attendanceStatus)
        {
            return StudentAttendenceMapper.Mapper(CallContext).SaveAllStudentAttendances(classID, sectionID, attendanceDateString, attendanceStatus);
        }

        public List<AgendaDTO> GetStudentSubjectWiseAgendas(long loginID, long subjectID, string date)
        {
            return AgendaTopicMapper.Mapper(CallContext).GetStudentSubjectWiseAgendas(loginID,subjectID,date);
        }
    }
}