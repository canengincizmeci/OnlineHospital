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
        public int DoctorId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [ForeignKey("AppUser")]
        public string UserId { get; set; }


        public AppUser User { get; set; }


    }
}
