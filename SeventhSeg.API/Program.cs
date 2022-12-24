using SeventhSeg.Application.Interfaces;
using SeventhSeg.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "API SeventhSeg";
});

app.MapGet("/api/servers", async (IServerService service) =>
{
    var servers = await service.GetServersAsync();
    if (servers == null)
    {
        return Results.NotFound("Servers not found");
    }
    return Results.Ok(servers);
});

app.Run();
