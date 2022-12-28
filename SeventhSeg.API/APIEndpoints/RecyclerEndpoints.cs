using Hangfire;
using MiniValidation;
using Newtonsoft.Json;
using SeventhSeg.Application.Common;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Enums;

namespace SeventhSeg.API.APIEndpoints
{
    public static class RecyclerEndpoints
    {
        public static void MapRecyclerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/recycler/status", async (IRecyclerService service) =>
            {
                var recycler = await service.GetRecyclerStatusAsync();
                if (recycler is null)
                {
                    return Results.NotFound("Recycler not found");
                }

                var result = new
                {
                    Status = recycler.Status == RecyclerStatusEnum.Running ? "running" : "not running"
                };

                return Results.Ok(JsonConvert.SerializeObject(result));

            }).Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("StatusRecycleOldVideos")
                .WithTags("Recycler");

            app.MapPost("/api/recycler/process/{days}", async (int days, IRecyclerService service) =>
            {
                if (days <= 0)
                    return Results.BadRequest("Days not informed.");


                var result = await service.CreateAsync(days);

                if(result is null)
                {
                    return Results.BadRequest("Already have a recycling running.");
                }

                BackgroundJob.Enqueue(() => service.ExecuteRecyclerTask(result));

                return Results.Accepted($"/api/recycler/status", result);

            }).ProducesValidationProblem()
                .Produces<RecyclerDTO>(StatusCodes.Status202Accepted)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("RunRecyclingOldMovies")
                .WithTags("Recycler");
        }
    }
}
