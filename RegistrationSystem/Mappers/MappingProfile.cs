using AutoMapper;
using RegistrationSystem.Models.Dtos;
using RegistrationSystem.Models.Entities;

namespace RegistrationSystem.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CourseType, CourseTypeDto>();
            CreateMap<CourseTypeDto, CourseType>();

            CreateMap<Instructor, InstructorDto>();
            CreateMap<InstructorDto, Instructor>();

        }
    }
}
