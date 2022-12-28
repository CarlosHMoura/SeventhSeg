using Hangfire;
using Hangfire.MemoryStorage;
using SeventhSeg.API.APIEndpoints;
using SeventhSeg.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "MoviesUploaded")))
{
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "MoviesUploaded"));
}

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.MapServerEndpoints();
app.MapMovieEndpoints();
app.MapRecyclerEndpoints();

app.Run();

public partial class Program { }