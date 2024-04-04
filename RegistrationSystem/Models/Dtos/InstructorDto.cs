using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Models.Dtos
{
    public class InstructorDto
    {
        public int InstructorId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
