using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class AppointmentViewModel
    {

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

    }
}
