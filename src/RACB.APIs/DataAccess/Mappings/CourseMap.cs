using AutoMapper;
using RACB.APIs.DTOs;
using RACB.APIs.Models;

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
