using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [Required]
        [StringLength(100)]
        public string DoctorName { get; set; }

        public DateTime? BirthYear { get; set; }

        [Required]
        [ForeignKey("AppUser")]
        public string UserId { get; set; }

        [ForeignKey("DoctorSpecialty")]
        [Required]
        public int DoctorSpecialtyId { get; set; }

        public AppUser User { get; set; }
        public DoctorSpecialty DoctorSpecialty { get; set; }
        [Required]
        public bool ActivityStatus { get; set; }
        public bool IsProfileUpdated { get; set; }



    }
}
