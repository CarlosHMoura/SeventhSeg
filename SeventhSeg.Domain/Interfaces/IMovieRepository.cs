using SeventhSeg.Domain.Entities;

namespace SeventhSeg.Domain.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetMoviesAsync();
    Task<Movie> GetByIdAsync(Guid serverId, Guid movieId);
    Task<IEnumerable<Movie>> GetMoviesByServerIdAsync(Guid serverId);
    Task<IEnumerable<Movie>> GetOldMoviesByDaysAsync(int days);
    Task<Movie> GetByIdAsync(Guid id);
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<Movie> RemoveAsync(Movie movie);
}
