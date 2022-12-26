using SeventhSeg.Domain.Validation;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace SeventhSeg.Domain.Entities;

public sealed class Server : Entity
{
    public string Name { get; private set; }
    public string Ip { get; private set; }
    public int Port { get; private set; }
    public IEnumerable<Movie> Movies { get; }

    public Server(string name, string ip, int port)
    {
        this.Id= Guid.NewGuid();
        ValidateDomain(name,ip, port);
    }

    public Server(string id, string name, string ip, int port)
    {
        Guid guidId;
        
        DomainExceptionValidation.When(!Guid.TryParse(id, out guidId), "Invalid Id value.");
        Id = guidId;

        ValidateDomain(name, ip, port);
    }

    private void ValidateDomain(string name, string ip, int port)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(name.Length < 3,
            "Invalid name, too short, minimum 3 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(ip),
            "Invalid IP. IP is required");
        
        DomainExceptionValidation.When(ip.Length < 7,
            "Invalid IP, too short, minimum 7 characters");

        DomainExceptionValidation.When(port < 0, "Invalid port value");

        Name = name;
        Ip = ip;
        Port = port;
    }
}
