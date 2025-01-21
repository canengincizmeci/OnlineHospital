using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class CompletedAppointmentDTO
    {
        public int createdAppointmentId { get; set; }
        public DateTime completedDate { get; set; }
        public string result { get; set; } = null!;
    }
}
