using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Data;
using RegistrationSystem.Models.Dtos;
using RegistrationSystem.Models.Entities;

namespace RegistrationSystem.Services
{
    public class CourseTypesService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CourseTypesService(ApplicationDbContext db, IMapper mapper ) {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<CourseTypeDto>> GetAllAsync()
        {
            var results = await _db.CourseTypes.ToListAsync();

            var mappedResults = _mapper.Map<List<CourseTypeDto>>(results);

            return mappedResults;
        }

        public async Task<CourseTypeDto?> GetOneAsync(int courseTypeId)
        {
            var result = await _db.CourseTypes.FindAsync(courseTypeId);


            var mappedResult = _mapper.Map<CourseTypeDto>( result );

            return mappedResult;
        }

        public async Task<int> UpdateAsync(int courseTypeId, CourseTypeDto courseType)
        {

            CourseType? courseTypeToUpdate = await _db.CourseTypes.FindAsync(courseTypeId);

            if (courseTypeToUpdate == null) {
                return -2;
            }
            else {
                courseTypeToUpdate.TypeName = courseType.TypeName;
                courseTypeToUpdate.TypeDescription = courseType.TypeDescription;

                return await _db.SaveChangesAsync();
            }
        }

        public async Task<CourseTypeDto> AddAsync(CourseTypeDto courseType)
        {
            CourseType courseTypeToAdd = new()
            {
                TypeName = courseType.TypeName,
                TypeDescription = courseType.TypeDescription,
                IsDeleted = false
            };

            _ = _db.CourseTypes.Add(courseTypeToAdd);

            // assuming an error is thrown if not added
            _ = await _db.SaveChangesAsync();

            return _mapper.Map<CourseTypeDto>(courseTypeToAdd);
        }

        public async Task<int> DeleteAsync(int courseTypeId)
        {
            CourseType? courseTypeToDelete = await _db.CourseTypes.FindAsync(courseTypeId);

            if (courseTypeToDelete == null)
            {
                return -2;
            }
            else
            {
                courseTypeToDelete.IsDeleted = true;

                return await _db.SaveChangesAsync();
            }
        }
    }
}
