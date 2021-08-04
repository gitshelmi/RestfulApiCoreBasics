using AutoMapper;
using RACB.APIs.DTOs;
using RACB.APIs.Helpers.Extensions;
using RACB.APIs.Models;

namespace RACB.APIs.DataAccess.Mappings
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
