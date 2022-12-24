using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;

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
        var movieEntity = _mapper.Map<Movie>(movie);
        await _movieRepository.CreateAsync(movieEntity);
        movie.Id = movieEntity.Id;
        return movie;
    }

    public async Task<MovieDTO> GetByIdAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var movieEntity = await _movieRepository.GetByIdAsync(guidId);
        return _mapper.Map<MovieDTO>(movieEntity);
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesAsync()
    {
        var moviesEntity = await _movieRepository.GetMoviesAsync();
        return _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesByServerIdAsync(string serverId)
    {
        Guid guidId = Guid.Parse(serverId);

        var moviesEntity = await _movieRepository.GetMoviesByServerIdAsync(guidId);
        return _mapper.Map<IEnumerable<MovieDTO>>(moviesEntity);
    }

    public async Task<MovieDTO> RemoveAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var movieEntity = _movieRepository.GetByIdAsync(guidId).Result;
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
