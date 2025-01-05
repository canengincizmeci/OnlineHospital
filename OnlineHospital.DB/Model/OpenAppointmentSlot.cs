using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class OpenAppointmentSlot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }



        [Required]
        public DateTime AppointmentDate { get; set; }
        [ForeignKey("WeekForAppointments")]
        [Required]
        public int WeekForAppointmentId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public bool IsSelected { get; set; }
        public Doctor Doctor { get; set; }
        public WeekForAppointment WeekForAppointment { get; set; }

    }
}
