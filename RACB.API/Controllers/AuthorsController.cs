using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using RACB.API.DTOs;
using RACB.API.Models;

namespace RACB.API.Controllers
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
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authors = _courseRepository.GetAuthors();

            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}")]
        [HttpHead]
        public ActionResult<AuthorDto> GetAuthor(Guid authorId)
        {
            var author = _courseRepository.GetAuthor(authorId);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(author));
        }
    }
}
