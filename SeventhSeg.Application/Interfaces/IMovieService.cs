using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDTO>> GetMoviesAsync();
    Task<IEnumerable<MovieDTO>> GetMoviesByServerIdAsync(string serverId);
    Task<IEnumerable<MovieDTO>> GetOldMoviesByDaysAsync(int days);
    Task<MovieDTO> GetByIdAsync(string id);
    Task<MovieDTO> GetByIdAsync(string serverId, string movieId);
    Task<MovieDTO> GetByIdBinaryAsync(string serverId, string movieId);
    Task<MovieDTO> CreateAsync(MovieDTO movie);
    Task<MovieDTO> UpdateAsync(MovieDTO movie);
    Task<MovieDTO> RemoveAsync(string id);
    Task<MovieDTO> RemoveAsync(string serverId, string movieId);
}
