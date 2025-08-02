using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class TimeTableAllocationMapper : DTOEntityDynamicMapper
    {
        public static TimeTableAllocationMapper Mapper(CallContext context)
        {
            var mapper = new TimeTableAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TimeTableAllocationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TimeTableAllocations.Where(X => X.TimeTableAllocationIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entitydto = new TimeTableAllocationDTO()
                {
                    TimeTableAllocationIID = entity.TimeTableAllocationIID,
                    //StaffID = entity.StaffID,
                    WeekDayID = entity.WeekDayID,
                    SubjectID = entity.SubjectID,
                    SectionID = entity.SectionID,
                    ClassTimingID = entity.ClassTimingID,
                    ClassId = entity.ClassId,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    Notes = null
                };

                return ToDTOString(entitydto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TimeTableAllocationDTO;

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (toDto.StaffIDList.Any())
                {
                    var entityGetStaffId = dbContext.TimeTableAllocations.Where(a => ((a.ClassId != toDto.ClassId) || (a.ClassId == toDto.ClassId
                    && a.SectionID != toDto.SectionID)) && a.TimeTableID == toDto.TimeTableID && a.WeekDayID == toDto.WeekDayID
                    && a.ClassTimingID == toDto.ClassTimingID && toDto.StaffIDList.Contains(a.StaffID != null ? a.StaffID.Value : 0))
                        .AsNoTracking().ToList();

                    if (entityGetStaffId.Count() > 0)
                    {
                        return "#101";
                    }
                    toDto.StaffIDList.All(staff =>
                    {
                        var entity = new TimeTableAllocation()
                        {
                            ClassId = toDto.ClassId,
                            SectionID = toDto.SectionID,
                            WeekDayID = toDto.WeekDayID,
                            ClassTimingID = toDto.ClassTimingID,
                            StaffID = staff,
                            SubjectID = toDto.SubjectID,
                            TimeTableID = toDto.TimeTableID,
                            SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                            AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                            TimeTableAllocationIID = toDto.TimeTableAllocationIID,
                            CreatedBy = toDto.TimeTableAllocationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                            UpdatedBy = toDto.TimeTableAllocationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                            CreatedDate = toDto.TimeTableAllocationIID == 0 ? DateTime.Now : dto.CreatedDate,
                            UpdatedDate = toDto.TimeTableAllocationIID > 0 ? DateTime.Now : dto.UpdatedDate
                        };
                        //TimeStamps = toDto.TimeStamps == null ? null : toDto.TimeStamps,

                        var entityGetId = dbContext.TimeTableAllocations
                        .Where(a => a.TimeTableID == entity.TimeTableID && a.ClassId == entity.ClassId && a.SectionID == entity.SectionID && a.WeekDayID == entity.WeekDayID && a.ClassTimingID == entity.ClassTimingID && a.StaffID == staff)
                        .AsNoTracking().FirstOrDefault();

                        if (entityGetId == null)
                        {
                            dbContext.TimeTableAllocations.Add(entity);
                            dbContext.Entry(entity).State = EntityState.Added;
                        }
                        else
                        {
                            entityGetId.StaffID = entity.StaffID;
                            entityGetId.SubjectID = entity.SubjectID;
                            entity.TimeTableAllocationIID = entityGetId.TimeTableAllocationIID;

                            dbContext.TimeTableAllocations.Add(entity);
                            dbContext.Entry(entity).State = EntityState.Modified;
                        }
                        dbContext.SaveChanges();

                        toDto.AllocationIIDList.Add(entity.TimeTableAllocationIID);
                        entity.TimeTableAllocationIID = 0;

                        return true;
                    });
                }
            }
            toDto.AllocationIIDs = string.Join(",", toDto.AllocationIIDList);
            toDto.StaffIDs = string.Join(",", toDto.StaffIDList);
            return (ToDTOString(toDto));
        }

        private TimeTableAllocationDTO ToDTO(TimeTableAllocation entity)
        {
            return new TimeTableAllocationDTO()
            {
                ClassId = entity.ClassId,
                SectionID = entity.SectionID,
                //StaffID = entity.StaffID,
                WeekDayID = entity.WeekDayID,
                SubjectID = entity.SubjectID,
                ClassTimingID = entity.ClassTimingID,
                TimeTableAllocationIID = entity.TimeTableAllocationIID,
                Class = new KeyValueDTO()
                {
                    Key = entity.ClassId.ToString(),
                    Value = entity.Class.ClassDescription
                },
                Section = new KeyValueDTO()
                {
                    Key = entity.SectionID.ToString(),
                    Value = entity.Section.SectionName
                },
                TimeTable = new KeyValueDTO()
                {
                    Key = entity.TimeTableID.ToString(),
                    Value = entity.TimeTable.TimeTableDescription
                },
                //ClassTiming = new KeyValueDTO()
                //{
                //    Key = entity.ClassTimingID.ToString(),
                //    Value = entity.ClassTiming.TimingDescription
                //},
                //Employee = new KeyValueDTO()
                //{
                //    Key = entity.StaffID.ToString(),
                //    Value = entity.Employee.EmployeeName
                //}//,
                //CreatedBy = entity.CreatedBy,
                //UpdatedBy = entity.UpdatedBy,
                //CreatedDate = entity.CreatedDate,
                //UpdatedDate = entity.UpdatedDate
            };
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByClassID(int classID, int tableMasterId)
        {
            var timeTableInfoList = new List<TimeTableAllocInfoHeaderDTO>();
            var timeTableList = new List<TimeTableAllocationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesTableMaster = dbContext.TimeTables.Where(X => X.TimeTableID == tableMasterId).AsNoTracking().FirstOrDefault();

                var entitiesClassSection = dbContext.ClassSectionMaps.Where(X => X.ClassID == classID && X.AcademicYearID == _context.AcademicYearID && X.SchoolID == _context.SchoolID)
                    .Include(i => i.Section)
                    .Include(i => i.Class)
                    .AsNoTracking()
                    .OrderBy(a => a.Section.SectionName)
                    .ToList();

                foreach (var classSection in entitiesClassSection)
                {
                    var entitiesTimeTableAlloc = dbContext.TimeTableAllocations.Where(a => a.ClassId == classID && a.TimeTableID == tableMasterId && a.SectionID == classSection.SectionID)
                        .Include(i => i.Class)
                        .Include(i => i.Section)
                        .Include(i => i.ClassTiming)
                        .Include(i => i.Subject)
                        .Include(i => i.Employee)
                        .Include(i => i.WeekDay).ThenInclude(j => j.Day)
                        .AsNoTracking()
                        .ToList();

                    var detailList = new List<TimeTableAllocInfoDetailDTO>();
                    var breakTimeList = new List<TimeTableAllocInfoDetailDTO>();
                    //Group by to make distinct without staffid
                    detailList.AddRange((from timeTableAlloc in entitiesTimeTableAlloc
                                         select new TimeTableAllocInfoDetailDTO
                                         {
                                             //TimeTableAllocationIID = timeTableAlloc.TimeTableAllocationIID,
                                             ClassTimingID = timeTableAlloc.ClassTimingID,
                                             StaffIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                             && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList(),

                                             StaffIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                             && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList())),

                                             StaffNames = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                               && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.Employee.FirstName).ToList())),

                                             AllocationIIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                             && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList(),

                                             AllocationIIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                             && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList())),

                                             //StaffID = timeTableAlloc.StaffID.HasValue ? timeTableAlloc.StaffID.Value : (long?)null,
                                             SubjectID = timeTableAlloc.SubjectID.HasValue ? timeTableAlloc.SubjectID : (int?)null,
                                             WeekDayID = timeTableAlloc.WeekDayID,
                                             ClassTiming = new KeyValueDTO()
                                             {
                                                 Key = timeTableAlloc.ClassTiming.ClassTimingID.ToString(),
                                                 Value = timeTableAlloc.ClassTiming.TimingDescription
                                             },
                                             Subject = timeTableAlloc.SubjectID.HasValue ? new KeyValueDTO()
                                             {
                                                 Key = timeTableAlloc.Subject.SubjectID.ToString(),
                                                 Value = timeTableAlloc.Subject.SubjectName
                                             } : new KeyValueDTO(),
                                             Employee = timeTableAlloc.StaffID.HasValue ? new KeyValueDTO()
                                             {
                                                 Key = timeTableAlloc.Employee.EmployeeIID.ToString(),
                                                 Value = timeTableAlloc.Employee.EmployeeCode + " - " + timeTableAlloc.Employee.FirstName + " " + timeTableAlloc.Employee.MiddleName + " " + timeTableAlloc.Employee.LastName
                                             } : new KeyValueDTO(),
                                             WeekDay = new KeyValueDTO()
                                             {
                                                 Key = timeTableAlloc.WeekDay.WeekDayID.ToString(),
                                                 Value = timeTableAlloc.WeekDay.Day.DayName
                                             },
                                             IsBreakTime = false
                                         }).Distinct());


                    var entitiesWeek = dbContext.WeekDays.Where(X => X.IsWeekDay == true)
                        .Include(i => i.Day)
                        .AsNoTracking()
                        .ToList();

                    foreach (var week in entitiesWeek)
                    {
                        var entitiesClassTiming = dbContext.ClassTimings.Where(X => X.IsBreakTime == true)
                            .Include(i => i.BreakType)
                            .AsNoTracking()
                            .ToList();

                        foreach (var classTiming in entitiesClassTiming)
                        {
                            detailList.Add(new TimeTableAllocInfoDetailDTO()
                            {
                                TimeTableAllocationIID = 0,
                                ClassTimingID = classTiming.ClassTimingID,
                                StaffID = (long?)null,
                                SubjectID = (int?)null,
                                WeekDayID = week.WeekDayID,
                                ClassTiming = new KeyValueDTO() { Key = classTiming.ClassTimingID.ToString(), Value = classTiming.TimingDescription },
                                Employee = new KeyValueDTO() { Key = null, Value = null },
                                Subject = new KeyValueDTO() { Key = null, Value = classTiming.BreakType.BreakTypeName },
                                WeekDay = new KeyValueDTO() { Key = week.WeekDayID.ToString(), Value = week.Day.DayName },
                                IsBreakTime = true
                            });
                        }

                    }

                    timeTableInfoList.Add(new TimeTableAllocInfoHeaderDTO()
                    {
                        Class = new KeyValueDTO() { Key = classSection.Class.ClassID.ToString(), Value = classSection.Class.ClassDescription },
                        Section = new KeyValueDTO() { Key = classSection.Section.SectionID.ToString(), Value = classSection.Section.SectionName },
                        ClassId = classSection.ClassID,
                        SectionID = classSection.SectionID,

                        AllocInfoDetails = detailList,
                        TimeTableAllocationIID = detailList.Count() > 0 ? detailList[0].TimeTableAllocationIID : 0
                    });
                }
            }

            return timeTableInfoList;
        }

        public List<TimeTableAllocationDTO> GetClassTimeTable(long studentId)
        {
            DateTime currentDate = System.DateTime.Now;
            var timeTAllocList = new List<TimeTableAllocationDTO>();
            var timeTableBreakList = new List<TimeTableAllocationDTO>();
            var timeTable = new List<TimeTableAllocationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var timeTAlloc = (from time in dbContext.TimeTableAllocations

                                  join stud in dbContext.Students on time.ClassId equals stud.ClassID
                                  join clstm in dbContext.ClassTimings on time.ClassTimingID equals clstm.ClassTimingID
                                  join wday in dbContext.WeekDays on time.WeekDayID equals wday.WeekDayID
                                  join day in dbContext.Days on wday.DayID equals day.DayID
                                  join tableAloc in dbContext.TimeTables on time.TimeTableID equals tableAloc.TimeTableID
                                  where ((stud.StudentIID == studentId) && (stud.ClassID == time.ClassId) && (stud.SectionID == time.SectionID) && tableAloc.IsActive == true &&
                                   (tableAloc.AcademicYear.StartDate.Value) <= (currentDate.Date) && (tableAloc.AcademicYear.EndDate.Value) >= (currentDate.Date))
                                  orderby time.TimeTableAllocationIID descending
                                  select time).AsNoTracking().ToList();

                timeTAllocList = timeTAlloc.Select(timeTableGroup => new TimeTableAllocationDTO()
                {
                    TimeTableAllocationIID = timeTableGroup.TimeTableAllocationIID,
                    TimeTableID = timeTableGroup.TimeTableID,
                    ClassTimingID = timeTableGroup.ClassTimingID,
                    SubjectID = timeTableGroup.SubjectID,
                    //StaffID = timeTableGroup.StaffID,
                    ClassId = timeTableGroup.ClassId,
                    SectionID = timeTableGroup.SectionID,
                    WeekDay = new KeyValueDTO() { Key = timeTableGroup.WeekDay.WeekDayID.ToString(), Value = timeTableGroup.WeekDay.Day.DayName },
                    ClassTiming = new KeyValueDTO() { Key = timeTableGroup.ClassTiming.ClassTimingID.ToString(), Value = timeTableGroup.ClassTiming.TimingDescription },
                    Subject = (timeTableGroup.ClassTiming.IsBreakTime.HasValue && timeTableGroup.ClassTiming.IsBreakTime.Value == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Subject.SubjectID.ToString(), Value = timeTableGroup.Subject.SubjectName },
                    Employee = (timeTableGroup.ClassTiming.IsBreakTime.HasValue && timeTableGroup.ClassTiming.IsBreakTime == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Employee.StaffAttendences.ToString(), Value = timeTableGroup.Employee.FirstName + " " + timeTableGroup.Employee.MiddleName + " " + timeTableGroup.Employee.LastName },
                    Class = new KeyValueDTO() { Key = timeTableGroup.Class.ClassID.ToString(), Value = timeTableGroup.Class.ClassDescription },
                    Section = new KeyValueDTO() { Key = timeTableGroup.Section.SectionID.ToString(), Value = timeTableGroup.Section.SectionName },
                    IsBreakTime = timeTableGroup.ClassTiming.IsBreakTime
                }).ToList();

                var timeTableBreak = (from wday in dbContext.WeekDays
                                      join clstm in dbContext.ClassTimings on wday.ClassTimingSetID equals clstm.ClassTimingSetID
                                      join day in dbContext.Days on wday.DayID equals day.DayID
                                      where clstm.IsBreakTime == true
                                      orderby wday.DayID descending
                                      select new TimeTableAllocationDTO()
                                      {
                                          TimeTableAllocationIID = 0,
                                          TimeTableID = (int?)null,
                                          WeekDayID = wday.WeekDayID,
                                          ClassTimingID = clstm.ClassTimingID,
                                          SubjectID = (int?)null,
                                          //StaffID = (long?)null,
                                          ClassId = (int?)null,
                                          SectionID = (int?)null,
                                          WeekDay = new KeyValueDTO() { Key = wday.WeekDayID.ToString(), Value = day.DayName },
                                          ClassTiming = new KeyValueDTO() { Key = clstm.ClassTimingID.ToString(), Value = clstm.TimingDescription },
                                          Subject = new KeyValueDTO() { Key = null, Value = "Break" },
                                          Employee = new KeyValueDTO() { Key = null, Value = "Break" },
                                          Class = new KeyValueDTO() { Key = null, Value = null },
                                          Section = new KeyValueDTO() { Key = null, Value = null },
                                          IsBreakTime = clstm.IsBreakTime
                                      }).AsNoTracking().ToList();

                //timeTAlloc = (from time in dbContext.TimeTableAllocations.AsEnumerable()

                //              join stud in dbContext.Students on time.ClassId equals stud.ClassID
                //              join clstm in dbContext.ClassTimings on time.ClassTimingID equals clstm.ClassTimingID
                //              join wday in dbContext.WeekDays on time.WeekDayID equals wday.WeekDayID
                //              join day in dbContext.Days on wday.DayID equals day.DayID
                //              join tableAloc in dbContext.TimeTables on time.TimeTableID equals tableAloc.TimeTableID
                //              where ((stud.StudentIID == studentId) && (stud.ClassID == time.ClassId) && (stud.SectionID == time.SectionID) && tableAloc.IsActive == true &&
                //               (tableAloc.AcademicYear.StartDate.Value) <= (currentDate.Date) && (tableAloc.AcademicYear.EndDate.Value) >= (currentDate.Date))
                //              orderby time.TimeTableAllocationIID descending
                //              group stud by new
                //              {
                //                  time.TimeTableAllocationIID,
                //                  time.ClassId,
                //                  time.SectionID,
                //                  time.TimeTableID,
                //                  time.Section,
                //                  time.WeekDayID,
                //                  time.ClassTimingID,
                //                  time.SubjectID,
                //                  time.StaffID,
                //                  time.Class,
                //                  time.Employee,
                //                  time.Subject,
                //                  time.ClassTiming,
                //                  time.WeekDay,
                //                  clstm.IsBreakTime
                //              } into timeTableGroup
                //              select new TimeTableAllocationDTO()
                //              {
                //                  TimeTableAllocationIID = timeTableGroup.Key.TimeTableAllocationIID,
                //                  TimeTableID = timeTableGroup.Key.TimeTableID,
                //                  WeekDayID = timeTableGroup.Key.WeekDayID,
                //                  ClassTimingID = timeTableGroup.Key.ClassTimingID,
                //                  SubjectID = timeTableGroup.Key.SubjectID,
                //                  StaffID = timeTableGroup.Key.StaffID,
                //                  ClassId = timeTableGroup.Key.ClassId,
                //                  SectionID = timeTableGroup.Key.SectionID,
                //                  WeekDay = new KeyValueDTO() { Key = timeTableGroup.Key.WeekDay.WeekDayID.ToString(), Value = timeTableGroup.Key.WeekDay.Day.DayName },
                //                  ClassTiming = new KeyValueDTO() { Key = timeTableGroup.Key.ClassTiming.ClassTimingID.ToString(), Value = timeTableGroup.Key.ClassTiming.TimingDescription },
                //                  Subject = (timeTableGroup.Key.IsBreakTime.HasValue && timeTableGroup.Key.IsBreakTime == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Key.Subject.SubjectID.ToString(), Value = timeTableGroup.Key.Subject.SubjectName },
                //                  Employee = (timeTableGroup.Key.IsBreakTime.HasValue && timeTableGroup.Key.IsBreakTime == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Key.Employee.StaffAttendences.ToString(), Value = timeTableGroup.Key.Employee.FirstName + " " + timeTableGroup.Key.Employee.MiddleName + " " + timeTableGroup.Key.Employee.LastName },
                //                  Class = new KeyValueDTO() { Key = timeTableGroup.Key.Class.ClassID.ToString(), Value = timeTableGroup.Key.Class.ClassDescription },
                //                  Section = new KeyValueDTO() { Key = timeTableGroup.Key.Section.SectionID.ToString(), Value = timeTableGroup.Key.Section.SectionName },
                //                  IsBreakTime = timeTableGroup.Key.IsBreakTime
                //              }).ToList();

                //timeTableBreak = (from wday in dbContext.WeekDays.AsEnumerable()                                                                                                
                //                  join clstm in dbContext.ClassTimings on wday.ClassTimingSetID equals clstm.ClassTimingSetID
                //                  join day in dbContext.Days on wday.DayID equals day.DayID                                 
                //                  where clstm.IsBreakTime == true 
                //                  orderby wday.DayID descending
                //                  group wday by new
                //                  {                                     
                //                      wday.WeekDayID,
                //                      clstm.ClassTimingID,                                    

                //                      clstm.TimingDescription,
                //                      wday.Day,
                //                      clstm.IsBreakTime
                //                  } into timeTableGroup
                //                  select new TimeTableAllocationDTO()
                //                  {
                //                      TimeTableAllocationIID = 0,
                //                      TimeTableID = (int?)null,
                //                      WeekDayID = timeTableGroup.Key.WeekDayID,
                //                      ClassTimingID = timeTableGroup.Key.ClassTimingID,
                //                      SubjectID = (int?)null,
                //                      StaffID = (long?)null,
                //                      ClassId = (int?)null,
                //                      SectionID = (int?)null,
                //                      WeekDay = new KeyValueDTO() { Key = timeTableGroup.Key.WeekDayID.ToString(), Value = timeTableGroup.Key.Day.DayName },
                //                      ClassTiming = new KeyValueDTO() { Key = timeTableGroup.Key.ClassTimingID.ToString(), Value = timeTableGroup.Key.TimingDescription },
                //                      Subject =  new KeyValueDTO() { Key = null, Value = "Break" } ,
                //                      Employee = new KeyValueDTO() { Key = null, Value = "Break" } ,
                //                      Class = new KeyValueDTO() { Key = null, Value = null },
                //                      Section = new KeyValueDTO() { Key = null, Value = null },
                //                      IsBreakTime = timeTableGroup.Key.IsBreakTime
                //                  }).ToList();
                timeTable.AddRange(timeTAllocList);
                timeTable.AddRange(timeTableBreakList);
            }
            return timeTable;
        }

        public void DeleteEntity(string timeTableAllocationIDs)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string[] deleteID = timeTableAllocationIDs.Split(',');
                long[] deleteIDLong = deleteID.Select(x => long.Parse(x)).ToArray();

                var entitiesTimeTableAllocations = dbContext.TimeTableAllocations.Where(a => deleteIDLong.Any(d => d == a.TimeTableAllocationIID)).AsNoTracking().ToList();

                if (entitiesTimeTableAllocations.Count > 0)
                {
                    dbContext.RemoveRange(entitiesTimeTableAllocations);
                    dbContext.SaveChanges();
                }
            }
        }

        //public List<TimeTableAllocationDTO> GetTimeTableByStudentID(long studentID)
        //{
        //    DateTime currentDate = System.DateTime.Now;
        //    var timeTAllocList = new List<TimeTableAllocationDTO>();
        //    var timeTableBreakList = new List<TimeTableAllocationDTO>();
        //    var timeTable = new List<TimeTableAllocationDTO>();
        //    using (var dbContext = new dbEduegateSchoolContext())
        //    {
        //        var timeTAlloc = (from time in dbContext.TimeTableAllocations
        //                          join stud in dbContext.Students on time.ClassId equals stud.ClassID
        //                          join clstm in dbContext.ClassTimings on time.ClassTimingID equals clstm.ClassTimingID
        //                          join wday in dbContext.WeekDays on time.WeekDayID equals wday.WeekDayID
        //                          join day in dbContext.Days on wday.DayID equals day.DayID
        //                          join tableAloc in dbContext.TimeTables on time.TimeTableID equals tableAloc.TimeTableID
        //                          where ((stud.StudentIID == studentID) && (stud.ClassID == time.ClassId) && (stud.SectionID == time.SectionID) && tableAloc.IsActive == true &&
        //                           (tableAloc.AcademicYear.StartDate.Value) <= (currentDate.Date) && (tableAloc.AcademicYear.EndDate.Value) >= (currentDate.Date))
        //                          orderby time.TimeTableAllocationIID descending
        //                          select time
        //                          ).ToList();

        //        timeTAllocList = timeTAlloc.Select(timeTableGroup => new TimeTableAllocationDTO()
        //        {
        //            TimeTableAllocationIID = timeTableGroup.TimeTableAllocationIID,
        //            TimeTableID = timeTableGroup.TimeTableID,
        //            ClassTimingID = timeTableGroup.ClassTimingID,
        //            SubjectID = timeTableGroup.SubjectID,
        //            //StaffID = timeTableGroup.StaffID,
        //            ClassId = timeTableGroup.ClassId,
        //            SectionID = timeTableGroup.SectionID,
        //            WeekDay = new KeyValueDTO() { Key = timeTableGroup.WeekDay.WeekDayID.ToString(), Value = timeTableGroup.WeekDay.Day.DayName },
        //            ClassTiming = new KeyValueDTO() { Key = timeTableGroup.ClassTiming.ClassTimingID.ToString(), Value = timeTableGroup.ClassTiming.TimingDescription },
        //            Subject = (timeTableGroup.ClassTiming.IsBreakTime.HasValue && timeTableGroup.ClassTiming.IsBreakTime.Value == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Subject.SubjectID.ToString(), Value = timeTableGroup.Subject.SubjectName },
        //            Employee = (timeTableGroup.ClassTiming.IsBreakTime.HasValue && timeTableGroup.ClassTiming.IsBreakTime == true) ? new KeyValueDTO() { Key = null, Value = "Break" } : new KeyValueDTO() { Key = timeTableGroup.Employee.StaffAttendences.ToString(), Value = timeTableGroup.Employee.FirstName + " " + timeTableGroup.Employee.MiddleName + " " + timeTableGroup.Employee.LastName },
        //            Class = new KeyValueDTO() { Key = timeTableGroup.Class.ClassID.ToString(), Value = timeTableGroup.Class.ClassDescription },
        //            Section = new KeyValueDTO() { Key = timeTableGroup.Section.SectionID.ToString(), Value = timeTableGroup.Section.SectionName },
        //            IsBreakTime = timeTableGroup.ClassTiming.IsBreakTime
        //        }).ToList();

        //        var timeTableBreak = (from wday in dbContext.WeekDays
        //                              join clstm in dbContext.ClassTimings on wday.ClassTimingSetID equals clstm.ClassTimingSetID
        //                              join day in dbContext.Days on wday.DayID equals day.DayID
        //                              where clstm.IsBreakTime == true
        //                              orderby wday.DayID descending
        //                              select new TimeTableAllocationDTO()
        //                              {
        //                                  TimeTableAllocationIID = 0,
        //                                  TimeTableID = (int?)null,
        //                                  WeekDayID = wday.WeekDayID,
        //                                  ClassTimingID = clstm.ClassTimingID,
        //                                  SubjectID = (int?)null,
        //                                  //StaffID = (long?)null,
        //                                  ClassId = (int?)null,
        //                                  SectionID = (int?)null,
        //                                  WeekDay = new KeyValueDTO() { Key = wday.WeekDayID.ToString(), Value = day.DayName },
        //                                  ClassTiming = new KeyValueDTO() { Key = clstm.ClassTimingID.ToString(), Value = clstm.TimingDescription },
        //                                  Subject = new KeyValueDTO() { Key = null, Value = "Break" },
        //                                  Employee = new KeyValueDTO() { Key = null, Value = "Break" },
        //                                  Class = new KeyValueDTO() { Key = null, Value = null },
        //                                  Section = new KeyValueDTO() { Key = null, Value = null },
        //                                  IsBreakTime = clstm.IsBreakTime
        //                              }).ToList();
        //        timeTable.AddRange(timeTAllocList);
        //        timeTable.AddRange(timeTableBreakList);
        //    }
        //    return timeTable;
        //}

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStudentID(long studentID)
        {
            var timeTableInfoList = new List<TimeTableAllocInfoHeaderDTO>();
            var timeTableList = new List<TimeTableAllocationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var settingMasterID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_TIMETABLE_MASTERID", 1);

                var studDet = dbContext.Students.Where(s => s.StudentIID == studentID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                var timeTableMasterData = dbContext.TimeTables.Where(t => t.AcademicYearID == studDet.AcademicYearID && t.SchoolID == studDet.SchoolID && t.IsActive == true)
                    .OrderByDescending(o => o.TimeTableID).AsNoTracking().FirstOrDefault();

                int tableMasterId = timeTableMasterData != null ? timeTableMasterData.TimeTableID : settingMasterID;

                var entitiesTimeTableAlloc = dbContext.TimeTableAllocations.Where(a => a.ClassId == studDet.ClassID && a.TimeTableID == tableMasterId && a.SectionID == studDet.SectionID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.ClassTiming)
                    .Include(i => i.Subject)
                    .Include(i => i.Employee)
                    .Include(i => i.WeekDay)
                    .AsNoTracking()
                    .ToList();

                var detailList = new List<TimeTableAllocInfoDetailDTO>();
                var breakTimeList = new List<TimeTableAllocInfoDetailDTO>();
                //Group by to make distinct without staffid
                detailList.AddRange((from timeTableAlloc in entitiesTimeTableAlloc
                                     select new TimeTableAllocInfoDetailDTO
                                     {
                                         //TimeTableAllocationIID = timeTableAlloc.TimeTableAllocationIID,
                                         ClassTimingID = timeTableAlloc.ClassTimingID,
                                         StaffIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList(),

                                         StaffIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList())),

                                         StaffNames = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                           && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.Employee.FirstName).ToList())),

                                         AllocationIIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList(),

                                         AllocationIIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList())),

                                         //StaffID = timeTableAlloc.StaffID.HasValue ? timeTableAlloc.StaffID.Value : (long?)null,
                                         SubjectID = timeTableAlloc.SubjectID.HasValue ? timeTableAlloc.SubjectID.Value : (int?)null,
                                         WeekDayID = timeTableAlloc.WeekDayID,
                                         ClassTiming = new KeyValueDTO()
                                         {
                                             Key = timeTableAlloc.ClassTiming.ClassTimingID.ToString(),
                                             Value = timeTableAlloc.ClassTiming.TimingDescription
                                         },
                                         //                                Employee = timeTableAlloc.StaffID.HasValue ? new KeyValueDTO() { Key = timeTableAlloc.Employee.EmployeeIID.ToString(), Value = timeTableAlloc.Employee.FirstName + " " + timeTableAlloc.Employee.MiddleName + " " + timeTableAlloc.Employee.LastName } :
                                         //new KeyValueDTO() { Key = null, Value = null },
                                         Subject = timeTableAlloc.SubjectID.HasValue ? new KeyValueDTO()
                                         {
                                             Key = timeTableAlloc.Subject.SubjectID.ToString(),
                                             Value = timeTableAlloc.Subject.SubjectName
                                         } : new KeyValueDTO(),
                                         WeekDay = new KeyValueDTO()
                                         {
                                             Key = timeTableAlloc.WeekDay.WeekDayID.ToString(),
                                             Value = timeTableAlloc.WeekDay.Day.DayName
                                         },
                                         IsBreakTime = false
                                     }).Distinct());

                var entitiesWeek = dbContext.WeekDays.Where(X => X.IsWeekDay == true)
                    .Include(i => i.Day)
                    .AsNoTracking()
                    .ToList();

                foreach (var week in entitiesWeek)
                {
                    var entitiesClassTiming = dbContext.ClassTimings.Where(X => X.IsBreakTime == true)
                        .Include(i => i.BreakType)
                        .AsNoTracking()
                        .ToList();
                    foreach (var classTiming in entitiesClassTiming)
                    {
                        detailList.Add(new TimeTableAllocInfoDetailDTO()
                        {
                            TimeTableAllocationIID = 0,
                            ClassTimingID = classTiming.ClassTimingID,
                            StaffID = (long?)null,
                            SubjectID = (int?)null,
                            WeekDayID = week.WeekDayID,
                            ClassTiming = new KeyValueDTO() { Key = classTiming.ClassTimingID.ToString(), Value = classTiming.TimingDescription },
                            Employee = new KeyValueDTO() { Key = null, Value = null },
                            Subject = new KeyValueDTO() { Key = null, Value = classTiming.BreakType.BreakTypeName },
                            WeekDay = new KeyValueDTO() { Key = week.WeekDayID.ToString(), Value = week.Day.DayName },
                            IsBreakTime = true
                        });
                    }
                }

                timeTableInfoList.Add(new TimeTableAllocInfoHeaderDTO()
                {
                    Class = new KeyValueDTO() { Key = studDet.Class.ClassID.ToString(), Value = studDet.Class.ClassDescription },
                    Section = new KeyValueDTO() { Key = studDet.Section.SectionID.ToString(), Value = studDet.Section.SectionName },
                    ClassId = studDet.ClassID,
                    SectionID = studDet.SectionID,
                    AllocInfoDetails = detailList,
                    TimeTableAllocationIID = detailList.Count() > 0 ? detailList[0].TimeTableAllocationIID : 0
                });
            }

            return timeTableInfoList;
        }

        public List<TimeTableAllocInfoHeaderDTO> GetTimeTableByStaffLoginID(long loginID)
        {
            var timeTableInfoList = new List<TimeTableAllocInfoHeaderDTO>();
            var timeTableList = new List<TimeTableAllocationDTO>();

            var classSectionList = new List<ClassTeacherMapDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var employee = dbContext.Employees.Where(X => X.LoginID == loginID).AsNoTracking().FirstOrDefault();

                var entitiesTimeTableAlloc = dbContext.TimeTableAllocations.Where(a => a.StaffID == employee.EmployeeIID)
                    .Include(i => i.Employee)
                    .Include(i => i.ClassTiming)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.WeekDay).ThenInclude(i => i.Day)
                    .AsNoTracking().ToList();

                var detailList = new List<TimeTableAllocInfoDetailDTO>();
                var breakTimeList = new List<TimeTableAllocInfoDetailDTO>();

                //Group by to make distinct without staffid
                detailList.AddRange((from timeTableAlloc in entitiesTimeTableAlloc
                                     select new TimeTableAllocInfoDetailDTO
                                     {
                                         //TimeTableAllocationIID = timeTableAlloc.TimeTableAllocationIID,
                                         ClassTimingID = timeTableAlloc.ClassTimingID,
                                         StaffIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList(),

                                         StaffIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.StaffID.Value).ToList())),

                                         StaffNames = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                           && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.Employee.FirstName).ToList())),

                                         AllocationIIDList = entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList(),

                                         AllocationIIDs = string.Join(",", (entitiesTimeTableAlloc.Where(i => i.ClassTimingID == timeTableAlloc.ClassTimingID
                                         && i.SubjectID == timeTableAlloc.SubjectID && i.WeekDayID == timeTableAlloc.WeekDayID).Select(i => i.TimeTableAllocationIID).ToList())),

                                         //StaffID = timeTableAlloc.StaffID.HasValue ? timeTableAlloc.StaffID.Value : (long?)null,
                                         SubjectID = timeTableAlloc.SubjectID.HasValue ? timeTableAlloc.SubjectID.Value : (int?)null,
                                         WeekDayID = timeTableAlloc.WeekDayID,
                                         ClassTiming = new KeyValueDTO() { Key = timeTableAlloc.ClassTiming.ClassTimingID.ToString(), Value = timeTableAlloc.ClassTiming.TimingDescription },
                                         //                                Employee = timeTableAlloc.StaffID.HasValue ? new KeyValueDTO() { Key = timeTableAlloc.Employee.EmployeeIID.ToString(), Value = timeTableAlloc.Employee.FirstName + " " + timeTableAlloc.Employee.MiddleName + " " + timeTableAlloc.Employee.LastName } :
                                         //new KeyValueDTO() { Key = null, Value = null },
                                         Subject = timeTableAlloc.SubjectID.HasValue ? new KeyValueDTO() { Key = timeTableAlloc.Subject.SubjectID.ToString(), Value = timeTableAlloc.Class.ClassDescription + " [" + timeTableAlloc.Subject.SubjectName + "]" } : new KeyValueDTO() { Key = null, Value = null },
                                         WeekDay = new KeyValueDTO() { Key = timeTableAlloc.WeekDay.WeekDayID.ToString(), Value = timeTableAlloc.WeekDay.Day.DayName },
                                         IsBreakTime = false
                                     }).Distinct());

                var entitiesWeek = dbContext.WeekDays.Where(X => X.IsWeekDay == true)
                    .Include(i => i.Day)
                    .AsNoTracking()
                    .ToList();

                foreach (var week in entitiesWeek)
                {
                    var entitiesClassTiming = dbContext.ClassTimings.Where(X => X.IsBreakTime == true)
                        .Include(i => i.BreakType)
                        .AsNoTracking()
                        .ToList();

                    foreach (var classTiming in entitiesClassTiming)
                    {
                        detailList.Add(new TimeTableAllocInfoDetailDTO()
                        {
                            TimeTableAllocationIID = 0,
                            ClassTimingID = classTiming.ClassTimingID,
                            StaffID = (long?)null,
                            SubjectID = (int?)null,
                            WeekDayID = week.WeekDayID,
                            ClassTiming = new KeyValueDTO() { Key = classTiming.ClassTimingID.ToString(), Value = classTiming.TimingDescription },
                            Employee = new KeyValueDTO() { Key = null, Value = null },
                            Subject = new KeyValueDTO() { Key = null, Value = classTiming.BreakType.BreakTypeName },
                            WeekDay = new KeyValueDTO() { Key = week.WeekDayID.ToString(), Value = week.Day.DayName },
                            IsBreakTime = true
                        });
                    }
                }

                timeTableInfoList.Add(new TimeTableAllocInfoHeaderDTO()
                {
                    //Class = new KeyValueDTO() { Key = classSection.ClassID.ToString(), Value = classSection.ClassName },
                    //Section = new KeyValueDTO() { Key = classSection.SectionID.ToString(), Value = classSection.SectionName },
                    //ClassId = classSection.ClassID,
                    //SectionID = classSection.SectionID,
                    AllocInfoDetails = detailList,
                    TimeTableAllocationIID = detailList.Count() > 0 ? detailList[0].TimeTableAllocationIID : 0
                });
            }

            return timeTableInfoList;
        }

    }
}