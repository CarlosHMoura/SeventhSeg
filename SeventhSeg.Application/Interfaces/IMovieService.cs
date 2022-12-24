using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDTO>> GetMoviesAsync();
    Task<IEnumerable<MovieDTO>> GetMoviesByServerIdAsync(string serverId);
    Task<MovieDTO> GetByIdAsync(string id);
    Task<MovieDTO> CreateAsync(MovieDTO movie);
    Task<MovieDTO> UpdateAsync(MovieDTO movie);
    Task<MovieDTO> RemoveAsync(string id);
}
