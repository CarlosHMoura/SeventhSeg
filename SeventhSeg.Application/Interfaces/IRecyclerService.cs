using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IRecyclerService
{
    Task<IEnumerable<RecyclerDTO>> GetRecyclerAsync();
    Task<RecyclerDTO> GetByIdAsync(string id);
    Task<RecyclerDTO> CreateAsync(int days);
    Task<RecyclerDTO> GetRecyclerStatusAsync();
    Task<RecyclerDTO> UpdateAsync(RecyclerDTO server);
    Task<RecyclerDTO> RemoveAsync(string id);
    Task ExecuteRecyclerTask(RecyclerDTO recycler);
}
