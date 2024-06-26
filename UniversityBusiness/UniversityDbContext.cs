using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityBusiness
{
    public class UniversityDbContext: DbContext
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FPTUniversity"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { SpecializationId=1,SpecializationName="IT"},
                new Specialization { SpecializationId = 2, SpecializationName = "BIS" },
                new Specialization { SpecializationId = 3, SpecializationName = "MATH" }
                );
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId=1, StudentName="Tran Thi Thu", SpecializationId=1},
                new Student { StudentId = 2, StudentName = "Hoang Van Bao", SpecializationId = 1 },
                new Student { StudentId = 3, StudentName = "Ngo Thi Ha", SpecializationId = 2 },
                new Student { StudentId = 4, StudentName = "Tong Van Dat", SpecializationId = 2 },
                new Student { StudentId = 5, StudentName = "Hoang Bach", SpecializationId = 3 },
                new Student { StudentId = 6, StudentName = "Nong Thi Thom", SpecializationId = 3 },
                new Student { StudentId = 7, StudentName = "Hoang Lan", SpecializationId = 3 },
                new Student { StudentId = 8, StudentName = "Le Viet Tinh", SpecializationId = 1 }
                );
        }
    }
}
