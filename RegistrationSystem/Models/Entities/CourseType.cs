namespace RegistrationSystem.Models.Entities
{
    public class CourseType
    {
        public int CourseTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeDescription { get; set; }
        public bool IsDeleted { get; set; }

    }
}
