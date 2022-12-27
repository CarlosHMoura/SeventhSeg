using Microsoft.EntityFrameworkCore;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;
using SeventhSeg.Infra.Data.Context;

namespace SeventhSeg.Infra.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private ApplicationDbContext _movieContext;
    public MovieRepository(ApplicationDbContext movieContext)
    {
        _movieContext = movieContext;
    }
    public async Task<Movie> CreateAsync(Movie movie)
    {
        movie.CreatedDate = DateTime.Now;
        _movieContext.Add(movie);
        await _movieContext.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> GetByIdAsync(Guid id)
    {
        return await _movieContext.Movies.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
    }
    public async Task<Movie> GetByIdAsync(Guid serverId, Guid movieId)
    {
        return await _movieContext.Movies.AsNoTracking()
            .SingleOrDefaultAsync(s => s.ServerId == serverId && s.Id == movieId);
    }

    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        return await _movieContext.Movies.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetMoviesByServerIdAsync(Guid serverId)
    {
        return await _movieContext.Movies.Where(x => x.ServerId == serverId).AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetOldMoviesByDaysAsync(int days)
    {
        return await _movieContext.Movies.Where(x => x.CreatedDate >= DateTime.Now.AddDays(-days)).AsNoTracking().ToListAsync();
    }

    public async Task<Movie> RemoveAsync(Movie movie)
    {
        _movieContext.Remove(movie);
        await _movieContext.SaveChangesAsync();
        return movie;
    }


    public async Task<Movie> UpdateAsync(Movie movie)
    {
        movie.UpdatedDate = DateTime.Now;
        _movieContext.Update(movie);
        await _movieContext.SaveChangesAsync();
        _movieContext.Entry(movie).State = EntityState.Detached;
        return movie;
    }
}
