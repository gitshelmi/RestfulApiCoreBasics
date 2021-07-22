using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RACB.API.DTOs;
using RACB.API.Models;

namespace RACB.APIs.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ??
                                throw new ArgumentException(nameof(courseRepository));
            _mapper = mapper ??
                      throw new ArgumentException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(
            [FromQuery(Name = "mainCategory")] string category, string searchQuery)
        {
            var authors = _courseRepository.GetAuthors(category, searchQuery);

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public ActionResult<AuthorDto> GetAuthor(Guid authorId)
        {
            var author = _courseRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(NewAuthorDto newAuthor)
        {
            var author = _mapper.Map<Author>(newAuthor);
            _courseRepository.AddAuthor(author);
            _courseRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(author);

            return CreatedAtRoute("GetAuthor",
                new { authorId = authorToReturn.Id },
                authorToReturn);
        }

        [HttpOptions]
        public IActionResult AuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,HEAD");

            return Ok();
        }
    }
}
