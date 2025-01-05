using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.ViewModels
{
    public class DoctorListViewModel
    {
        public int? DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public bool IsCompletedInfos { get; set; }
        public string Specialty { get; set; } = null!;




    }
}
