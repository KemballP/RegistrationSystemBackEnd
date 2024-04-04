using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using RegistrationSystem.Models.Dtos;
using RegistrationSystem.Services;
using static System.Net.Mime.MediaTypeNames;

namespace RegistrationSystem.Endpoints
{
    public static class CourseTypesEndpoints
    {
        public static void RegisterCourseTypesEndpoints(this WebApplication app)
        {
            _ = app.MapGet("/courseTypes", async (CourseTypesService service) =>
            {
                return await service.GetAllAsync();
            });

            _ = app.MapGet("/courseTypes/{courseTypeId}", async (int courseTypeId, CourseTypesService service) =>
            {
                CourseTypeDto? courseType = await service.GetOneAsync(courseTypeId);

                return courseType == null ? Results.NotFound() : Results.Ok(courseType);
            });

            _ = app.MapPut("/courseTypes/{courseTypeId}", async (int courseTypeId, CourseTypeDto courseType, CourseTypesService service) =>
            {
                if (!MiniValidator.TryValidate(courseType, out var errors))
                {
                    return Results.ValidationProblem(errors);
                }

                int result = await service.UpdateAsync(courseTypeId, courseType);

                if (result == -2)
                {
                    return Results.NotFound();
                }
                else if (result == 1) 
                {
                    return Results.NoContent();
                } else
                {
                    return Results.Problem();
                }
            });

            _ = app.MapPost("/courseTypes", async (CourseTypeDto courseType, CourseTypesService service) =>
            {
                if (!MiniValidator.TryValidate(courseType, out var errors))
                {
                    return Results.ValidationProblem(errors);
                }

                CourseTypeDto addedCourseType = await service.AddAsync(courseType);

                return Results.Created($"/courseTypes/{addedCourseType.CourseTypeId}", addedCourseType);
            });

            _ = app.MapDelete("/courseTypes/{courseTypeId}", async (int courseTypeId, CourseTypesService service) =>
            {
                var result = await service.DeleteAsync(courseTypeId);

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
