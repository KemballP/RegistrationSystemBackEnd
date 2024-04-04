using RegistrationSystem.Models.Entities;

namespace RegistrationSystem.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            _ = context.Database.EnsureCreated();

            if (context.CourseTypes.Any())
            {
                return; // DB has been seeded
            }

            CourseType[] courseTypes = new CourseType[]
            {
                new CourseType{TypeName="Online", TypeDescription="Online course", IsDeleted=false},
                new CourseType{TypeName="Offline", TypeDescription="Offline course", IsDeleted=false}
            };
            foreach (CourseType ct in courseTypes)
            {
                _ = context.CourseTypes.Add(ct);
            }

            if (context.Instructors.Any())
            {
                return; // DB has been seeded
            }

            Instructor[] instructors = new Instructor[]
            {
                new Instructor{FirstName ="John", LastName="Smith", Email="test1@abc.ca",PhoneNumber="123-456-7777",IsDeleted=false},
                new Instructor{FirstName ="Jane", LastName="Doe", Email="test2@abc.ca",PhoneNumber="123-456-9999",IsDeleted=false}
            };
            foreach (Instructor i in instructors)
            {
                _ = context.Instructors.Add(i);
            }
            _ = context.SaveChanges();

        }
    }
}
