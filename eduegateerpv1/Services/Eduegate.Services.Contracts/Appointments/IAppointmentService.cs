using Eduegate.Services.Contracts.Salon;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Appointments
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppointmentService" in both code and config file together.
    public interface IAppointmentService
    {
        List<AppointmentDTO> GetAppointments();

        AppointmentDTO GetAppointment(long appointmentID);

        List<AppointmentDTO> GetAppointmentsByLoginID(long loginID);

        List<AppointmentDTO> SaveAppointments(List<AppointmentDTO> appointments);

        AppointmentDTO SaveAppointments(AppointmentDTO appointment);
    }
}