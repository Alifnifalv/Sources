using Eduegate.Services.Contracts.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Salon;
using Eduegate.Service.Client;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Services.Appointments;

namespace Eduegate.Services.Client.Direct.Appointments
{
    public class AppointmentServiceClient : IAppointmentService
    {
        AppointmentService service = new AppointmentService();

        public AppointmentServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

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
