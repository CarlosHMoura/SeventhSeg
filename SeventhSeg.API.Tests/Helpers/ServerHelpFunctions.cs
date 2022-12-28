using SeventhSeg.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SeventhSeg.API.Tests.Helpers;

public static class ServerHelpFunctions
{
    public static string urlGetServers = "/api/servers";
    public static string urlCreateServer = "/api/server";
    public static string urlGetAvailableServer = "/api/servers/available";

    public static async Task<ResponseDTO<ServerDTO>> GetListServer(SeventhSegAPIApplication application)
    {
        var client = application.CreateClient();
        var status = await client.GetAsync(urlGetServers);
        var servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(urlGetServers);

        return new ResponseDTO<ServerDTO> { ResultList = servers, Status = status };
    }

    public static async Task<ResponseDTO<ServerDTO>> GetServerById(SeventhSegAPIApplication application, string? serverId)
    {
        var client = application.CreateClient();

        IEnumerable<ServerDTO>? servers = null;

        string? urlId = $"{urlGetServers}/{serverId ?? ""}";
        var status = await client.GetAsync(urlGetServers);
        if (serverId is null)
        {
            servers = await client.GetFromJsonAsync<IEnumerable<ServerDTO>>(urlGetServers);
            urlId = $"{urlGetServers}/{servers?.First().Id}";
        }

        status = await client.GetAsync(urlId);

        var server = await client.GetFromJsonAsync<ServerDTO>(urlId);

        return new ResponseDTO<ServerDTO> { ResultList = servers, Status = status, Result = server };
    }

    public static async Task<ResponseDTO<ServerDTO>> CreateNewServer(SeventhSegAPIApplication application)
    {
        var server = new ServerDTO { Name = "Interno", Ip = "192.168.1.1", Port = 80 };

        await ServerMockData.CreateServers(application, false);
   
        var client = application.CreateClient();

        var status = await client.PostAsJsonAsync(urlCreateServer, server);

        var serverResult = await status.Content.ReadFromJsonAsync<ServerDTO>();

        return new ResponseDTO<ServerDTO> { Result = serverResult, Status = status };
    }

}
