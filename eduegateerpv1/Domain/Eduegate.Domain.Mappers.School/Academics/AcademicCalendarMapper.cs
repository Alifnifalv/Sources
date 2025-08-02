using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AcademicCalendarMapper : DTOEntityDynamicMapper
    {
        public static AcademicCalendarMapper Mapper(CallContext context)
        {
            var mapper = new AcademicCalendarMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AcadamicCalendarDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AcadamicCalendars.Where(X => X.AcademicCalendarID == IID)
                  .AsNoTracking()
                  .FirstOrDefault();

                var entitydto = new AcademicYearCalendarEventDTO()
                {

                };
                return ToDTOString(entitydto);
            }
        }

        public List<AcademicYearDTO> GetAcademicYearDataByCalendarID(long calendarID)
        {
            var academicList = new List<AcademicYearDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AcadamicCalendars
                    .Include(x => x.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.AcademicCalendarID == calendarID);

                var data = entity == null ? null : new AcademicYearDTO()
                {
                    AcademicYearID = entity.AcademicYearID.Value,
                    StartDate = entity.AcademicYear.StartDate,
                    EndDate = entity.AcademicYear.EndDate,
                };

                if (data != null)
                {
                    academicList.Add(data);
                }
            }
            return academicList;
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AcadamicCalendarDTO;
            var academicEvents = new List<AcademicYearCalendarEvent>();
            var academicYearCalendarEvent = new AcademicYearCalendarEvent();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                foreach (var eventDet in toDto.AcademicYearCalendarEventDTO)
                {
                    var entitySameColor = dbContext.AcademicYearCalendarEvents
                        .Where(a => ((a.AcademicYearCalendarEventIID != eventDet.AcademicYearCalendarEventIID)
                        && (a.AcadamicCalendar.AcademicCalendarID == toDto.AcademicCalendarID))
                        && a.ColorCode == eventDet.ColorCode && a.EventTitle.ToUpper() != eventDet.EventTitle.ToUpper())
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (entitySameColor != null)
                    {
                        return "#101";
                    }

                    academicYearCalendarEvent = new AcademicYearCalendarEvent()
                    {
                        AcademicYearCalendarEventIID = eventDet.AcademicYearCalendarEventIID,
                        AcademicYearCalendarEventTypeID = eventDet.AcademicCalendarEventTypeID,
                        AcademicCalendarID = eventDet.AcademicCalendarID,
                        EventTitle = eventDet.EventTitle?.Trim(),
                        Description = eventDet.Description?.Trim(),
                        StartDate = eventDet.StartDate,
                        EndDate = eventDet.EndDate,
                        NoofHours = eventDet.NoofHours,
                        IsThisAHoliday = eventDet.IsThisAHoliday,
                        IsEnableReminders = eventDet.IsEnableReminders,
                        ColorCode = eventDet.ColorCode,
                        CreatedBy = (int)_context.LoginID,
                        CreatedDate = DateTime.Now,
                    };
                }

                if (academicYearCalendarEvent.AcademicYearCalendarEventIID == 0)
                {
                    dbContext.Entry(academicYearCalendarEvent).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(academicYearCalendarEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();

                UpdateCalendarEntriesInSaveEvents(academicYearCalendarEvent);
            }

            return (Convert.ToString(toDto.AcademicCalendarID));
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID, long academicCalendarID)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var defaultWorkingHours = new Domain.Setting.SettingBL(null).GetSettingValue<decimal>("HOURSPERDAY", 0, 8);
            byte? calendarTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ACADEMIC_CALENDAR_TYPE_ID");

            var academicCalenderEventList = new List<AcademicCalenderEventDateDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? workingHours = null;

                var calendarData = dbContext.AcadamicCalendars
                    .Where(c => c.AcademicCalendarID == academicCalendarID && c.AcademicCalendarStatusID == 1)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (calendarData != null)
                {
                    var academicYearData = calendarData.AcademicYear;

                    var academicYearDateList = new List<AcademicCalenderEventDateDTO>();

                    if (academicYearData != null)
                    {
                        var startDateMonth = academicYearData.StartDate.Value.Month;
                        var startDateYear = academicYearData.StartDate.Value.Year;
                        var endDateMonth = academicYearData.EndDate.Value.Month;
                        var endDateYear = academicYearData.EndDate.Value.Year;

                        var entitiesEvents = dbContext.AcademicYearCalendarEvents
                            .Where(a => a.AcademicCalendarID == academicCalendarID && a.AcadamicCalendar.AcademicCalendarStatusID == 1)
                            .Include(i => i.AcadamicCalendar)
                            .Include(i => i.AcademicYearCalendarEventType)
                            .AsNoTracking()
                            .ToList();

                        for (DateTime aDate = academicYearData.StartDate.Value.Date; aDate.Date <= academicYearData.EndDate.Value.Date; aDate = aDate.AddDays(1))
                        {
                            var eventDataList = entitiesEvents.Count > 0 ? entitiesEvents.Where(x => aDate.Date >= x.StartDate.Value.Date && aDate.Date <= x.EndDate.Value.Date).ToList() : null;
                            if (eventDataList != null && eventDataList.Count > 0)
                            {
                                foreach (var eventData in eventDataList)
                                {
                                    if (eventData.AcadamicCalendar != null)
                                    {
                                        if (eventData.AcadamicCalendar.CalendarTypeID != calendarTypeID)
                                        {
                                            workingHours = eventData.NoofHours != null ? eventData.NoofHours : defaultWorkingHours;
                                        }
                                    }

                                    academicCalenderEventList.Add(new AcademicCalenderEventDateDTO()
                                    {
                                        AcademicYearCalendarEventID = eventData.AcademicYearCalendarEventIID,
                                        AcademicCalendarID = eventData.AcademicCalendarID,
                                        AcademicYearID = eventData.AcadamicCalendar.AcademicYearID,
                                        EventTitle = eventData.EventTitle,
                                        Description = eventData.Description,
                                        StartDate = eventData.StartDate,
                                        EndDate = eventData.EndDate,
                                        EventStartDateString = eventData.StartDate.HasValue ? eventData.StartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        EventEndDateString = eventData.EndDate.HasValue ? eventData.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        IsThisAHoliday = eventData.IsThisAHoliday,
                                        IsEnableReminders = eventData.IsEnableReminders,
                                        ColorCode = eventData.ColorCode,
                                        ActualDate = aDate.Date,
                                        ActualDateString = aDate.Date != null ? aDate.Date.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                        Day = aDate.Day,
                                        Month = aDate.Month,
                                        MonthName = new DateTimeFormatInfo().GetMonthName(aDate.Month),
                                        Year = aDate.Year,
                                        AcademicCalendarStatusID = eventData.AcadamicCalendar.AcademicCalendarStatusID,
                                        AcademicYearCalendarEventTypeID = eventData.AcademicYearCalendarEventTypeID,
                                        AcademicYearCalendarEventType = eventData.AcademicYearCalendarEventTypeID.HasValue ? eventData.AcademicYearCalendarEventType.Description : null,
                                        NoofHours = workingHours,
                                    });
                                }
                            }
                            else
                            {
                                workingHours = defaultWorkingHours;
                                if (calendarData?.CalendarTypeID != calendarTypeID)
                                {
                                    academicCalenderEventList.Add(new AcademicCalenderEventDateDTO()
                                    {
                                        AcademicYearCalendarEventID = 0,
                                        AcademicCalendarID = calendarData.AcademicCalendarID,
                                        AcademicYearID = calendarData.AcademicYearID,
                                        EventTitle = null,
                                        Description = null,
                                        StartDate = aDate,
                                        EndDate = aDate,
                                        IsThisAHoliday = false,
                                        IsEnableReminders = false,
                                        ColorCode = null,
                                        ActualDate = aDate.Date,
                                        Day = aDate.Day,
                                        Month = aDate.Month,
                                        MonthName = new DateTimeFormatInfo().GetMonthName(aDate.Month),
                                        Year = aDate.Year,
                                        AcademicCalendarStatusID = calendarData.AcademicCalendarStatusID,
                                        AcademicYearCalendarEventTypeID = null,
                                        AcademicYearCalendarEventType = null,
                                        NoofHours = workingHours,
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return academicCalenderEventList;
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicCalenderByMonthYear(int month, int year)
        {
            var academicCalenderEventList = new List<AcademicCalenderEventDateDTO>();

            var defaultWorkingHours = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOURSPERDAY");
            var academicCalenderTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ACADEMIC_CALENDAR_TYPE_ID");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                byte? calendarTypeID = byte.Parse(academicCalenderTypeID);

                decimal? workingHours = null;

                var acadamicCalenderEvent = dbContext.AcademicYearCalendarEvents.Where(acevent =>
                                              acevent.AcadamicCalendar.AcademicCalendarStatusID == 1 && acevent.IsThisAHoliday == true &&
                                             ((acevent.StartDate.Value.Month <= month && acevent.EndDate.Value.Month >= month) &&
                                             (acevent.StartDate.Value.Year <= year && acevent.EndDate.Value.Year >= year)))
                                             .Include(i => i.AcadamicCalendar).ThenInclude(i => i.AcademicYearCalendarStatus)
                                             .Include(i => i.AcademicYearCalendarEventType)
                                             .AsNoTracking()
                                             .ToList();

                foreach (var eventData in acadamicCalenderEvent)
                {
                    for (DateTime adate = eventData.StartDate.Value.Date; adate.Date <= eventData.EndDate.Value.Date; adate = adate.AddDays(1))
                    {
                        if (eventData.AcadamicCalendar?.CalendarTypeID != calendarTypeID)
                        {
                            workingHours = eventData.NoofHours != null ? eventData.NoofHours : decimal.Parse(defaultWorkingHours);
                        }

                        academicCalenderEventList.Add(new AcademicCalenderEventDateDTO()
                        {
                            AcademicYearCalendarEventID = eventData.AcademicYearCalendarEventIID,
                            AcademicCalendarID = eventData.AcademicCalendarID,
                            AcademicYearID = eventData.AcadamicCalendar.AcademicYearID,
                            EventTitle = eventData.EventTitle,
                            Description = eventData.Description,
                            StartDate = eventData.StartDate,
                            EndDate = eventData.EndDate,
                            IsThisAHoliday = eventData.IsThisAHoliday,
                            IsEnableReminders = eventData.IsEnableReminders,
                            ColorCode = eventData.ColorCode,
                            ActualDate = adate,
                            Day = adate.Day,
                            Month = adate.Month,
                            MonthName = new DateTimeFormatInfo().GetMonthName(adate.Month),
                            Year = adate.Year,
                            AcademicCalendarStatusID = eventData.AcadamicCalendar.AcademicCalendarStatusID,
                            AcademicYearCalendarEventTypeID = eventData.AcademicYearCalendarEventTypeID,
                            AcademicYearCalendarEventType = eventData.AcademicYearCalendarEventTypeID.HasValue ? eventData.AcademicYearCalendarEventTypeID.ToString() : null,
                            NoofHours = workingHours
                        });
                    }
                }
            }

            return academicCalenderEventList;
        }

        public void DeleteEntity(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entitiesAcYearCalEvent = dbContext.AcademicYearCalendarEvents.Where(a => a.AcademicYearCalendarEventIID == academicYearCalendarEventIID).AsNoTracking().FirstOrDefault();

                if (entitiesAcYearCalEvent != null)
                {
                    dbContext.AcademicYearCalendarEvents.Remove(entitiesAcYearCalEvent);
                    dbContext.SaveChanges();
                }

                UpdateCalendarEntriesInDeleteEvents(entitiesAcYearCalEvent);
            }
        }

        public List<AcademicCalenderEventDateDTO> GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            var calendarEventDTOList = new List<AcademicCalenderEventDateDTO>();

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var calendarData = dbContext.AcadamicCalendars
                    .Where(c => c.AcademicCalendarID == calendarID && c.AcademicCalendarStatusID == 1)
                    .Include(i => i.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (calendarData != null)
                {
                    var academicYearData = calendarData.AcademicYear;

                    if (academicYearData != null)
                    {
                        var startDateMonth = academicYearData.StartDate.Value.Month;
                        var startDateYear = academicYearData.StartDate.Value.Year;
                        var endDateMonth = academicYearData.EndDate.Value.Month;
                        var endDateYear = academicYearData.EndDate.Value.Year;

                        for (var year = startDateYear; year <= endDateYear; year++)
                        {
                            for (var month = 1; month <= 12; month++)
                            {
                                if ((month >= startDateMonth && year == startDateYear) || (month <= endDateMonth && year == endDateYear))
                                {
                                    calendarEventDTOList.Add(new AcademicCalenderEventDateDTO()
                                    {
                                        Month = Convert.ToByte(month),
                                        Year = year,
                                        MonthName = new DateTimeFormatInfo().GetMonthName(month),
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return calendarEventDTOList;
        }

        public string CheckAndInsertCalendarEntries(long calendarID)
        {
            var data = string.Empty;

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var calendarEntryList = dbContext.CalendarEntries.Where(x => x.AcademicCalendarID == calendarID)
                    .AsNoTracking()
                    .ToList();

                if (calendarEntryList.Count == 0)
                {
                    data = InsertCalendarEntries(calendarID);
                }
                else
                {
                    data = "True";
                }
                return data;
            }
        }

        public string InsertCalendarEntries(long calendarID)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception  
            //var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            String[] dataResult = null;
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand("schools.SPS_Update_CalendarEntryLogs", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@AcademicCalendarID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@AcademicCalendarID"].Value = calendarID;

                DataSet dt = new DataSet();

                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowValue = row.ItemArray[0].ToString().TrimEnd('_');
                        dataResult = (rowValue.Split('_'));
                    }
                }


            }
            return "Successfully Inserted";
        }

        public void UpdateCalendarEntriesInSaveEvents(AcademicYearCalendarEvent eventData)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? workingHours = new Domain.Setting.SettingBL(null).GetSettingValue<decimal>("HOURSPERDAY");

                if (eventData.NoofHours == null)
                {
                    eventData.NoofHours = 0;
                }

                //decimal? workingHours = null;
                for (DateTime aDate = eventData.StartDate.Value.Date; aDate.Date <= eventData.EndDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    var calendarEntryData = dbContext.CalendarEntries.Where(x => x.AcademicCalendarID == eventData.AcademicCalendarID && (x.CalendarDate.Value.Day == aDate.Day && x.CalendarDate.Value.Month == aDate.Month && x.CalendarDate.Value.Year == aDate.Year))
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (calendarEntryData != null)
                    {
                        if (calendarEntryData.NoofHours == workingHours)
                        {
                            calendarEntryData.NoofHours = eventData.NoofHours;
                        }
                        else
                        {
                            var totalHours = calendarEntryData.NoofHours + eventData.NoofHours;
                            calendarEntryData.NoofHours = totalHours;
                        }

                        calendarEntryData.UpdatedBy = Convert.ToInt32(_context.LoginID);
                        calendarEntryData.UpdatedDate = DateTime.Now;

                        dbContext.Entry(calendarEntryData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public void UpdateCalendarEntriesInDeleteEvents(AcademicYearCalendarEvent eventData)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? workingHours = new Domain.Setting.SettingBL(null).GetSettingValue<decimal>("HOURSPERDAY");

                //decimal? workingHours = null;
                for (DateTime aDate = eventData.StartDate.Value.Date; aDate.Date <= eventData.EndDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    if (eventData.NoofHours == null)
                    {
                        eventData.NoofHours = workingHours;
                    }

                    var calendarEntryData = dbContext.CalendarEntries
                        .Where(x => x.AcademicCalendarID == eventData.AcademicCalendarID && (x.CalendarDate.Value.Day == aDate.Day && x.CalendarDate.Value.Month == aDate.Month && x.CalendarDate.Value.Year == aDate.Year))
                        .AsNoTracking().FirstOrDefault();

                    if (calendarEntryData != null)
                    {
                        if (calendarEntryData.NoofHours == 0)
                        {
                            calendarEntryData.NoofHours = workingHours;
                        }
                        else
                        {
                            var totalHours = calendarEntryData.NoofHours - eventData.NoofHours;
                            if (totalHours <= 0)
                            {
                                totalHours = workingHours;
                            }
                            calendarEntryData.NoofHours = totalHours;
                        }

                        calendarEntryData.UpdatedBy = Convert.ToInt32(_context.LoginID);
                        calendarEntryData.UpdatedDate = DateTime.Now;

                        dbContext.Entry(calendarEntryData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public List<AcademicCalenderEventDateDTO> GetCalendarEventsByCalendarID(long calendarID)
        {
            var academicCalenderEventList = new List<AcademicCalenderEventDateDTO>();

            var defaultWorkingHours = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOURSPERDAY");
            var calendarTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ACADEMIC_CALENDAR_TYPE_ID");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                decimal? workingHours = null;

                var acadamicCalenderEvent = dbContext.AcademicYearCalendarEvents
                    .Where(x => x.AcademicCalendarID == calendarID)
                    .Include(i => i.AcadamicCalendar).ThenInclude(i => i.AcademicYearCalendarStatus)
                    .Include(i => i.AcademicYearCalendarEventType)
                    .AsNoTracking().ToList();

                foreach (var eventData in acadamicCalenderEvent)
                {
                    for (DateTime adate = eventData.StartDate.Value.Date; adate.Date <= eventData.EndDate.Value.Date; adate = adate.AddDays(1))
                    {
                        if (eventData.AcadamicCalendar?.CalendarTypeID != calendarTypeID)
                        {
                            workingHours = eventData.NoofHours != null ? eventData.NoofHours : decimal.Parse(defaultWorkingHours);
                        }

                        academicCalenderEventList.Add(new AcademicCalenderEventDateDTO()
                        {
                            AcademicYearCalendarEventID = eventData.AcademicYearCalendarEventIID,
                            AcademicCalendarID = eventData.AcademicCalendarID,
                            AcademicYearID = eventData.AcadamicCalendar.AcademicYearID,
                            EventTitle = eventData.EventTitle,
                            Description = eventData.Description,
                            StartDate = eventData.StartDate,
                            EndDate = eventData.EndDate,
                            IsThisAHoliday = eventData.IsThisAHoliday,
                            IsEnableReminders = eventData.IsEnableReminders,
                            ColorCode = eventData.ColorCode,
                            ActualDate = adate,
                            Day = adate.Day,
                            Month = adate.Month,
                            MonthName = new DateTimeFormatInfo().GetMonthName(adate.Month),
                            Year = adate.Year,
                            AcademicCalendarStatusID = eventData.AcadamicCalendar.AcademicCalendarStatusID,
                            AcademicYearCalendarEventTypeID = eventData.AcademicYearCalendarEventTypeID,
                            AcademicYearCalendarEventType = eventData.AcademicYearCalendarEventTypeID.HasValue ? eventData.AcademicYearCalendarEventTypeID.ToString() : null,
                            NoofHours = workingHours
                        });
                    }
                }
            }

            return academicCalenderEventList;
        }

    }
}