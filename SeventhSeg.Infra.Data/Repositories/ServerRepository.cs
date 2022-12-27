using Microsoft.EntityFrameworkCore;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;
using SeventhSeg.Infra.Data.Context;

namespace SeventhSeg.Infra.Data.Repositories;

public class ServerRepository : IServerRepository
{
    private ApplicationDbContext _serverContext;
    public ServerRepository(ApplicationDbContext serverContext)
    {
        _serverContext = serverContext;
    }

    public async Task<Server> CreateAsync(Server server)
    {
        server.CreatedDate = DateTime.Now;
        _serverContext.Add(server);
        await _serverContext.SaveChangesAsync();
        return server;
    }

    public async Task<Server> GetByIdAsync(Guid id)
    {
        return await _serverContext.Servers.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Server>> GetServersAsync()
    {
        return await _serverContext.Servers.AsNoTracking().ToListAsync();
    }

    public async Task<Server> RemoveAsync(Server server)
    {
        _serverContext.Remove(server);
        await _serverContext.SaveChangesAsync();
        return server;
    }

    public async Task<Server> UpdateAsync(Server server)
    {
        server.UpdatedDate = DateTime.Now;
        _serverContext.Update(server);
        await _serverContext.SaveChangesAsync();
        _serverContext.Entry(server).State = EntityState.Detached;
        return server;
    }
}
