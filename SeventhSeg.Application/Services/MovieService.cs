using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;
using System.IO;

namespace SeventhSeg.Application.Services;

public class MovieService : IMovieService
{
    private IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieDTO> CreateAsync(MovieDTO movie)
    {
        byte[] movieBytes = Convert.FromBase64String(movie.Binary);

        var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "MoviesUploaded",
                   Guid.NewGuid().ToString() + ".mp4");

        await File.WriteAllBytesAsync(path, movieBytes);

        movie.PathFile = path;
        movie.FileName = Path.GetFileName(path);

        var movieEntity = _mapper.Map<Movie>(movie);
        await _movieRepository.CreateAsync(movieEntity);
        movie.Id = movieEntity.Id;
        return movie;
    }

    public async Task<MovieDTO> GetByIdAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var movieEntity = await _movieRepository.GetByIdAsync(guidId);

        if (movieEntity == null) return null;

        MovieDTO movieDTO = _mapper.Map<MovieDTO>(movieEntity);

        return movieDTO;
    }

    public async Task<MovieDTO> GetByIdAsync(string serverId, string movieId)
    {
        Guid guidServerId = Guid.Parse(serverId);
        Guid guidMovieId = Guid.Parse(movieId);

        var movieEntity = await _movieRepository.GetByIdAsync(guidServerId, guidMovieId);

        if (movieEntity == null) return null;

        MovieDTO movieDTO = _mapper.Map<MovieDTO>(movieEntity);

        return movieDTO;
    }

    public async Task<MovieDTO> GetByIdBinaryAsync(string serverId, string movieId)
    {
        Guid guidServerId = Guid.Parse(serverId);
        Guid guidMovieId = Guid.Parse(movieId);

        var movieEntity = await _movieRepository.GetByIdAsync(guidServerId, guidMovieId);

        if (movieEntity == null) return null;

        MovieDTO movieDTO = _mapper.Map<MovieDTO>(movieEntity);

        if (File.Exists(movieEntity.PathFile))
        {
            byte[] bytes = File.ReadAllBytes(movieDTO.PathFile);
            String file = Convert.ToBase64String(bytes);
            movieDTO.Binary = file;
        }

        return movieDTO;
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesAsync()
    {
        var moviesEntity = await _movieRepository.GetMoviesAsync();

        if (moviesEntity.Count() <= 0) return null;

        return _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesByServerIdAsync(string serverId)
    {
        Guid guidId = Guid.Parse(serverId);

        var moviesEntity = await _movieRepository.GetMoviesByServerIdAsync(guidId);

        if (moviesEntity.Count() <= 0) return null;

        return _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);
    }

    public async Task<IEnumerable<MovieDTO>> GetOldMoviesByDaysAsync(int days)
    {
        var moviesEntity = await _movieRepository.GetOldMoviesByDaysAsync(days);

        if (moviesEntity.Count() <= 0) return null;

        return _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);
    }

    public async Task<MovieDTO> RemoveAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var movieEntity = _movieRepository.GetByIdAsync(guidId).Result;

        if (movieEntity == null) return null;

        if (File.Exists(movieEntity.PathFile))
        {
            File.Delete(movieEntity.PathFile);
        } 
        else
        {
            return null;
        }

        await _movieRepository.RemoveAsync(movieEntity);

        return _mapper.Map<MovieDTO>(movieEntity);
    }

    public async Task<MovieDTO> RemoveAsync(string serverId, string movieId)
    {
        Guid guidServerId = Guid.Parse(serverId);
        Guid guidMovieId = Guid.Parse(movieId);

        var movieEntity = await _movieRepository.GetByIdAsync(guidServerId, guidMovieId);

        if (movieEntity == null) return null;

        if (File.Exists(movieEntity.PathFile))
        {
            File.Delete(movieEntity.PathFile);
        }
        else
        {
            return null;
        }

        await _movieRepository.RemoveAsync(movieEntity);

        return _mapper.Map<MovieDTO>(movieEntity);
    }

    public async Task<MovieDTO> UpdateAsync(MovieDTO movie)
    {
        var movieEntity = _mapper.Map<Movie>(movie);
        await _movieRepository.UpdateAsync(movieEntity);
        return movie;
    }
}
