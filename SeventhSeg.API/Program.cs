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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard();
app.UseHangfireServer();

app.UseHttpsRedirection();

app.MapServerEndpoints();
app.MapMovieEndpoints();

app.Run();
