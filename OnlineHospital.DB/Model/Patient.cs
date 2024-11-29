using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatinetId { get; set; }

        public DateTime BirthYear { get; set; }
        [Required]
        [ForeignKey("AppUser")]
        public string UserId { get; set; }


        public AppUser User { get; set; }
        [Required]
        public bool ActivityStatus { get; set; }




    }
}
