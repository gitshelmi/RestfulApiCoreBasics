using AutoMapper;
using RACB.API.DTOs;
using RACB.API.Models;
using RACB.APIs.DTOs;

namespace RACB.APIs.DataAccess.Mappings
{
    public class CourseMap: Profile
    {
        public CourseMap()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<NewCourseDto, Course>();
            CreateMap<UpdatedCourseDto, Course>();
        }
    }
}
