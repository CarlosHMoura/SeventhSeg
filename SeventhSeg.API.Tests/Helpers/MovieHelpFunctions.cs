using SeventhSeg.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SeventhSeg.API.Tests.Helpers;

public static class MovieHelpFunctions
{
    public static string urlGetMovies = "/api/servers";
    public static string urlCreateServer = "/api/server";
    public static string urlGetAvailableServer = "/api/servers/available";

    public static async Task<ResponseDTO<MovieDTO>> CreateNewMovie(SeventhSegAPIApplication application, bool invalid = false, bool nullable = false)
    {
        await ServerMockData.CreateServers(application, true);

        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var movie = new MovieDTO { Description = "Video Camera 1", SizeInBytes = 1024, Binary = "iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAbSURBVChTY/iPBdx79AzKIh4wMVAJDFuDGBgAn3lHausb+Q8AAAAASUVORK5CYII=" };

        var client = application.CreateClient();

        var urlMovie = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos";

        if (invalid) movie.Description = null;

        if (nullable) movie = null;

        var status = await client.PostAsJsonAsync(urlMovie, movie);

        MovieDTO? movieResult = null;

        if(!nullable)
            movieResult = await status.Content.ReadFromJsonAsync<MovieDTO>();

        return new ResponseDTO<MovieDTO> { Result = movieResult, Status = status };
    }

    public static async Task<ResponseDTO<MovieDTO>> GetListMovies(SeventhSegAPIApplication application)
    {
        await ServerMockData.CreateServers(application, true);

        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var movieResult = await CreateNewMovie(application);

        var client = application.CreateClient();

        var urlMovie = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos";

        var status = await client.GetAsync(urlMovie);
        var resultGetMovie = await client.GetFromJsonAsync<IEnumerable<MovieDTO>>(urlMovie);


        return new ResponseDTO<MovieDTO> { ResultList = resultGetMovie, Status = status };
    }

    public static async Task<ResponseDTO<MovieDTO>> GetMovieById(SeventhSegAPIApplication application)
    {
        var moviesResult = await GetListMovies(application);

        var serverResult = await ServerHelpFunctions.GetListServer(application);

        var client = application.CreateClient();

        var urlMovieById = $"/api/servers/{serverResult?.ResultList?.First().Id}/videos/{moviesResult?.ResultList?.First().Id}";

        var status = await client.GetAsync(urlMovieById);
        var movieGetResult = await client.GetFromJsonAsync<MovieDTO>(urlMovieById);

        return new ResponseDTO<MovieDTO> { Result = movieGetResult, Status = status };
    }
}
