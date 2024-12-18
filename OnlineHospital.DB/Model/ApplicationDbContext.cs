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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<PatientRelationsWorker> PatientRelationsWorker { get; set; }
        public DbSet<OpenAppointmentSlot> OpenAppointmentSlots { get; set; }
        public DbSet<CreatedAppointment> CreatedAppointments { get; set; }
        public DbSet<CompletedAppointment> CompletedAppointments { get; set; }
        public DbSet<WeekForAppointment> WeekForAppointments { get; set; }
    }
}

