using SeventhSeg.Application.DTOs;

namespace SeventhSeg.Application.Interfaces;

public interface IServerService
{
    Task<IEnumerable<ServerDTO>> GetServersAsync();
    Task<ServerDTO> GetByIdAsync(string id);
    Task<ServerDTO> CreateAsync(ServerDTO server);
    Task<ServerDTO> UpdateAsync(string id, ServerDTO server);
    Task<ServerDTO> RemoveAsync(string id);
    bool CheckServerAvailability(ServerDTO server);
}
