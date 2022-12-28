using MiniValidation;
using SeventhSeg.Application.Common;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace SeventhSeg.API.APIEndpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            app.MapGet("/api/servers/{serverId}/videos", async (string serverId, IMovieService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID entered is not valid.");

                var movies = await service.GetMoviesByServerIdAsync(serverId);

                if (movies is null) return Results.NotFound("Movies not found");

                return Results.Ok(movies);

            }).Produces<IEnumerable<MovieDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("ListAllMoviesOnTheServer")
                .WithTags("Movies")
                .WithMetadata(new SwaggerOperationAttribute(summary: "List registration data of all videos on a server."));

            app.MapGet("/api/servers/{serverId}/videos/{videoId}", async (string serverId, string videoId, IMovieService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID server entered is not valid.");
                if (GuidTest.IsGUID(videoId) is false) return Results.BadRequest("GUID movie entered is not valid.");

                var movie = await service.GetByIdAsync(serverId, videoId);

                if (movie is null)
                {
                    return Results.NotFound("Movie not found");
                }

                return Results.Ok(movie);

            }).Produces<MovieDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("RetrieveRegistrationDataFromVideo​")
                .WithTags("Movies")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve registration data from a video"));

            app.MapGet("/api/servers/{serverId}/videos/{videoId}/binary", async (string serverId, string videoId, IMovieService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID server entered is not valid.");
                if (GuidTest.IsGUID(videoId) is false) return Results.BadRequest("GUID movie entered is not valid.");

                var movie = await service.GetByIdBinaryAsync(serverId, videoId);

                if (movie is null)
                {
                    return Results.NotFound("Movie not found");
                }

                return Results.Ok(movie);

            }).Produces<MovieDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("DownloadTheBinaryContentOfVideo​")
                .WithTags("Movies")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve binary content from a video."));

            app.MapPost("/api/servers/{serverId}/videos", async (string serverId, MovieDTO movie, IMovieService service) =>
            {
                if (!Guid.TryParse(serverId, out Guid serverGuidId)) return Results.BadRequest("GUID server entered is not valid.");

                if (movie is null)
                    return Results.BadRequest("Movie not informed.");

                movie.ServerId = serverGuidId;

                if (!MiniValidator.TryValidate(movie, out var errors))
                    return Results.ValidationProblem(errors);

                var result = await service.CreateAsync(movie);

                return result is not null ? Results.Created($"/api/servers/{result.ServerId.ToString("B")}/videos/{result.Id.ToString("B")}", result)
                        : Results.BadRequest("There was a problem saving the record.");

            }).ProducesValidationProblem()
                .Produces<MovieDTO>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("AddNewVideoToSserver​")
                .WithTags("Movies")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Add video to a server​."));
            
            app.MapDelete("/api/servers/{serverId}/videos/{videoId}", async (string serverId, string videoId, IMovieService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID server entered is not valid.");
                if (GuidTest.IsGUID(videoId) is false) return Results.BadRequest("GUID movie entered is not valid.");

                var result = await service.RemoveAsync(serverId, videoId);
                if (result is null) return Results.NotFound();

                return result is not null ? Results.NoContent()
                    : Results.BadRequest("There was a problem removing the record.");

            }).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("RemoveExistingMovie​")
                .WithTags("Movies")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Remove a video​ from the server​."));



        }
    }
}
