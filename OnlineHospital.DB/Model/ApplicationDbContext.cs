using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHospital.DB.Model
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {






        }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        DbSet<Administrator> Administrators { get; set; }
        DbSet<PatientRelationsWorker> PatientRelationsWorker { get; set; }
        DbSet<OpenAppointmentSlot> OpenAppointmentSlots { get; set; }
        DbSet<CreatedAppointment> CreatedAppointments { get; set; }
        DbSet<CompletedAppointment> CompletedAppointments { get; set; }

    }
}
