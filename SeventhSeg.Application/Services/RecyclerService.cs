using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Enums;
using SeventhSeg.Domain.Interfaces;

namespace SeventhSeg.Application.Services;

public class RecyclerService : IRecyclerService
{
    private IRecyclerRepository _recyclerRepository;
    private readonly IMapper _mapper;
    private IMovieService _movieService;

    public RecyclerService(IRecyclerRepository recyclerRepository, IMapper mapper, IMovieService movieService)
    {
        _recyclerRepository = recyclerRepository;
        _mapper = mapper;
        _movieService = movieService;
    }

    public async Task<RecyclerDTO> CreateAsync(int days)
    {
        var isRunning = await _recyclerRepository.GetRecyclerRunningAsync();

        if (isRunning is not null) return null!;

        RecyclerDTO recycler = new RecyclerDTO()
        {
            Days = days,
            Status = RecyclerStatusEnum.Not_Running
        };

        var recyclerEntity = _mapper.Map<Recycler>(recycler);
        await _recyclerRepository.CreateAsync(recyclerEntity);
        recycler.Id = recyclerEntity.Id;
        return recycler;
    }

    public async Task<RecyclerDTO> GetRecyclerStatusAsync()
    {
        var recyclerEntity = await _recyclerRepository.GetRecyclerStatusAsync();

        if (recyclerEntity is null) return null!;

        return _mapper.Map<RecyclerDTO>(recyclerEntity);
    }

    public async Task<RecyclerDTO> GetByIdAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var recyclerEntity = await _recyclerRepository.GetByIdAsync(guidId);
        return _mapper.Map<RecyclerDTO>(recyclerEntity);
    }

    public async Task<IEnumerable<RecyclerDTO>> GetRecyclerAsync()
    {
        var recyclerEntity = await _recyclerRepository.GetRecyclerAsync();
        return _mapper.Map<IEnumerable<RecyclerDTO>>(recyclerEntity);
    }

    public async Task<RecyclerDTO> RemoveAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var recyclerEntity = await _recyclerRepository.GetByIdAsync(guidId);

        if (recyclerEntity is null) return null!;

        await _recyclerRepository.RemoveAsync(recyclerEntity);

        return _mapper.Map<RecyclerDTO>(recyclerEntity);
    }

    public async Task<RecyclerDTO> UpdateAsync(RecyclerDTO recycler)
    {
        var recyclerEntity = _mapper.Map<Recycler>(recycler);
        await _recyclerRepository.UpdateAsync(recyclerEntity);
        return recycler;
    }

    public async Task ExecuteRecyclerTask(RecyclerDTO recycler)
    {
        var movies = await _movieService.GetOldMoviesByDaysAsync(recycler.Days);

        if (movies is null) 
        {
            recycler.Status = RecyclerStatusEnum.Finished;

            await UpdateAsync(recycler);
            return;
        }

        recycler.Status = RecyclerStatusEnum.Running;

        await UpdateAsync(recycler);

        foreach (var movie in movies)
        {
            await _movieService.RemoveAsync(movie.Id.ToString());
        }

        recycler.Status = RecyclerStatusEnum.Finished;

        await UpdateAsync(recycler);
    }

}
