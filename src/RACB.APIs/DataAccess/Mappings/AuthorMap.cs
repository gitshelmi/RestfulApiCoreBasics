using AutoMapper;
using RACB.API.DTOs;
using RACB.API.Extensions;
using RACB.API.Models;

namespace RACB.API.DataAccess.Mappings
{
    public class AuthorMap : Profile
    {
        public AuthorMap()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    destination => destination.Name,
                    map => map.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(
                    destination => destination.Age,
                    map => map.MapFrom(source => source.DateOfBirth.GetCurrentAge()));

            CreateMap<NewAuthorDto, Author>();
        }
    }
}
