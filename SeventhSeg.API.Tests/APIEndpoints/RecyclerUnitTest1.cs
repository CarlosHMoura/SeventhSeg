using NUnit.Framework;
using System.Net.Http.Json;
using System.Net;
using SeventhSeg.Application.DTOs;
using System;

namespace SeventhSeg.API.Tests.APIEndpoints;

public class RecyclerUnitTest1
{
    [Test]
    public async Task GetRecyclerStatus_WithValidParameters_ResultStatus()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var urlRecycler = "/api/recycler/process/1";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlRecycler, new { });

        var urlRecyclerStatus = "/api/recycler/status";

        var resultGetStatus = await client.GetAsync(urlRecyclerStatus);
        var recyclerGetResult = await client.GetFromJsonAsync<object>(urlRecyclerStatus);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        Assert.That(resultGetStatus.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetRecyclerStatus_WithInvalidParameters_ResultNotFound()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);

        var client = application.CreateClient();

        var urlRecyclerStatus = "/api/recycler/status";

        var resultGetStatus = await client.GetAsync(urlRecyclerStatus);

        Assert.That(resultGetStatus.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CreateRecycler_WithValidParameters_ResultAccept()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var urlRecycler = "/api/recycler/process/1";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlRecycler, new { });

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
    }

    [Test]
    public async Task CreateRecycler_WithInvalidParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var urlRecycler = "/api/recycler/process/0";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlRecycler, new { });

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task CreateRecycler_WithIsRunningParameters_ResultBadRequest()
    {
        await using var application = new SeventhSegAPIApplication();

        await ServerMockData.CreateServers(application, false);
        var urlRecycler = "/api/recycler/process/1";

        var client = application.CreateClient();
        var result = await client.PostAsJsonAsync(urlRecycler, new { });

        var otherResult = await client.PostAsJsonAsync(urlRecycler, new { });

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
        Assert.That(otherResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

}