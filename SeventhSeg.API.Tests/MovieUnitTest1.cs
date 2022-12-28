using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using SeventhSeg.Application.DTOs;
using System;

namespace SeventhSeg.API.Tests;

public class MovieUnitTest1
{
    [Test]
    public async Task CreateMovie_WithValidParameters_ResultNewMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";
        var resultMovie = await client.PostAsJsonAsync(urlMovie, movie);

        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.IsTrue(movieResult.Description == "Video Camera 1");
        Assert.IsTrue(movieResult.SizeInBytes == 1024);
    }

    [Test]
    public async Task CreateMovie_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";
        var resultMovie = await client.PostAsJsonAsync(urlMovie, movie);

        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateMovie_WithNullParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
       
        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";
        var resultMovie = await client.PostAsJsonAsync(urlMovie, new { });

        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovie_WithValidParameters_ResultListMovies()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";

        var resultMovie = await client.PostAsJsonAsync(urlMovie, movie);
        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        var resultGetMovie = await client.GetAsync(urlMovie);
        var movieGetResult = await client.GetFromJsonAsync<IEnumerable<MovieDTO>>(urlMovie);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(serverResult);
        Assert.IsNotNull(movieResult);
        Assert.IsNotNull(movieGetResult);
    }

    [Test]
    public async Task GetMovie_WithValidParameters_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";

        var resultGetMovie = await client.GetAsync(urlMovie);

        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetMovieById_WithValidParameters_ResultMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovies = $"/api/servers/{serverResult?.Id}/videos";

        var resultMovie = await client.PostAsJsonAsync(urlMovies, movie);
        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        var urlMovieById = $"/api/servers/{serverResult?.Id}/videos/{movieResult?.Id}";

        var resultGetMovie = await client.GetAsync(urlMovieById);
        var movieGetResult = await client.GetFromJsonAsync<MovieDTO>(urlMovieById);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(serverResult);
        Assert.IsNotNull(movieResult);
        Assert.IsNotNull(movieGetResult);
    }

    [Test]
    public async Task GetMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-1638-DEADBEEF1638}/videos/{CD2C1638-1638-72D5-1638}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-}/videos/{CD2C1638-1638-72D5-1638-DEADBEEF1638}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovieById = $"/api/servers/{serverResult?.Id}/videos/CD2C1638-1638-72D5-1638-DEADBEEF1638";
        var resultMovieById = await client.GetAsync(urlMovieById);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovieById.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.IsNotNull(serverResult);
    }

    [Test]
    public async Task GetBinaryMovieById_WithValidParameters_ResultBinaryMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovies = $"/api/servers/{serverResult?.Id}/videos";

        var resultMovie = await client.PostAsJsonAsync(urlMovies, movie);
        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        var urlMovieById = $"/api/servers/{serverResult?.Id}/videos/{movieResult?.Id}/binary";

        var resultGetMovie = await client.GetAsync(urlMovieById);
        var movieGetResult = await client.GetFromJsonAsync<MovieDTO>(urlMovieById);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(serverResult);
        Assert.IsNotNull(movieResult);
        Assert.IsNotNull(movieGetResult);
        Assert.IsTrue(movieGetResult.Binary == movie.Binary);
    }

    [Test]
    public async Task GetBinaryMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-1638-DEADBEEF1638}/videos/{CD2C1638-1638-72D5-1638}/binary";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetBinaryMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-}/videos/{CD2C1638-1638-72D5-1638-DEADBEEF1638}/binary";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetBinaryMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        
        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovieById = $"/api/servers/{serverResult?.Id}/videos/CD2C1638-1638-72D5-1638-DEADBEEF1638";
        var resultMovieById = await client.GetAsync(urlMovieById);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovieById.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.IsNotNull(serverResult);
    }

    [Test]
    public async Task DeleteMovieById_WithValidParameters_ResultNoContent()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlMovie = $"/api/servers/{serverResult?.Id}/videos";
        var resultMovie = await client.PostAsJsonAsync(urlMovie, movie);

        var movieResult = await resultMovie.Content.ReadFromJsonAsync<MovieDTO>();

        var urlDeleteMovie = $"/api/servers/{serverResult?.Id}/videos/{movieResult?.Id}";
        var resultDelete = await client.DeleteAsync(urlDeleteMovie);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultMovie.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultDelete.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task DeleteMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };
        
        await ServerMockData.CreateServers(application, false);
        var urlServer = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlServer, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();
                
        var urlDeleteMovie = $"/api/servers/{serverResult?.Id}/videos/CD2C1638-1638-72D5-1638-DEADBEEF1638";
        var resultDelete = await client.DeleteAsync(urlDeleteMovie);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(resultDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-}/videos/CD2C1638-1638-72D5-1638-DEADBEEF1638";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task DeleteMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/CD2C1638-1638-72D5-1638-DEADBEEF1638/videos/CD2C1638-1DEADBEEF1638";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }



}