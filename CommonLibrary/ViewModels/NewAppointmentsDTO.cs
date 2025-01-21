using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class NewAppointmentsDTO
    {
        public int createdAppointmentId { get; set; }
        public int patientId { get; set; }
        public string patientName { get; set; }
        public DateTime appointmentDate { get; set; }


    }
}
