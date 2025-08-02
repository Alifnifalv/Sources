using Eduegate.Domain.Payroll;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Schedulers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.MobileAppWrapper
{
    public class AppMaidBL
    {
        private CallContext Context { get; set; }

        public AppMaidBL(CallContext context)
        {
            Context = context;
        }

        public EmployeeDTO GetEmployeeDtailsByLoginUserID(long loginUserID)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO GetEmployeeDtails(long employeeID)
        {
            return new EmployeeBL(Context).GetEmployee(employeeID);
        }

        public List<EmployeeScheduleDTO> GetEmployeeSchedules(long employeeID, DateTime scehduleDate)
        {
            throw new NotImplementedException();
        }

        public EmployeeScheduleDetailsDTO GetEmployeeScheduleDetails(long employeeID, DateTime scheduleDate)
        {
            var detailDTO = new EmployeeScheduleDetailsDTO();
            var employeeDetails = new EmployeeRepository().GetEmployee(employeeID);

            if (employeeDetails.EmployeeRoleMaps.Where(a => a.EmployeeRole.EmployeeRoleName.Equals("Maid")).Count() > 0)
            {
                var mapper = Mappers.Schedule.MaidScheduleMapper.Mapper(Context);
                var scheduleEntities = mapper.GetScheduleDetails(employeeDetails.EmployeeAlias, scheduleDate);
                detailDTO = mapper.ToDTO(scheduleEntities);
                var driverSchedule = Mappers.Schedule.DriverScheduleMapper.Mapper(Context).GetNextPickupDetailsByMaid(employeeDetails.EmployeeAlias, scheduleDate);
                detailDTO.PickUpDetail = driverSchedule == null ? null : driverSchedule.PickUp_Location;
            }
            else if (employeeDetails.EmployeeRoleMaps.Where(a => a.EmployeeRole.EmployeeRoleName.Equals("Driver")).Count() > 0)
            {
                var mapper = Mappers.Schedule.DriverScheduleMapper.Mapper(Context);
                var scheduleEntities = mapper.GetScheduleDetails(employeeDetails.EmployeeAlias, scheduleDate);
                detailDTO = mapper.ToDTO(scheduleEntities);
            }

            detailDTO.EmployeeName = employeeDetails.EmployeeName;
            detailDTO.EmployeeID = employeeDetails.EmployeeIID;
            detailDTO.AmountReceived = detailDTO.Schedules.Sum(a=> a.ReceivedAmount);
            detailDTO.TotalHours = detailDTO.Schedules.Sum(a => a.Duration);
            detailDTO.RemainingHours = detailDTO.TotalHours + (detailDTO.Schedules.Where(a=> a.StatusID == 4)).Sum(a => a.Duration);
            return detailDTO;
        }

        public EmployeeScheduleDetailsDTO GetEmployeeScheduleDetailsByTime(long employeeID, DateTime scheduleDate, string time)
        {
            var detailDTO = new EmployeeScheduleDetailsDTO();
            var employeeDetails = new EmployeeRepository().GetEmployee(employeeID);
            var timeWithDate = DateTime.ParseExact("01/01/1900 " + time, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture); 

            if (employeeDetails.EmployeeRoleMaps.Where(a => a.EmployeeRole.EmployeeRoleName.Equals("Driver")).Count() > 0)
            {
                var mapper = Mappers.Schedule.DriverScheduleMapper.Mapper(Context);
                var scheduleEntities = mapper.GetScheduleDetails(employeeDetails.EmployeeAlias, scheduleDate, timeWithDate);
                detailDTO = mapper.ToDTO(scheduleEntities);
                detailDTO.Schedules = detailDTO.Schedules.Where(a => a.Time.Equals(timeWithDate.ToString("hh:mm tt"))).ToList();
            }

            detailDTO.EmployeeName = employeeDetails.EmployeeName;
            detailDTO.EmployeeID = employeeDetails.EmployeeIID;
            detailDTO.AmountReceived = detailDTO.Schedules.Sum(a => a.ReceivedAmount);
            detailDTO.TotalHours = detailDTO.Schedules.Sum(a => a.Duration);
            detailDTO.RemainingHours = detailDTO.TotalHours + (detailDTO.Schedules.Where(a => a.StatusID == 4)).Sum(a => a.Duration);
            return detailDTO;
        }

        public EmployeeScheduleSummaryInfoDTO GetEmployeeSummaryScheduleDetails(long employeeID, DateTime scheduleDate)
        {
            var detailDTO = new EmployeeScheduleSummaryInfoDTO();
            var employeeDetails = new EmployeeRepository().GetEmployee(employeeID);
            if (employeeDetails.EmployeeRoleMaps.Where(a => a.EmployeeRole.EmployeeRoleName.Equals("Driver")).Count() > 0)
            {
                var mapper = Mappers.Schedule.DriverScheduleMapper.Mapper(Context);
                var scheduleEntities = mapper.GetScheduleDetails(employeeDetails.EmployeeAlias, scheduleDate);
                detailDTO = mapper.ToScheduleDTO(scheduleEntities);
            }

            detailDTO.EmployeeName = employeeDetails.EmployeeName;
            detailDTO.EmployeeID = employeeDetails.EmployeeIID;
            //detailDTO.AmountReceived = detailDTO.Schedules.Sum(a => a.ReceivedAmount);
            //detailDTO.TotalHours = detailDTO.Schedules.Sum(a => a.Duration);
            //detailDTO.RemainingHours = detailDTO.TotalHours + (detailDTO.Schedules.Where(a => a.StatusID == 4)).Sum(a => a.Duration);
            return detailDTO;
        }

        public EmployeeScheduleDTO GetNextScheduleDetails(long employeeID, DateTime scehduleDate)
        {
            throw new NotImplementedException();
        }

        public EmployeeScheduleDTO GetEmployeeScheduleByDespatchID(long despatchID)
        {
            var mapper = Mappers.Schedule.MaidScheduleMapper.Mapper(Context);
            var scheduleEntities = mapper.GetScheduleDetails(despatchID);
            var schedule = mapper.ToDTO(scheduleEntities);
            schedule.AmountDue = new Accounts.AccountingBL(Context).GetCustomerArrears(scheduleEntities.CustomerCode);
            var getLastLocation = new MutualRepository().GetLastGeoLocation("customer", schedule.CustomerID.ToString());
            if (getLastLocation != null)
            {
                schedule.Location = new GeoLocationDTO() { lat = getLastLocation.Latitude, lon = getLastLocation.Longitude };
            }
            return schedule;
        }

        public EmployeeScheduleDTO GetDriverScheduleByScheduleID(long scheduleID)
        {
            var mapper = Mappers.Schedule.DriverScheduleMapper.Mapper(Context);
            var scheduleEntities = mapper.GetScheduleDetails(scheduleID);
          
            var dto = mapper.ToDTO(scheduleEntities);

            var getLastLocation = new MutualRepository().GetLastGeoLocation("area", scheduleEntities.AreaId.ToString());
            if (getLastLocation != null)
            {
                dto.Location = new GeoLocationDTO() { lat = getLastLocation.Latitude, lon = getLastLocation.Longitude };
            }

            return dto;
        }

        public OperationResultDTO MaidScheduleUpdate(EmployeeScheduleDTO schedule)
        {
            try
            {
                var mapper = Mappers.Schedule.MaidScheduleMapper.Mapper(Context);
                var scheduleDetail = mapper.GetScheduleDetails(schedule.DespatchID);
               
                var despatchMapper = Mappers.Schedule.DespatchMapper.Mapper(Context);
                var despatch = despatchMapper.GetDespatchByDesptach(schedule.DespatchID);

                if (despatch == null) despatch = despatchMapper.ToEntity(schedule);
                despatch.StatusID = schedule.StatusID;
                despatch.ReceivedAmount = schedule.ReceivedAmount;

                despatchMapper.UpdateDespatch(despatch);
                return new OperationResultDTO() { operationResult = OperationResult.Success };
            }
            catch
            {
                return new OperationResultDTO() { operationResult = OperationResult.Error };
            }            
        }

        public OperationResultDTO DriverScheduleUpdate(EmployeeScheduleDTO schedule)
        {
            try
            {
                var mapper = Mappers.Schedule.DriverScheduleMapper.Mapper(Context);
                var scheduleDetail = mapper.GetScheduleDetails(schedule.ScheduleID);
                scheduleDetail.StatusId = schedule.StatusID;
                var driverMapper = Mappers.Schedule.ScheduleDriverMapper.Mapper(Context);
                var driverSchedule = driverMapper.GetDriverScheduleBySchedule(schedule.ScheduleID);

                if (driverSchedule == null) driverSchedule = driverMapper.FromvSchedule(scheduleDetail);
                driverSchedule.StatusId = schedule.StatusID;
                driverMapper.UpdateDriverSchedule(driverSchedule);
                return new OperationResultDTO() { operationResult = OperationResult.Success };
            }
            catch(Exception ex)
            {
                return new OperationResultDTO() { operationResult = OperationResult.Error, Message = ex.Message };
            }
        }

        public OperationResultDTO SaveGeoLocation(GeoLocationLogDTO location)
        {
            try
            {
                new MutualRepository().SaveGeoLocation(Mappers.Mutual.GeoLocationLogMapper.Mapper(Context).ToEntity(location));
                return new OperationResultDTO() { operationResult = OperationResult.Success };
            }
            catch (Exception ex)
            {
                return new OperationResultDTO() { operationResult = OperationResult.Error, Message = ex.Message };
            }
        }
    }
}
