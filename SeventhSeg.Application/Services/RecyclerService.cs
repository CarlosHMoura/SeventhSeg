using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;

namespace SeventhSeg.Application.Services;

public class RecyclerService : IRecyclerService
{
    private IRecyclerRepository _recyclerRepository;
    private readonly IMapper _mapper;

    public RecyclerService(IRecyclerRepository recyclerRepository, IMapper mapper)
    {
        _recyclerRepository = recyclerRepository;
        _mapper = mapper;
    }

    public async Task<RecyclerDTO> CreateAsync(RecyclerDTO recycler)
    {
        var recyclerEntity = _mapper.Map<Recycler>(recycler);
        await _recyclerRepository.CreateAsync(recyclerEntity);
        recycler.Id = recyclerEntity.Id;
        return recycler;
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

        if (recyclerEntity == null) return null;

        await _recyclerRepository.RemoveAsync(recyclerEntity);

        return _mapper.Map<RecyclerDTO>(recyclerEntity);
    }

    public async Task<RecyclerDTO> UpdateAsync(RecyclerDTO recycler)
    {
        var recyclerEntity = _mapper.Map<Recycler>(recycler);
        await _recyclerRepository.UpdateAsync(recyclerEntity);
        return recycler;
    }

}
