using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class CreatedAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreatedAppointmentId { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }


        [Required]
        [ForeignKey("OpenAppointmentSlot")]
        public int OpenAppointmentSlotId { get; set; }


        public OpenAppointmentSlot OpenAppointmentSlot { get; set; }
        public Patient Patient { get; set; }

    }
}
