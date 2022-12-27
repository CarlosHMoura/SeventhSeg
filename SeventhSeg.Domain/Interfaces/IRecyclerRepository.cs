using SeventhSeg.Domain.Entities;

namespace SeventhSeg.Domain.Interfaces;

public interface IRecyclerRepository
{
    Task<IEnumerable<Recycler>> GetRecyclerAsync();
    Task<Recycler> GetByIdAsync(Guid id);
    Task<Recycler> CreateAsync(Recycler movie);
    Task<Recycler> UpdateAsync(Recycler movie);
    Task<Recycler> RemoveAsync(Recycler movie);
    Task<Recycler> GetRecyclerRunningAsync();
    Task<Recycler> GetRecyclerStatusAsync();
}
