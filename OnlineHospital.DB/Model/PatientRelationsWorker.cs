using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class PatientRelationsWorker
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkerId { get; set; }
        [Required]
        [StringLength(100)]
        public string WorkerName { get; set; }
        [Required]
        [StringLength(100)]
        public string WorkerUserName { get; set; }
        public DateTime? BirthYear { get; set; }
        [Required]
        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        public bool IsProfileUpdated { get; set; }
        public AppUser User { get; set; }
        [Required]
        public bool ActivityStatus { get; set; }
        [Required]
        public bool Availability { get; set; }



    }
}
