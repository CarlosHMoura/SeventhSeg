using SeventhSeg.Domain.Validation;

namespace SeventhSeg.Domain.Entities;

public sealed class Movie : Entity
{
    public string Description { get; private set; }
    public string FileName { get; private set; }
    public string PathFile { get; private set; }
    public int SizeInBytes { get; private set; }
    public Guid ServerId { get; set; }
    public Server? Server { get; set; }

    public Movie(string description, string fileName, string pathFile, int sizeInBytes, Guid serverId)
    {
        this.Id = Guid.NewGuid();
        ValidateDomain(description, fileName, pathFile, sizeInBytes, serverId);
    }

    public Movie(string id, string description, string fileName, string pathFile, int sizeInBytes, Guid serverId)
    {
        Guid guidId;

        DomainExceptionValidation.When(!Guid.TryParse(id, out guidId), "Invalid Id value.");
        Id = guidId;

        ValidateDomain(description, fileName, pathFile, sizeInBytes, serverId);
    }

    private void ValidateDomain(string description, string fileName, string pathFile, int sizeInBytes, Guid serverId)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(description),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(description.Length < 3,
            "Invalid name, too short, minimum 3 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(fileName),
            "Invalid file name. File name is required");

        DomainExceptionValidation.When(fileName.Length < 3,
            "Invalid file name, too short, minimum 3 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(pathFile),
            "Invalid path file. Path file is required");

        DomainExceptionValidation.When(pathFile.Length < 3,
            "Invalid path file, too short, minimum 3 characters");

        DomainExceptionValidation.When(sizeInBytes < 0, "Invalid size value");


        Description = description;
        FileName = fileName;
        PathFile = pathFile;
        SizeInBytes = sizeInBytes;
        ServerId = serverId;
    }
}
