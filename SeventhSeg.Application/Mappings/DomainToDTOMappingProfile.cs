using AutoMapper;
using SeventhSeg.Application.DTOs;
using SeventhSeg.Domain.Entities;

namespace SeventhSeg.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Server, ServerDTO>().ReverseMap();
        CreateMap<Movie, MovieDTO>().ReverseMap();
        CreateMap<Recycler, RecyclerDTO>().ReverseMap();
    }
}
