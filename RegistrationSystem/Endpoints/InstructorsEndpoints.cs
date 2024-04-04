using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using RegistrationSystem.Models.Dtos;
using RegistrationSystem.Services;
using static System.Net.Mime.MediaTypeNames;

namespace RegistrationSystem.Endpoints
{
    public static class InstructorsEndpoints
    {
        public static void RegisterInstructorsEndpoints(this WebApplication app)
        {
            _ = app.MapGet("/instructors", async (InstructorsService service) =>
            {
                return await service.GetAllAsync();
            });

            _ = app.MapGet("/instructors/{instructorId}", async (int instructorId, InstructorsService service) =>
            {
                InstructorDto? instructor = await service.GetOneAsync(instructorId);

                return instructor == null ? Results.NotFound() : Results.Ok(instructor);
            });

            _ = app.MapPut("/instructors/{instructorId}", async (int instructorId, InstructorDto instructor, InstructorsService service) =>
            {
                if (!MiniValidator.TryValidate(instructor, out var errors))
                {
                    return Results.ValidationProblem(errors);
                }

                int result = await service.UpdateAsync(instructorId, instructor);

                if (result == -2)
                {
                    return Results.NotFound();
                }
                else if (result == 1)
                {
                    return Results.NoContent();
                }
                else
                {
                    return Results.Problem();
                }
            });

            _ = app.MapPost("/instructors", async (InstructorDto instructor, InstructorsService service) =>
            {
                if (!MiniValidator.TryValidate(instructor, out var errors))
                {
                    return Results.ValidationProblem(errors);
                }

                InstructorDto addedInstructor = await service.AddAsync(instructor);

                return Results.Created($"/instructors/{addedInstructor.InstructorId}", addedInstructor);
            });

            _ = app.MapDelete("/instructors/{instructorId}", async (int instructorId, InstructorsService service) =>
            {
                var result = await service.DeleteAsync(instructorId);

                if (result == -2)
                {
                    return Results.NotFound();
                }
                else if (result == 1)
                {
                    return Results.Ok();
                }
                else
                {
                    return Results.Problem();
                }
            });
        }
    }
}
