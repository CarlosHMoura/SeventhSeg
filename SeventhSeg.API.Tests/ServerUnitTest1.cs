using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using SeventhSeg.Application.DTOs;

namespace SeventhSeg.API.Tests;

public class ServerUnitTest1
{
    [Test]
    public async Task GetServer_WithValidParameters_ResultListServers()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers";

        var client = application.CreateClient();

        var result = await client.GetAsync(url);
        var servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(url);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotNull(servers);
        Assert.IsTrue(servers.Count() == 1);
    }

    [Test]
    public async Task GetServer_WithValidParameters_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var url = "/api/servers";

        var client = application.CreateClient();

        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetServerById_WithValidParameters_ResultServer()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers";

        var client = application.CreateClient();

        var servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(url);

        var urlId = $"{url}/{servers.First().Id}";

        var result = await client.GetAsync(urlId);

        var server = await client.GetFromJsonAsync<ServerDTO>(urlId);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(servers);
        Assert.IsNotNull(server);
        Assert.IsTrue(server.Port == 80);
    }

    [Test]
    public async Task GetServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{Ddddd}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task GetServerById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-1638-DEADBEEF1638}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetAvailableServerById_WithValidParameters_ResultStatusServer()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers";

        var client = application.CreateClient();

        var servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(url);

        var urlId = $"/api/servers/available/{servers.First().Id}";

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
        var url = "/api/servers/available/{CD2C1638-1638-72D5-1638-DEADBEEF1638}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetAvailableServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var url = "/api/servers/available/{Ddddd}";

        var client = application.CreateClient();
        var result = await client.GetAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateServer_WithValidParameters_ResultNewServer()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);
        var url = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(url, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.IsTrue(serverResult.Name == "Interno");
        Assert.IsTrue(serverResult.Ip == "192.168.1.1");
        Assert.IsTrue(serverResult.Port == 80);
    }

    [Test]
    public async Task CreateServer_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);
        var url = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(url, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateServer_WithNullParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var url = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(url, new {});

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task DeleteServer_WithValidParameters_ResultNoContent()
    {
        await using var application = new SeventhSegAPIApplication();

        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);
        var url = "/api/server";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(url, server);

        var serverResult = await result.Content.ReadFromJsonAsync<ServerDTO>();

        var urlId = $"/api/servers/{serverResult?.Id}";

        var response = await client.DeleteAsync(urlId);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task DeleteServerById_WithoutRealId_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-1638-72D5-1638-DEADBEEF1638}";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteServerById_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, true);
        var url = "/api/servers/{CD2C1638-}";

        var client = application.CreateClient();
        var result = await client.DeleteAsync(url);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }



}