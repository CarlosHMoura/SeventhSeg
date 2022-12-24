using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<MovieDTO>> GetMoviesAsync();
    Task<IEnumerable<MovieDTO>> GetMoviesByServerIdAsync(Guid serverId);
    Task<MovieDTO> GetByIdAsync(Guid id);
    Task<MovieDTO> CreateAsync(MovieDTO movie);
    Task<MovieDTO> UpdateAsync(MovieDTO movie);
    Task<MovieDTO> RemoveAsync(MovieDTO movie);
}
