using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Application.Interfaces;
using SeventhSeg.Domain.Entities;
using SeventhSeg.Domain.Interfaces;
using System.Net.Sockets;

namespace SeventhSeg.Application.Services;

public class ServerService : IServerService
{
    private IServerRepository _serverRepository;
    private readonly IMapper _mapper;

    public ServerService(IServerRepository serverRepository, IMapper mapper)
    {
        _serverRepository = serverRepository;
        _mapper = mapper;
    }

    public async Task<ServerDTO> CreateAsync(ServerDTO server)
    {
        var serverEntity = _mapper.Map<Server>(server);
        await _serverRepository.CreateAsync(serverEntity);
        server.Id = serverEntity.Id;
        return server;
    }

    public async Task<ServerDTO> GetByIdAsync(string id)
    {
        Guid guidId = Guid.Parse(id);

        var serverEntity = await _serverRepository.GetByIdAsync(guidId);
        return _mapper.Map<ServerDTO>(serverEntity);
    }

    public async Task<IEnumerable<ServerDTO>> GetServersAsync()
    {
        var serversEntity = await _serverRepository.GetServersAsync();
        return _mapper.Map<IEnumerable<ServerDTO>>(serversEntity);
    }

    public async Task<ServerDTO> RemoveAsync(string id)
    {
        Guid guidId = Guid.Parse(id);
               
        var serverEntity = await _serverRepository.GetByIdAsync(guidId);

        if (serverEntity == null) return null;

        await _serverRepository.RemoveAsync(serverEntity);

        return _mapper.Map<ServerDTO>(serverEntity);
    }

    public async Task<ServerDTO> UpdateAsync(ServerDTO server)
    {
        var serverEntity = _mapper.Map<Server>(server);
        await _serverRepository.UpdateAsync(serverEntity);
        return server;
    }

    public async Task<bool> CheckServerAvailability(ServerDTO server)
    {
        bool result = false;

        using (TcpClient client = new TcpClient())
        {
            try
            {
                client.Connect(server.Ip, server.Port);

                result = client.Connected;

                client.Close();
            }
            catch { }
        }

        return result;
    }
}
