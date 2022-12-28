using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using SeventhSeg.Application.DTOs;
using System;
using SeventhSeg.API.Tests.Helpers;

namespace SeventhSeg.API.Tests.APIEndpoints;

public class MovieUnitTest1
{
    [Test]
    public async Task CreateMovie_WithValidParameters_ResultNewMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var result = await MovieHelpFunctions.CreateNewMovie(application);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.IsTrue(result.Result?.Description == "Video Camera 1");
        Assert.IsTrue(result.Result?.SizeInBytes == 1024);
    }

    [Test]
    public async Task CreateMovie_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var result = await MovieHelpFunctions.CreateNewMovie(application, true);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateMovie_WithNullParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var result = await MovieHelpFunctions.CreateNewMovie(application, false, true);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovie_WithValidParameters_ResultListMovies()
    {
        await using var application = new SeventhSegAPIApplication();

        var result = await MovieHelpFunctions.GetListMovies(application);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(result.ResultList);
    }

    [Test]
    public async Task GetMovie_WithValidParameters_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);

        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlMovie = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos";

        var resultGetMovie = await client.GetAsync(urlMovie);

        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetMovieById_WithValidParameters_ResultMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var result = await MovieHelpFunctions.GetMovieById(application);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(result.Result);
    }

    [Test]
    public async Task GetMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"/api/servers/{Guid.NewGuid()}/videos/CD2C1638-1638-72D5-1638";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"/api/servers/CD2C1638-1638-72D5-/videos/{Guid.NewGuid()}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var serverResult = await ServerHelpFunctions.GetListServer(application);
        
        var client = application.CreateClient();

        var urlMovieById = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/{Guid.NewGuid()}";
        var resultMovieById = await client.GetAsync(urlMovieById);

        Assert.That(resultMovieById.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetBinaryMovieById_WithValidParameters_ResultBinaryMovie()
    {
        await using var application = new SeventhSegAPIApplication();

        var movieResult = await MovieHelpFunctions.GetListMovies(application);
        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlMovieById = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/{movieResult?.ResultList?.First().Id}/binary";

        var resultGetMovie = await client.GetAsync(urlMovieById);
        var movieGetResult = await client.GetFromJsonAsync<MovieDTO>(urlMovieById);

        Assert.That(resultGetMovie.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(serverResult);
        Assert.IsNotNull(movieResult);
        Assert.IsNotNull(movieGetResult);
        Assert.IsTrue(movieGetResult.Binary is not null);
    }

    [Test]
    public async Task GetBinaryMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var url = $"/api/servers/{Guid.NewGuid()}/videos/CD2C1638-1638-72D5-1638/binary";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetBinaryMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var url = $"/api/servers/CD2C1638-1638-72D5-/videos/{Guid.NewGuid()}/binary";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetBinaryMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlMovieById = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/CD2C1638-1638-72D5-1638-DEADBEEF1638";
        var resultMovieById = await client.GetAsync(urlMovieById);

        Assert.That(resultMovieById.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.IsNotNull(serverResult);
    }

    [Test]
    public async Task DeleteMovieById_WithValidParameters_ResultNoContent()
    {
        await using var application = new SeventhSegAPIApplication();

        var movieResult = await MovieHelpFunctions.GetListMovies(application);
        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlDeleteMovie = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/{movieResult?.ResultList?.First().Id}";
        var resultDelete = await client.DeleteAsync(urlDeleteMovie);

        Assert.That(resultDelete.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        Assert.IsNotNull(movieResult);
        Assert.IsNotNull(serverResult);
    }

    [Test]
    public async Task DeleteMovieById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlDeleteMovie = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/{Guid.NewGuid()}";
        var resultDelete = await client.DeleteAsync(urlDeleteMovie);

        Assert.That(resultDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteMovieById_WithInvalidServerParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var url = $"/api/servers/CD2C1638-/videos/{Guid.NewGuid()}";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task DeleteMovieById_WithInvalidMovieParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var url = $"/api/servers/{Guid.NewGuid()}/videos/CD2C1638-1DEADBEEF1638";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }



}