using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class CompletedAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompletedAppointmentId { get; set; }
        [Required]
        public string Results { get; set; }
        [Required]
        public DateTime CompletedDate { get; set; }
        [Required]
        [ForeignKey("CreatedAppointment")]
        public int CreatedAppointmentId { get; set; }

        public CreatedAppointment CreatedAppointment { get; set; }
    }
}
