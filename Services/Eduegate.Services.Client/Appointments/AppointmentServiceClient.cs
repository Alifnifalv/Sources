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
using Eduegate.Domain;

namespace Eduegate.Services.Client.Appointments
{
    public class AppointmentServiceClient : BaseClient, IAppointmentService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string accountService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.APPOINTMENT_SERVICE);

        public AppointmentServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
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
