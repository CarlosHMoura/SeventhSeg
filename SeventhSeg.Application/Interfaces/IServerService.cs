using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IServerService
{
    Task<IEnumerable<ServerDTO>> GetServersAsync();
    Task<ServerDTO> GetByIdAsync(Guid id);
    Task<ServerDTO> CreateAsync(ServerDTO server);
    Task<ServerDTO> UpdateAsync(ServerDTO server);
    Task<ServerDTO> RemoveAsync(ServerDTO server);
}
