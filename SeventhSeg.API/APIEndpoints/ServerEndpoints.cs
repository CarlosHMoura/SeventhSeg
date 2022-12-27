using MiniValidation;
using SeventhSeg.Application.Common;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;

namespace SeventhSeg.API.APIEndpoints
{
    public static class ServerEndpoints
    {
        public static void MapServerEndpoints(this WebApplication app)
        {
            app.MapGet("/api/servers", async (IServerService service) =>
            {
                var servers = await service.GetServersAsync();
                if (servers is null)
                {
                    return Results.NotFound("Servers not found");
                }

                return Results.Ok(servers);

            }).Produces<IEnumerable<ServerDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("ListAllServers")
                .WithTags("Servers");

            app.MapGet("/api/servers/{serverId}​", async (string serverId, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) == false) return Results.BadRequest("GUID entered is not valid.");

                var server = await service.GetByIdAsync(serverId);
                if (server is null)
                {
                    return Results.NotFound("Server not found");
                }

                return Results.Ok(server);

            }).Produces<ServerDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("RecoverExistingServer​")
                .WithTags("Servers");

            app.MapPost("/api/server​", async (ServerDTO server, IServerService service) =>
            {

                if (server == null)
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
                .WithTags("Servers");

            app.MapDelete("/api/servers/{serverId}", async (string serverId, IServerService service) =>
            {
                if (GuidTest.IsGUID(serverId) == false) return Results.BadRequest("GUID entered is not valid.");

                var result = await service.RemoveAsync(serverId);
                if (result == null) return Results.NotFound();

                return result is not null ? Results.NoContent()
                    : Results.BadRequest("There was a problem removing the record.");

            }).Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("RemoveExistingServer​")
                .WithTags("Servers");

        }
    }
}
