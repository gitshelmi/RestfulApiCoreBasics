using Microsoft.AspNetCore.Mvc;
using System;
using RACB.API.Services;

namespace RACB.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public AuthorsController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ??
                                throw new ArgumentException(nameof(courseRepository));
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _courseRepository.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var author = _courseRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
    }
}
