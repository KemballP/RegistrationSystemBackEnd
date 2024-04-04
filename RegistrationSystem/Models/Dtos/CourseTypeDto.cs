using System.ComponentModel.DataAnnotations;

namespace RegistrationSystem.Models.Dtos
{
    public class CourseTypeDto
    {
        
        public int CourseTypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string? TypeName { get; set; }

        [Required]
        [StringLength(50)]
        public string? TypeDescription { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
