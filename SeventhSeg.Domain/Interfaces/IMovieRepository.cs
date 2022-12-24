using SeventhSeg.Domain.Entities;

namespace SeventhSeg.Domain.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<IEnumerable<Movie>> GetMoviesByServerIdAsync(string serverId);
    Task<Movie> GetByIdAsync(string id);
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<Movie> RemoveAsync(Movie movie);T
}
