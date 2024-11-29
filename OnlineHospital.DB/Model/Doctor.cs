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
        [Required]
        [StringLength(100)]
        public string DoctorUserName { get; set; }
        public DateTime BirthYear { get; set; }

        [Required]
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("DoctorSpecialty")]
        public int MedicalSpecialtyId { get; set; }

        public AppUser User { get; set; }
        public DoctorSpecialty Specialty { get; set; }
        [Required]
        public bool ActivityStatus { get; set; }

    }
}
