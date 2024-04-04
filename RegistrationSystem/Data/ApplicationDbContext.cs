using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Models.Entities;

namespace RegistrationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
    }
}
