using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Data;
using RegistrationSystem.Models.Dtos;
using RegistrationSystem.Models.Entities;

namespace RegistrationSystem.Services
{
    public class InstructorsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public InstructorsService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<InstructorDto>> GetAllAsync()
        {
            var results = await _db.Instructors.ToListAsync();

            var mappedResults = _mapper.Map<List<InstructorDto>>(results);

            return mappedResults;
        }

        public async Task<InstructorDto?> GetOneAsync(int instructorId)
        {
            var result = await _db.Instructors.FindAsync(instructorId);


            var mappedResult = _mapper.Map<InstructorDto>(result);

            return mappedResult;
        }

        public async Task<int> UpdateAsync(int instructorId, InstructorDto instructor)
        {

            Instructor? instructorToUpdate = await _db.Instructors.FindAsync(instructorId);

            if (instructorToUpdate == null)
            {
                return -2;
            }
            else
            {
                instructorToUpdate.FirstName = instructor.FirstName;
                instructorToUpdate.LastName = instructor.LastName;
                instructorToUpdate.Email = instructor.Email;
                instructorToUpdate.PhoneNumber = instructor.PhoneNumber;
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<InstructorDto> AddAsync(InstructorDto instructor)
        {
            Instructor instructorToAdd = new()
            {
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Email = instructor.Email,
                PhoneNumber = instructor.PhoneNumber,
                IsDeleted = false
            };

            _ = _db.Instructors.Add(instructorToAdd);

            // assuming an error is thrown if not added
            _ = await _db.SaveChangesAsync();

            return _mapper.Map<InstructorDto>(instructorToAdd);
        }

        public async Task<int> DeleteAsync(int instructorId)
        {
            Instructor? instructorToDelete = await _db.Instructors.FindAsync(instructorId);

            if (instructorToDelete == null)
            {
                return -2;
            }
            else
            {
                instructorToDelete.IsDeleted = true;

                return await _db.SaveChangesAsync();
            }
        }

    }
}
