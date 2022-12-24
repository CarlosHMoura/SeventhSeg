using SeventhSeg.Domain.Entities;
using System.Threading.Tasks;

namespace SeventhSeg.Domain.Interfaces;

public interface IServerRepository
{
    Task<IEnumerable<Server>> GetServersAsync();
    Task<Server> GetByIdAsync(string id);
    Task<Server> CreateAsync(Server server);
    Task<Server> UpdateAsync(Server server);
    Task<Server> RemoveAsync(Server server);
}
