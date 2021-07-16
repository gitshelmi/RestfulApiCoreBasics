using AutoMapper;
using RACB.API.DTOs;
using RACB.API.Models;

namespace RACB.API.DataAccess.Mappings
{
    public class CourseMap: Profile
    {
        public CourseMap()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
