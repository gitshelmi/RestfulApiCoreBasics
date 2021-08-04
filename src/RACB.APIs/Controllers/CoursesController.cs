using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RACB.APIs.DTOs;
using RACB.APIs.Models;

namespace RACB.APIs.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                                 throw new ArgumentException(nameof(courseRepository));
            _mapper = mapper ??
                      throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<CourseDto>> GetAuthorCourses(Guid authorId)
        {
            if (!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courses = _courseRepository.GetCourses(authorId);

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{courseId}", Name = "GetAuthorCourse")]
        public ActionResult<CourseDto> GetAuthorCourse(Guid authorId, Guid courseId)
        {
            if (!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _courseRepository.GetCourse(authorId, courseId);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateAuthorCourse(Guid authorId, NewCourseDto newCourseDto)
        {
            if (!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = _mapper.Map<Course>(newCourseDto);
            _courseRepository.AddCourse(authorId, course);
            _courseRepository.Save();

            return CreatedAtRoute("GetAuthorCourse",
                new { authorId = authorId, courseId = course.Id },
                _mapper.Map<CourseDto>(course));
        }

        [HttpPut("{courseId}")]
        public ActionResult UpdateCourseForAuthor(Guid authorId,
            Guid courseId,
            UpdatedCourseDto updatedCourse)
        {
            if (!_courseRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseToUpdate = _courseRepository.GetCourse(authorId, courseId);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedCourse, courseToUpdate);

            _courseRepository.UpdateCourse(courseToUpdate);

            _courseRepository.Save();

            return NoContent();
        }
    }
}
