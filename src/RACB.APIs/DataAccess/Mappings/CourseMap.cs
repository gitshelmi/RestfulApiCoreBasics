using AutoMapper;
using RACB.API.DTOs;
using RACB.API.Models;

namespace RACB.APIs.DataAccess.Mappings
{
    public class CourseMap: Profile
    {
        public CourseMap()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<NewCourse, Course>();
        }
    }
}
