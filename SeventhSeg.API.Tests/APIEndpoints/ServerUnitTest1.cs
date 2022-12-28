using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using SeventhSeg.Application.DTOs;
using SeventhSeg.API.Tests.Helpers;

namespace SeventhSeg.API.Tests.APIEndpoints;

public class ServerUnitTest1
{
    [Test]
    public async Task GetServer_WithValidParameters_ResultListServers()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);

        var result = await ServerHelpFunctions.GetListServer(application);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(result.ResultList);
        Assert.IsTrue(result.ResultList.Count() == 1);
    }

    [Test]
    public async Task GetServer_WithValidParameters_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);

        var client = application.CreateClient();

        var result = await client.GetAsync(ServerHelpFunctions.urlGetServers);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetServerById_WithValidParameters_ResultServer()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);

        var result = await ServerHelpFunctions.GetServerById(application, null);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(result.ResultList);
        Assert.IsNotNull(result.Result);
        Assert.IsTrue(result.Result.Port == 80);
    }

    [Test]
    public async Task GetServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"{ServerHelpFunctions.urlGetServers}/sdafsdf-sasdf";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetServerById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"{ServerHelpFunctions.urlGetServers}/{Guid.NewGuid()}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetAvailableServerById_WithValidParameters_ResultStatusServer()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);

        var client = application.CreateClient();
        var servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(ServerHelpFunctions.urlGetServers);

        var urlId = $"{ServerHelpFunctions.urlGetServers}/{servers?.First().Id}";

        var result = await client.GetAsync(urlId);

        var server = await client.GetFromJsonAsync<object>(urlId);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(servers);
        Assert.IsNotNull(server);
    }

    [Test]
    public async Task GetAvailableServerById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"{ServerHelpFunctions.urlGetAvailableServer}/{Guid.NewGuid()}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetAvailableServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var url = $"{ServerHelpFunctions.urlGetAvailableServer}/1651651654-sdasf";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateServer_WithValidParameters_ResultNewServer()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);

        var result = await ServerHelpFunctions.CreateNewServer(application);

        Assert.That(result.Status?.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.IsTrue(result.Result?.Name == "Interno");
        Assert.IsTrue(result.Result?.Ip == "192.168.1.1");
        Assert.IsTrue(result.Result?.Port == 80);
    }

    [Test]
    public async Task CreateServer_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(ServerHelpFunctions.urlCreateServer, server);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateServer_WithNullParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(ServerHelpFunctions.urlCreateServer, new { });

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task DeleteServer_WithValidParameters_ResultNoContent()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);

        var serverResult = await ServerHelpFunctions.CreateNewServer(application);

        var urlId = $"{ServerHelpFunctions.urlGetServers}/{serverResult.Result?.Id}";

        var client = application.CreateClient();
        var response = await client.DeleteAsync(urlId);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task DeleteServerById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"{ServerHelpFunctions.urlGetServers}/{Guid.NewGuid()}";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = $"{ServerHelpFunctions.urlGetServers}/16516165-sdsd";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }



}