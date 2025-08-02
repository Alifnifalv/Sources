using Eduegate.Services.Contracts.Appointments;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Framework.Services;

namespace Eduegate.Services.Appointments
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        public AppointmentDTO GetAppointment(long appointmentID)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentDTO> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public List<AppointmentDTO> GetAppointmentsByLoginID(long loginID)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentDTO> SaveAppointments(List<AppointmentDTO> appointments)
        {
            throw new NotImplementedException();
        }

        public AppointmentDTO SaveAppointments(AppointmentDTO appointment)
        {
            throw new NotImplementedException();
        }
    }
}