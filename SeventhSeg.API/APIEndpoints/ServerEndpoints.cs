using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using SeventhSeg.Application.Common;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace SeventhSeg.API.APIEndpoints
{
    public static class ServerEndpoints
    {
        public static void MapServerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/servers", async (IServerService service) =>
            {
                var servers = await service.GetServersAsync();
                if (servers is null || servers.Count() is 0)
                {
                    return Results.NotFound("Servers not found");
                }

                return Results.Ok(servers);

            }).Produces<IEnumerable<ServerDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("ListAllServers")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "List all servers."));

            app.MapGet("/api/servers/{serverId}", async (string serverId, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID entered is not valid.");
               
                var server = await service.GetByIdAsync(serverId);
                if (server is null)
                {
                    return Results.NotFound("Server not found");
                }

                return Results.Ok(server);

            }).Produces<ServerDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("RecoverExistingServer​")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Recover a server."));

            app.MapGet("/api/servers/available/{serverId}", async (string serverId, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID entered is not valid.");

                var server = await service.GetByIdAsync(serverId);

                if (server is null) return Results.NotFound("Server not found");

                var status = service.CheckServerAvailability(server);

                var result = new
                {
                    Status = status is true ? "server available" : "server not available"
                };

                return Results.Ok(result);

            }).Produces<ServerDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("CheckServerAvailability​")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Check server availability."));


            app.MapPost("/api/server", async (ServerDTO server, IServerService service) =>
            {

                if (server is null)
                    return Results.BadRequest("Server not informed.");

                if (!MiniValidator.TryValidate(server, out var errors))
                    return Results.ValidationProblem(errors);

                var result = await service.CreateAsync(server);

                return result is not null ? Results.Created($"/api/servers/{result.Id.ToString("B")}", result)
                        : Results.BadRequest("There was a problem saving the record.");

            }).ProducesValidationProblem()
                .Produces<ServerDTO>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("CreateNewServer")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Create a new server."));

            app.MapPut("/api/servers/{serverId}", async (string serverId, ServerDTO server, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID entered is not valid.");

                if (server is null)
                    return Results.BadRequest("Server not informed.");

                if (!MiniValidator.TryValidate(server, out var errors))
                    return Results.ValidationProblem(errors);


                var result = await service.UpdateAsync(serverId, server);

                return result is not null ? Results.NoContent()
                    : Results.NotFound();

            }).ProducesValidationProblem()
                .Produces<ServerDTO>(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("UpdateServer")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Update a server."));

            app.MapDelete("/api/servers/{serverId}", async (string serverId, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) is false) return Results.BadRequest("GUID entered is not valid.");

                var result = await service.RemoveAsync(serverId);

                return result is not null ? Results.NoContent()
                    : Results.NotFound();

            }).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("RemoveExistingServer​")
                .WithTags("Servers")
                .WithMetadata(new SwaggerOperationAttribute(summary: "Remove a server."));

        }
    }
}
